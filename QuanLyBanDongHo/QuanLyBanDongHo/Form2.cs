using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyBanDongHo
{
    public partial class Form2 : Form
    {
        static public Form3 form3;
        static public Form4 form4;
        public static String nameEmployee;
        public String maNV = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void buttonNhapNS_Click(object sender, EventArgs e)
        {
            form3 = new Form3();
            form3.Show();
        }

        private void buttonXuatNS_Click(object sender, EventArgs e)
        {
            try
            {
                // Nếu form4 đã tồn tại, đóng nó trước
                if (form4 != null && !form4.IsDisposed)
                {
                    form4.Close();
                }
                
                // Tạo form4 mới để đảm bảo dữ liệu được refresh
                form4 = new Form4();
                form4.Show();
                
                Console.WriteLine("Form4 thanh toán đã được mở");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form thanh toán: " + ex.Message + "\n" + ex.StackTrace, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonQLKhachHang_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }

        private void buttonCapNhat_Click(object sender, EventArgs e)
        {
            FormSaoLuuData form6 = new FormSaoLuuData();
            form6.Show();
        }

        private void buttonLSThanhToan_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
        }

        private void buttonQLKho_Click(object sender, EventArgs e)
        {
            FormQLKho formKho = new FormQLKho();
            formKho.Show();
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {
            // Tạo gradient background cho panel content
            Panel panel = sender as Panel;
            Rectangle rect = panel.ClientRectangle;
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, 
                Color.White, Color.FromArgb(248, 249, 250), 90f))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            
            // Vẽ shadow
            using (Pen pen = new Pen(Color.FromArgb(30, 0, 0, 0), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }
    }
}
