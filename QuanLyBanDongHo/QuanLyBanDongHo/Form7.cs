using QuanLyBanDongHo.Model;
using System;
using System.Windows.Forms;
using System.Xml;

namespace QuanLyBanDongHo
{
    public partial class Form7 : Form
    {
        XMLFile XmlFile = new XMLFile();
        XmlDocument XDoc;
        string tenKhach = "", tenNV = "";
        int tongtien = 0, soluong = 0;

        public Form7()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonXemChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvDonHang.CurrentRow == null || gvDonHang.CurrentRow.Index < 0)
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn cần xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maHD = gvDonHang.CurrentRow.Cells[0].Value?.ToString();
                if (string.IsNullOrEmpty(maHD))
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Hiển thị chi tiết hóa đơn
                ShowChiTietHoaDon(maHD);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowChiTietHoaDon(string maHD)
        {
            try
            {
                XmlDocument docCTHD = XmlFile.getXmlDocument("ChiTietHoaDons.xml");
                XmlDocument docSP = XmlFile.getXmlDocument("ChiTietSanPhams.xml");
                
                XmlNodeList chiTietList = docCTHD.SelectNodes($"/ChiTietHoaDons/ChiTietHoaDon[maHD='{maHD}']");
                
                if (chiTietList.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy chi tiết hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string chiTietText = $"CHI TIẾT HÓA ĐƠN: {maHD}\n";
                chiTietText += "".PadRight(50, '=') + "\n\n";
                
                int tongTien = 0;
                int stt = 0;
                
                foreach (XmlNode chiTiet in chiTietList)
                {
                    stt++;
                    string maSP = chiTiet.ChildNodes[1].InnerText;
                    string soLuong = chiTiet.ChildNodes[2].InnerText;
                    string donGia = chiTiet.ChildNodes[3].InnerText;
                    
                    // Tìm tên sản phẩm
                    XmlNodeList sanPhamList = docSP.SelectNodes($"/ChiTietSanPhams/ChiTietSanPham[maSP='{maSP}']");
                    string tenSP = sanPhamList.Count > 0 ? sanPhamList[0].ChildNodes[1].InnerText : "Không xác định";
                    
                    int thanhTien = int.Parse(soLuong) * int.Parse(donGia);
                    tongTien += thanhTien;
                    
                    chiTietText += $"{stt}. {tenSP} ({maSP})\n";
                    chiTietText += $"   Số lượng: {soLuong} x {string.Format("{0:N0}", int.Parse(donGia))} = {string.Format("{0:N0}", thanhTien)} VNĐ\n\n";
                }
                
                chiTietText += "".PadRight(50, '-') + "\n";
                chiTietText += $"TỔNG TIỀN: {string.Format("{0:N0}", tongTien)} VNĐ";
                
                MessageBox.Show(chiTietText, "Chi tiết hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void loadTable()
        {
            try
            {
                gvDonHang.Rows.Clear();
                
                // Load các file XML
                XmlDocument docHD = XmlFile.getXmlDocument("HoaDonNhapXuats.xml");
                XmlDocument docNV = XmlFile.getXmlDocument("NhanViens.xml");
                XmlDocument docKH = XmlFile.getXmlDocument("KhachHangs.xml");
                XmlDocument docCTHD = XmlFile.getXmlDocument("ChiTietHoaDons.xml");
                
                XmlNodeList nodeListDH = docHD.SelectNodes("/HoaDonNhapXuats/HoaDonNhapXuat");
                XmlNodeList nodeListNV = docNV.SelectNodes("/NhanViens/NhanVien");
                XmlNodeList nodeListKH = docKH.SelectNodes("/KhachHangs/KhachHang");
                XmlNodeList nodeListCTHD = docCTHD.SelectNodes("/ChiTietHoaDons/ChiTietHoaDon");
                
                foreach (XmlNode hoaDon in nodeListDH)
                {
                    // Hiển thị tất cả hóa đơn (cả nhập và xuất)
                    tongtien = 0;
                    soluong = 0;
                    tenKhach = "Không xác định";
                    tenNV = "Không xác định";
                    
                    string maHD = hoaDon.ChildNodes[0]?.InnerText ?? "";
                    string maNV = hoaDon.ChildNodes[1]?.InnerText ?? "";
                    string maKH = hoaDon.ChildNodes[2]?.InnerText ?? "";
                    string loaiHD = hoaDon.ChildNodes[3]?.InnerText ?? "";
                    string ngayTao = "";
                    
                    // Lấy ngày tạo nếu có (có thể là ChildNodes[4])
                    if (hoaDon.ChildNodes.Count > 4)
                    {
                        ngayTao = hoaDon.ChildNodes[4]?.InnerText ?? "";
                        if (!string.IsNullOrEmpty(ngayTao))
                        {
                            try
                            {
                                DateTime dt = DateTime.Parse(ngayTao);
                                ngayTao = dt.ToString("dd/MM/yyyy HH:mm");
                            }
                            catch
                            {
                                ngayTao = "Không có";
                            }
                        }
                        else
                        {
                            ngayTao = "Không có";
                        }
                    }
                    else
                    {
                        ngayTao = "Không có";
                    }
                    
                    // Tìm tên khách hàng
                    foreach (XmlNode khach in nodeListKH)
                    {
                        if (khach.ChildNodes[0]?.InnerText == maKH)
                        {
                            tenKhach = khach.ChildNodes[1]?.InnerText ?? "Không xác định";
                            break;
                        }
                    }
                    
                    // Tìm tên nhân viên
                    foreach (XmlNode nhanVien in nodeListNV)
                    {
                        if (nhanVien.ChildNodes[0]?.InnerText == maNV)
                        {
                            tenNV = nhanVien.ChildNodes[1]?.InnerText ?? "Không xác định";
                            break;
                        }
                    }
                    
                    // Tính tổng tiền và số lượng từ chi tiết hóa đơn
                    foreach (XmlNode chiTiet in nodeListCTHD)
                    {
                        if (chiTiet.ChildNodes[0]?.InnerText == maHD)
                        {
                            int soLuongSP = int.Parse(chiTiet.ChildNodes[2]?.InnerText ?? "0");
                            int donGia = int.Parse(chiTiet.ChildNodes[3]?.InnerText ?? "0");
                            
                            soluong += soLuongSP;
                            tongtien += (soLuongSP * donGia); // Tính thành tiền đúng
                        }
                    }
                    
                    // Xác định loại hóa đơn
                    string loaiHoaDon = "";
                    switch (loaiHD)
                    {
                        case "N":
                            loaiHoaDon = "📦 Nhập hàng";
                            break;
                        case "X":
                            loaiHoaDon = "💰 Bán hàng";
                            break;
                        default:
                            loaiHoaDon = "❓ Không xác định";
                            break;
                    }
                    
                    // Thêm vào DataGridView với đầy đủ thông tin
                    gvDonHang.Rows.Add(maHD, tenKhach, tenNV, soluong, string.Format("{0:N0}", tongtien), ngayTao, loaiHoaDon);
                }
                
                Console.WriteLine($"Đã load {gvDonHang.Rows.Count} hóa đơn");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            loadTable();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
