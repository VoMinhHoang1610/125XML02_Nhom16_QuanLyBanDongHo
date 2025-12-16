using System;
using System.Windows.Forms;
using System.Xml;
using QuanLyBanDongHo.Model;

namespace QuanLyBanDongHo
{
    public partial class Form3 : Form
    {
        XMLFile XmlFile = new XMLFile();
        XmlNodeList nodeListDM;
        XmlNodeList nodeListCTSP;
        int stt = 0;
        public Form3()
        {
            InitializeComponent();
        }



        private void buttonInHoaDon_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 1)
            {
                MessageBox.Show("Không có sản phẩm nào để lưu!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có muốn lưu tất cả sản phẩm vào kho không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    HoaDon hd = new HoaDon();
                    hd.ThemHoaDon(dataGridView1, textBoxNhaCungCap.Text, "N");
                    
                    MessageBox.Show("Lưu sản phẩm vào kho thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Reset form
                    stt = 0;
                    dataGridView1.Rows.Clear();
                    textBoxNhaCungCap.Text = "";
                    textBoxSoLuong.Text = "";
                    textBoxDonGia.Text = "";
                    comboBoxLoaiSP.SelectedIndex = -1;
                    comboBoxTenSanPham.Items.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {   
            DanhMucSanPham DMSP = new DanhMucSanPham();
            nodeListDM = DMSP.getListMD();
            foreach (XmlNode x in nodeListDM)
            {
                comboBoxLoaiSP.Items.Add(x.ChildNodes[1].InnerText);
            
            }
        }

        private void comboBoxTenSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
   

        }

        private void comboBoxLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChiTietSanPham CTSP = new ChiTietSanPham();
                nodeListCTSP = CTSP.getListName();
                String maSP = "";
                comboBoxTenSanPham.Items.Clear();
                foreach (XmlNode x in nodeListDM)
                {
                    if(comboBoxLoaiSP.SelectedItem != null)
                        if (x.ChildNodes[1].InnerText.Equals(comboBoxLoaiSP.SelectedItem.ToString()))
                            maSP = x.ChildNodes[0].InnerText;
                }

                foreach (XmlNode x in nodeListCTSP)
                {
                    if (x.ChildNodes[4].InnerText.Equals(maSP))
                        comboBoxTenSanPham.Items.Add(x.ChildNodes[1].InnerText + " " + x.ChildNodes[0].InnerText);
                }
            }
            catch { }
          
        }

        private void dataGridView1_MouseCaptureChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
                {
                    // Hiển thị thông tin lên các control
                    string loaiSP = dataGridView1.CurrentRow.Cells[2].FormattedValue?.ToString();
                    string tenSP = dataGridView1.CurrentRow.Cells[1].FormattedValue?.ToString();
                    string maSP = dataGridView1.CurrentRow.Cells[6].FormattedValue?.ToString();
                    
                    // Set loại sản phẩm
                    if (!string.IsNullOrEmpty(loaiSP))
                    {
                        comboBoxLoaiSP.SelectedIndex = comboBoxLoaiSP.Items.IndexOf(loaiSP);
                    }
                    
                    // Set tên sản phẩm (cần load lại danh sách trước)
                    if (!string.IsNullOrEmpty(tenSP) && !string.IsNullOrEmpty(maSP))
                    {
                        string fullName = tenSP + " " + maSP;
                        comboBoxTenSanPham.SelectedIndex = comboBoxTenSanPham.Items.IndexOf(fullName);
                    }
                    
                    // Set số lượng và đơn giá
                    textBoxSoLuong.Text = dataGridView1.CurrentRow.Cells[3].FormattedValue?.ToString();
                    textBoxDonGia.Text = dataGridView1.CurrentRow.Cells[4].FormattedValue?.ToString();
                }
            }
            catch (Exception ex)
            {
                // Không hiển thị lỗi để tránh spam message box
                Console.WriteLine("Lỗi khi hiển thị thông tin: " + ex.Message);
            }
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxNhaCungCap.Text.Equals(""))
                {
                    int a = int.Parse(textBoxNhaCungCap.Text);
                }
                int soLuong = int.Parse(textBoxSoLuong.Text);
                int donGia = int.Parse(textBoxDonGia.Text);
                int tong = soLuong * donGia;
                dataGridView1.Rows.Add(++stt, comboBoxTenSanPham.SelectedItem.ToString().Substring(0, comboBoxTenSanPham.SelectedItem.ToString().Length - 7), comboBoxLoaiSP.SelectedItem.ToString(), soLuong, donGia, tong,
                    
                comboBoxTenSanPham.SelectedItem.ToString().Substring(comboBoxTenSanPham.SelectedItem.ToString().Length-7));
                textBoxSoLuong.Text = "";
                textBoxDonGia.Text = "";
            }
            catch {

                MessageBox.Show("Thêm sản phẩm Thất Bại", "Thông Báo");
            }
       
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                        MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(textBoxSoLuong.Text) || string.IsNullOrEmpty(textBoxDonGia.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int soLuong = int.Parse(textBoxSoLuong.Text);
                int donGia = int.Parse(textBoxDonGia.Text);
                int tong = soLuong * donGia;

                // Cập nhật dữ liệu trong DataGridView
                int currentRowIndex = dataGridView1.CurrentRow.Index;
                dataGridView1.Rows[currentRowIndex].Cells[1].Value = comboBoxTenSanPham.SelectedItem?.ToString().Substring(0, comboBoxTenSanPham.SelectedItem.ToString().Length - 7);
                dataGridView1.Rows[currentRowIndex].Cells[2].Value = comboBoxLoaiSP.SelectedItem?.ToString();
                dataGridView1.Rows[currentRowIndex].Cells[3].Value = soLuong;
                dataGridView1.Rows[currentRowIndex].Cells[4].Value = donGia;
                dataGridView1.Rows[currentRowIndex].Cells[5].Value = tong;
                dataGridView1.Rows[currentRowIndex].Cells[6].Value = comboBoxTenSanPham.SelectedItem?.ToString().Substring(comboBoxTenSanPham.SelectedItem.ToString().Length - 7);

                MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
