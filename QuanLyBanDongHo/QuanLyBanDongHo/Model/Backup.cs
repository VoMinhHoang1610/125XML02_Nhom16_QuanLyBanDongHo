using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace QuanLyBanDongHo
{
    class BackUp
    {
        SqlConnection conn = new SqlConnection("Data Source=localhost\\MSSQLSERVER01;Initial Catalog=BanDongHo;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        // Cái này là overload                             
        public void BackUpData()
        {
            try
            {
                conn.Open();
                
                // Disable tất cả foreign key constraints
                ExecuteNonQuery("EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");
                
                // Xóa dữ liệu từ tất cả bảng
                ExecuteNonQuery("DELETE FROM ChiTietHoaDon");
                ExecuteNonQuery("DELETE FROM HoaDonNhapXuat");
                ExecuteNonQuery("DELETE FROM ChiTietSanPham");
                ExecuteNonQuery("DELETE FROM KhachHang");  // Xóa tất cả khách hàng
                ExecuteNonQuery("DELETE FROM DanhMucSanPham");
                ExecuteNonQuery("DELETE FROM NhanVien");
                
                // Insert dữ liệu mới theo thứ tự
                BackUpData("NhanVien");
                BackUpData("DanhMucSanPham");
                BackUpData("KhachHang");
                BackUpData("ChiTietSanPham");
                BackUpData("HoaDonNhapXuat");
                BackUpData("ChiTietHoaDon");
                
                // Enable lại tất cả foreign key constraints
                ExecuteNonQuery("EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'");
                
                conn.Close();
                Console.WriteLine("Sao lưu hoàn tất!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi sao lưu: {ex.Message}");
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    // Đảm bảo enable lại constraints nếu có lỗi
                    try
                    {
                        ExecuteNonQuery("EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'");
                    }
                    catch { }
                    conn.Close();
                }
                throw;
            }
        }
        
        private void ExecuteNonQuery(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"Executed: {query} - {rows} rows affected");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing: {query} - {ex.Message}");
            }
        }
        
        private String toString(XElement elm, String tableName)
        {
            String result = "";
            foreach (XElement x in elm.Elements())
            {
                if (x == elm.LastNode)
                    result += "N'" + x.Value + "'";
                else
                {
                    result += "N'" + x.Value + "',";
                }
            }
            
            // Xử lý đặc biệt cho bảng HoaDonNhapXuat - thêm cột ngayTao
            if (tableName == "HoaDonNhapXuat")
            {
                result += ",GETDATE()";
            }
            
            return "(" + result + "),\n";
        }

        // Cái này là overload    
        private void BackUpData(String XMLFileName)
        {
            try
            {
                XDocument XDoc = XDocument.Load(XMLFileName + "s.xml");
                
                // Xử lý từng record một để tránh lỗi duplicate key
                foreach (XElement x in XDoc.Descendants(XMLFileName))
                {
                    try
                    {
                        string query = "";
                        
                        // Tạo câu INSERT cho từng bảng
                        switch (XMLFileName)
                        {
                            case "HoaDonNhapXuat":
                                query = "INSERT INTO HoaDonNhapXuat (maHD, maNV, maKH, LoaiHD, ngayTao) VALUES " + toString(x, XMLFileName).TrimEnd(',', '\n');
                                break;
                            case "KhachHang":
                                query = "INSERT INTO KhachHang (maKH, tenKH, SoTienDaDung, namSinh, diaChi) VALUES " + toString(x, XMLFileName).TrimEnd(',', '\n');
                                break;
                            default:
                                query = "INSERT INTO " + XMLFileName + " VALUES " + toString(x, XMLFileName).TrimEnd(',', '\n');
                                break;
                        }
                        
                        SqlCommand command = new SqlCommand(query, conn);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception recordEx)
                    {
                        // Bỏ qua lỗi duplicate key, log các lỗi khác
                        if (!recordEx.Message.Contains("PRIMARY KEY") && !recordEx.Message.Contains("duplicate"))
                        {
                            Console.WriteLine($"Lỗi insert record {XMLFileName}: {recordEx.Message}");
                        }
                    }
                }
                
                Console.WriteLine($"BackUp {XMLFileName}: Completed");
            }
            catch (Exception e)
            {
                // try catch để biết lỗi
                Console.WriteLine($"Lỗi BackUp {XMLFileName}: {e.Message}");
                System.Windows.Forms.MessageBox.Show($"Lỗi sao lưu {XMLFileName}: {e.Message}", "Lỗi", 
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

    }
}
