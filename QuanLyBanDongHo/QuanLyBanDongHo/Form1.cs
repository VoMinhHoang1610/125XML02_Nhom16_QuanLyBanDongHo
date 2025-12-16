using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QuanLyBanDongHo.Model;

namespace QuanLyBanDongHo
{
    public partial class Form1 : Form
    {
        static public Form2 form2;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDangNhap_Click(object sender, EventArgs e)
        {
            DangNhap DN = new DangNhap();
            String maNV = DN.Login(textBoxTenDN.Text, textBoxMatKhau.Text);
            if (!maNV.Equals(""))
            {
                form2 = new Form2();
                form2.maNV = maNV;
                form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng Nhập Thất Bại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Console.WriteLine(DN.Login(textBoxTenDN.Text, textBoxMatKhau.Text));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
       
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Tạo gradient background
            Rectangle rect = panel1.ClientRectangle;
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, 
                Color.FromArgb(41, 128, 185), Color.FromArgb(109, 213, 250), 45f))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {
            // Tạo shadow effect cho panel login
            Panel panel = sender as Panel;
            Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);
            
            // Vẽ shadow
            Rectangle shadowRect = new Rectangle(5, 5, panel.Width, panel.Height);
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
            {
                e.Graphics.FillRectangle(shadowBrush, shadowRect);
            }
            
            // Vẽ panel chính
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            
            // Vẽ border
            using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 1))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private void textBox_Paint(object sender, PaintEventArgs e)
        {
            // Vẽ border cho textbox
            TextBox textBox = sender as TextBox;
            Rectangle rect = new Rectangle(0, textBox.Height - 2, textBox.Width, 2);
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(52, 152, 219)))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.FromArgb(41, 128, 185);
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.FromArgb(52, 152, 219);
        }
    }
}
