using System;
using System.Windows.Forms;

namespace QuanLyBanDongHo
{
    public partial class FormSaoLuuData : Form
    {
        public FormSaoLuuData()
        {
            InitializeComponent();
        }

        private void buttonXML_SQL_Click(object sender, EventArgs e)
        {
            BackUp bk = new BackUp();
            bk.BackUpData();
            MessageBox.Show("Đã sao lưu lên máy chủ","Thành công!");
        }

        private void buttonKhoiPhuc_Click(object sender, EventArgs e)
        {
            Restore rs = new Restore();
            rs.RestoreData();
            MessageBox.Show("Đã khôi phục dữ liệu từ máy chủ", "Thành công!");
        }

        private void FormSaoLuuData_Load(object sender, EventArgs e)
        {

        }

        private void buttonSQL_XML_Click(object sender, EventArgs e)
        {

        }
    }
}
