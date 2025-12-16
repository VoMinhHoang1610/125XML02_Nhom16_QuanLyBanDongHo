using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyBanDongHo
{
    class Restore
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-COK2SPF\\KIEUNHI;Initial Catalog=BanDongHo;User ID=sa;Password=12345;TrustServerCertificate=True");
        public void RestoreData()
        {
            RestoreData("select * from NhanVien", "NhanVien");
            RestoreData("select * from KhachHang", "KhachHang");
            RestoreData("select * from DanhMucSanPham", "DanhMucSanPham");
            RestoreData("select * from ChiTietSanPham", "ChiTietSanPham");
            RestoreData("select * from HoaDonNhapXuat", "HoaDonNhapXuat");
            RestoreData("select * from ChiTietHoaDon", "ChiTietHoaDon");
        }
        public void RestoreData(String query, String XMLName)
        {
            try
            {

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conn);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds, XMLName);
                ds.DataSetName = XMLName + "s";
                ds.WriteXml(XMLName + "s" + ".xml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
