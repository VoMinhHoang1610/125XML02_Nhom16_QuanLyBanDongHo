using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyBanDongHo.Model;
using System.Xml;

namespace QuanLyBanDongHo
{
    public partial class Form5 : Form
    {
        XMLFile XmlFile = new XMLFile();
        int stt = 0;
        public Form5()
        {
            InitializeComponent();
        }

        void capNhatBang() {
            stt = 0;
            dataGridView1.Rows.Clear();
            XmlDocument XDoc = XmlFile.getXmlDocument("KhachHangs.xml");
            XmlNodeList nodeList = XDoc.SelectNodes("/KhachHangs/KhachHang");
            foreach (XmlNode x in nodeList)
            {
                dataGridView1.Rows.Add(++stt,
                    x.ChildNodes[1].InnerText,
                    x.ChildNodes[0].InnerText,
                    x.ChildNodes[2].InnerText,
                    x.ChildNodes[3].InnerText,
                    x.ChildNodes[4].InnerText);

            }

        }


        private void Form5_Load(object sender, EventArgs e)
        {
            capNhatBang();

        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            try 
            {
                // Validation đầu vào
                if (string.IsNullOrWhiteSpace(textBoxTenKH.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxTenKH.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxNamSinh.Text))
                {
                    MessageBox.Show("Vui lòng nhập năm sinh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxNamSinh.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxDiaChi.Text))
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxDiaChi.Focus();
                    return;
                }

                // Kiểm tra năm sinh hợp lệ
                int namSinh = int.Parse(textBoxNamSinh.Text);
                int namHienTai = DateTime.Now.Year;
                
                if (namSinh < 1900 || namSinh > namHienTai)
                {
                    MessageBox.Show($"Năm sinh không hợp lệ! Vui lòng nhập từ 1900 đến {namHienTai}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxNamSinh.Focus();
                    return;
                }

                // Thêm khách hàng
                KhachHang kh = new KhachHang();
                if (kh.themKhachHang(textBoxTenKH.Text.Trim(), namSinh, textBoxDiaChi.Text.Trim()))
                {
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    capNhatBang();
                    clearForm();
                }
                else
                {
                    MessageBox.Show("Thêm khách hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Năm sinh phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxNamSinh.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có chọn khách hàng không
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validation đầu vào
                if (string.IsNullOrWhiteSpace(textBoxTenKH.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxTenKH.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxNamSinh.Text))
                {
                    MessageBox.Show("Vui lòng nhập năm sinh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxNamSinh.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxDiaChi.Text))
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxDiaChi.Focus();
                    return;
                }

                // Kiểm tra năm sinh hợp lệ
                int namSinh = int.Parse(textBoxNamSinh.Text);
                int namHienTai = DateTime.Now.Year;
                
                if (namSinh < 1900 || namSinh > namHienTai)
                {
                    MessageBox.Show($"Năm sinh không hợp lệ! Vui lòng nhập từ 1900 đến {namHienTai}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxNamSinh.Focus();
                    return;
                }

                // Xác nhận sửa
                DialogResult result = MessageBox.Show("Bạn có chắc muốn sửa thông tin khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;

                // Sửa thông tin
                KhachHang kh = new KhachHang();
                string maKH = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                
                if (kh.suaThongTin(maKH, textBoxTenKH.Text.Trim(), namSinh, textBoxDiaChi.Text.Trim()))
                {
                    MessageBox.Show("Sửa thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    capNhatBang();
                    clearForm();
                }
                else
                {
                    MessageBox.Show("Sửa thông tin thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Năm sinh phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxNamSinh.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có chọn khách hàng không
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tenKH = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string maKH = dataGridView1.CurrentRow.Cells[2].Value.ToString();

                // Kiểm tra không cho xóa khách vãn lai
                if (maKH == "KH00000")
                {
                    MessageBox.Show("Không thể xóa khách vãn lai!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa khách hàng '{tenKH}' (Mã: {maKH})?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes) 
                {
                    KhachHang kh = new KhachHang();
                    if (kh.xoaThongTin(maKH))
                    {
                        MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        capNhatBang();
                        clearForm();
                    }
                    else
                    {
                        MessageBox.Show("Xóa khách hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_MouseCaptureChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
                {
                    textBoxTenKH.Text = dataGridView1.CurrentRow.Cells[1].Value?.ToString() ?? "";
                    textBoxNamSinh.Text = dataGridView1.CurrentRow.Cells[4].Value?.ToString() ?? "";
                    textBoxDiaChi.Text = dataGridView1.CurrentRow.Cells[5].Value?.ToString() ?? "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi hiển thị thông tin: " + ex.Message);
            }
        }

        private void clearForm()
        {
            textBoxTenKH.Text = "";
            textBoxNamSinh.Text = "";
            textBoxDiaChi.Text = "";
            textBoxTenKH.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
