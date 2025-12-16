using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyBanDongHo.Model;
using System.Xml;

namespace QuanLyBanDongHo
{
    public partial class Form4 : Form
    {

        XMLFile XmlFile = new XMLFile();
        XmlDocument XDoc;
        XmlDocument XDocKhachHang;
        int sTT = 0;


        public Form4()
        {
            InitializeComponent();
        }


        void loadTable() {
            try
            {
                dataGridView1.Rows.Clear();
                
                if (!System.IO.File.Exists("ChiTietSanPhams.xml"))
                {
                    MessageBox.Show("File ChiTietSanPhams.xml không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                XDoc = XmlFile.getXmlDocument("ChiTietSanPhams.xml");
                if (XDoc == null)
                {
                    MessageBox.Show("Không thể load file XML!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                XmlNodeList nodeList = XDoc.SelectNodes("/ChiTietSanPhams/ChiTietSanPham");
                if (nodeList == null || nodeList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                foreach (XmlNode x in nodeList)
                {
                    try
                    {
                        // Kiểm tra node có đủ child nodes không
                        if (x.ChildNodes.Count < 7)
                        {
                            Console.WriteLine($"Node không đủ child nodes: {x.ChildNodes.Count}");
                            continue;
                        }
                        
                        // Cấu trúc XML: maSP, tenSP, soLuong, chiTiet, maDMSP, giaNhap, gia
                        // Form4 sử dụng giá bán (gia) - ChildNodes[6]
                        string maSP = x.ChildNodes[0]?.InnerText ?? "";
                        string tenSP = x.ChildNodes[1]?.InnerText ?? "";
                        string chiTiet = x.ChildNodes[3]?.InnerText ?? "";
                        string soLuong = x.ChildNodes[2]?.InnerText ?? "0";
                        string giaBan = x.ChildNodes[6]?.InnerText ?? "0";
                        
                        dataGridView1.Rows.Add(maSP, tenSP, chiTiet, soLuong, giaBan);
                    }
                    catch (Exception nodeEx)
                    {
                        Console.WriteLine($"Lỗi khi xử lý node: {nodeEx.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                textBoxMaKhachHang.Text = "";
                
                if (System.IO.File.Exists("KhachHangs.xml"))
                {
                    XDocKhachHang = XmlFile.getXmlDocument("KhachHangs.xml");
                }
                else
                {
                    MessageBox.Show("File KhachHangs.xml không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                loadTable();
                
                // Thêm event handler cho việc chọn sản phẩm
                if (dataGridView1 != null)
                {
                    dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
                }
                
                // Test load ảnh ngay khi form load
                Console.WriteLine("Form4 đã load, test load ảnh...");
                LoadDefaultImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo Form thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method để refresh dữ liệu khi có thay đổi
        public void RefreshData()
        {
            try
            {
                Console.WriteLine("RefreshData được gọi trong Form4");
                loadTable();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi refresh data: " + ex.Message);
            }
        }

        // Override Activated để refresh mỗi khi form được focus
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Console.WriteLine("Form4 được activated, refresh data...");
            RefreshData();
        }

        // Override VisibleChanged để refresh khi form hiển thị
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                Console.WriteLine("Form4 hiển thị, refresh data...");
                RefreshData();
            }
        }



        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            LoadProductImage();
        }

        private void LoadProductImage()
        {
            try
            {
                Console.WriteLine("LoadProductImage được gọi");
                
                if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
                {
                    Console.WriteLine("Không có row được chọn");
                    LoadDefaultImage();
                    return;
                }

                string maSP = dataGridView1.CurrentRow.Cells[0].Value?.ToString();
                Console.WriteLine($"Mã SP: {maSP}");
                
                if (string.IsNullOrEmpty(maSP))
                {
                    Console.WriteLine("Mã SP rỗng");
                    LoadDefaultImage();
                    return;
                }

                // Kiểm tra thư mục hiện tại
                string currentDir = System.IO.Directory.GetCurrentDirectory();
                Console.WriteLine($"Thư mục hiện tại: {currentDir}");

                // Tìm ảnh theo mã sản phẩm
                string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
                
                foreach (string ext in imageExtensions)
                {
                    string imagePath = $"imgs/{maSP}{ext}";
                    string fullPath = System.IO.Path.GetFullPath(imagePath);
                    Console.WriteLine($"Kiểm tra file: {fullPath}");
                    
                    if (System.IO.File.Exists(imagePath))
                    {
                        Console.WriteLine($"Tìm thấy ảnh: {imagePath}");
                        
                        // Giải phóng ảnh cũ trước khi load ảnh mới
                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image.Dispose();
                            pictureBox1.Image = null;
                        }
                        
                        using (var fs = new System.IO.FileStream(imagePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        {
                            pictureBox1.Image = Image.FromStream(fs);
                        }
                        Console.WriteLine("Đã load ảnh thành công");
                        return;
                    }
                }
                
                Console.WriteLine("Không tìm thấy ảnh nào, load ảnh mặc định");
                LoadDefaultImage();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi load ảnh: " + ex.Message);
                LoadDefaultImage();
            }
        }

        private void LoadDefaultImage()
        {
            try
            {
                Console.WriteLine("LoadDefaultImage được gọi");
                
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }

                string defaultImagePath = "imgs/noimage.png";
                string fullDefaultPath = System.IO.Path.GetFullPath(defaultImagePath);
                Console.WriteLine($"Kiểm tra ảnh mặc định: {fullDefaultPath}");
                
                if (System.IO.File.Exists(defaultImagePath))
                {
                    Console.WriteLine("Tìm thấy ảnh mặc định, đang load...");
                    using (var fs = new System.IO.FileStream(defaultImagePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        pictureBox1.Image = Image.FromStream(fs);
                    }
                    Console.WriteLine("Đã load ảnh mặc định thành công");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy ảnh mặc định");
                    // Tạo ảnh placeholder bằng code
                    CreatePlaceholderImage();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi load ảnh mặc định: " + ex.Message);
                CreatePlaceholderImage();
            }
        }

        private void CreatePlaceholderImage()
        {
            try
            {
                // Tạo ảnh placeholder 300x300 với text "Không có ảnh"
                Bitmap placeholder = new Bitmap(300, 300);
                using (Graphics g = Graphics.FromImage(placeholder))
                {
                    g.Clear(Color.LightGray);
                    g.DrawRectangle(Pens.Gray, 0, 0, 299, 299);
                    
                    string text = "Không có ảnh";
                    Font font = new Font("Arial", 16, FontStyle.Bold);
                    SizeF textSize = g.MeasureString(text, font);
                    float x = (300 - textSize.Width) / 2;
                    float y = (300 - textSize.Height) / 2;
                    
                    g.DrawString(text, font, Brushes.DarkGray, x, y);
                }
                
                pictureBox1.Image = placeholder;
                Console.WriteLine("Đã tạo ảnh placeholder");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tạo placeholder: " + ex.Message);
            }
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần thêm vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(textBoxSoLuong.Text))
                {
                    MessageBox.Show("Vui lòng nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int soLuongMua = int.Parse(textBoxSoLuong.Text);
                int soLuongTon = int.Parse(dataGridView1.CurrentRow.Cells[3].FormattedValue.ToString());
                
                if (soLuongMua <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (soLuongMua > soLuongTon)
                {
                    MessageBox.Show($"Số lượng hàng không đủ! Chỉ còn {soLuongTon} sản phẩm trong kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
                string maSP = dataGridView1.CurrentRow.Cells[0].FormattedValue.ToString();
                bool daTonTai = false;
                int dongTonTai = -1;

                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    if (dataGridView2.Rows[i].Cells[1].Value.ToString() == maSP)
                    {
                        daTonTai = true;
                        dongTonTai = i;
                        break;
                    }
                }

                if (daTonTai)
                {
                    // Cập nhật số lượng nếu sản phẩm đã có
                    int soLuongCu = int.Parse(dataGridView2.Rows[dongTonTai].Cells[3].Value.ToString());
                    int soLuongMoi = soLuongCu + soLuongMua;
                    
                    if (soLuongMoi > soLuongTon)
                    {
                        MessageBox.Show($"Tổng số lượng ({soLuongMoi}) vượt quá số lượng tồn kho ({soLuongTon})!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    dataGridView2.Rows[dongTonTai].Cells[3].Value = soLuongMoi;
                    dataGridView2.Rows[dongTonTai].Cells[5].Value = soLuongMoi * int.Parse(dataGridView1.CurrentRow.Cells[4].FormattedValue.ToString());
                }
                else
                {
                    // Thêm sản phẩm mới vào giỏ hàng
                    dataGridView2.Rows.Add(
                        ++sTT,
                        maSP,
                        dataGridView1.CurrentRow.Cells[1].FormattedValue.ToString(),
                        soLuongMua,
                        dataGridView1.CurrentRow.Cells[4].FormattedValue.ToString(),
                        (soLuongMua * int.Parse(dataGridView1.CurrentRow.Cells[4].FormattedValue.ToString()))
                    );
                }

                textBoxSoLuong.Text = "1"; // Reset về 1
                MessageBox.Show("Thêm sản phẩm vào giỏ hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            capNhatTongTien();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            try 
            { 
                if (dataGridView2.CurrentRow != null)
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
                        MessageBox.Show("Xóa sản phẩm khỏi giỏ hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            capNhatTongTien();
        }

        void capNhatTongTien() {
            int tongTien = 0;
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++) {
                tongTien += int.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString());
            }
            labelTongTien.Text = tongTien.ToString();
        }

        private void dataGridView1_MouseCaptureChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    string maSP = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    LoadProductImage(maSP);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi hiển thị ảnh: " + ex.Message);
                pictureBox1.Image = null;
            }
        }

        private void LoadProductImage(string maSP)
        {
            try
            {
                // Thử các định dạng ảnh khác nhau
                string[] extensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
                
                foreach (string ext in extensions)
                {
                    string imagePath = "imgs/" + maSP + ext;
                    if (System.IO.File.Exists(imagePath))
                    {
                        // Giải phóng ảnh cũ nếu có
                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image.Dispose();
                        }
                        
                        // Load ảnh mới
                        using (var fs = new System.IO.FileStream(imagePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        {
                            pictureBox1.Image = Image.FromStream(fs);
                        }
                        return;
                    }
                }
                
                // Nếu không tìm thấy ảnh nào, xóa ảnh hiện tại
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi load ảnh: " + ex.Message);
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
            }
        }

        private void textBoxMaKhachHang_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxMaKhachHang.Text))
                {
                    labelDanhSachNS.Text = "Khách vãn lai";
                    return;
                }

                XDocKhachHang = XmlFile.getXmlDocument("KhachHangs.xml");
                XmlNodeList nodeList = XDocKhachHang.SelectNodes("/KhachHangs/KhachHang[maKH ='" + textBoxMaKhachHang.Text + "']");
                
                if (nodeList.Count != 0)
                {
                    labelDanhSachNS.Text = nodeList[0].ChildNodes[1].InnerText;
                    labelDanhSachNS.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    labelDanhSachNS.Text = "Không tìm thấy khách hàng";
                    labelDanhSachNS.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                labelDanhSachNS.Text = "Lỗi: " + ex.Message;
                labelDanhSachNS.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count != 1)
                {
                    dataGridView2.CurrentRow.Cells[5].Value = (int.Parse(dataGridView2.CurrentRow.Cells[3].Value.ToString()) * int.Parse(dataGridView2.CurrentRow.Cells[4].Value.ToString())).ToString();
                    capNhatTongTien();
                }

            }
            catch (Exception)
            {

            }
        }

        private void buttonThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra giỏ hàng có sản phẩm không
                if (dataGridView2.Rows.Count <= 1)
                {
                    MessageBox.Show("Giỏ hàng trống! Vui lòng thêm sản phẩm trước khi thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra tổng tiền
                if (labelTongTien.Text == "0" || string.IsNullOrEmpty(labelTongTien.Text))
                {
                    MessageBox.Show("Tổng tiền không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận thanh toán
                string thongTinHoaDon = $"Tổng tiền: {labelTongTien.Text} VNĐ\n";
                thongTinHoaDon += $"Khách hàng: {(string.IsNullOrEmpty(labelDanhSachNS.Text) ? "Khách vãn lai" : labelDanhSachNS.Text)}\n";
                thongTinHoaDon += $"Số sản phẩm: {dataGridView2.Rows.Count - 1}\n\n";
                thongTinHoaDon += "Bạn có chắc muốn thanh toán không?";

                DialogResult result = MessageBox.Show(thongTinHoaDon, "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result != DialogResult.Yes)
                    return;

                // Tạo danh sách chi tiết hóa đơn
                List<XmlNode> nodeList = new List<XmlNode>();
                XmlDocument XDoc = XmlFile.getXmlDocument("ChiTietHoaDons.xml");
                
                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    XmlElement node = XDoc.CreateElement("ChiTietHoaDon");

                    XmlElement maSP = XDoc.CreateElement("maSP");
                    maSP.InnerText = dataGridView2.Rows[i].Cells[1].Value.ToString();

                    XmlElement soLuong = XDoc.CreateElement("soLuong");
                    soLuong.InnerText = dataGridView2.Rows[i].Cells[3].Value.ToString();
                    
                    XmlElement donGia = XDoc.CreateElement("DonGia");
                    donGia.InnerText = dataGridView2.Rows[i].Cells[4].Value.ToString();

                    node.AppendChild(maSP);
                    node.AppendChild(soLuong);
                    node.AppendChild(donGia);
                    nodeList.Add(node);
                }

                HoaDon hoaDon = new HoaDon();
                String maKH = textBoxMaKhachHang.Text;
                // Xử lý mã khách hàng
                if (string.IsNullOrEmpty(maKH))
                    maKH = "KH00000"; // Khách vãn lai

                // Lấy mã nhân viên từ Form2 (người đang đăng nhập)
                String maNV = "NV00001"; // Default
                if (Form1.form2 != null && !string.IsNullOrEmpty(Form1.form2.maNV))
                {
                    maNV = Form1.form2.maNV;
                    Console.WriteLine($"Sử dụng mã nhân viên: {maNV}");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy thông tin nhân viên, sử dụng default NV00001");
                }

                // Cập nhật số tiền đã dùng của khách hàng
                XmlNodeList khachHangNodes = XDocKhachHang.SelectNodes("/KhachHangs/KhachHang[maKH = '" + maKH + "']");
                if (khachHangNodes.Count > 0)
                {
                    int soTienCu = int.Parse(khachHangNodes[0].ChildNodes[2].InnerText);
                    int soTienMoi = soTienCu + int.Parse(labelTongTien.Text);
                    khachHangNodes[0].ChildNodes[2].InnerText = soTienMoi.ToString();
                    XDocKhachHang.Save("KhachHangs.xml");
                }

                // Lưu hóa đơn với mã nhân viên đúng
                hoaDon.add(XDoc, nodeList, maKH, maNV, "X");

                // Cập nhật lại danh sách sản phẩm
                loadTable();

                // Reset form
                resetAll();

                MessageBox.Show("Thanh toán thành công!\nCảm ơn quý khách đã mua hàng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void resetAll() {
            dataGridView2.Rows.Clear();
            textBoxMaKhachHang.Text = "";
            labelDanhSachNS.Text = "Khách vãn lai";
            labelDanhSachNS.ForeColor = System.Drawing.Color.Black;
            labelTongTien.Text = "0";
            textBoxSoLuong.Text = "1";
            sTT = 0;
            pictureBox1.Image = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Vẽ placeholder khi không có ảnh
            PictureBox pb = sender as PictureBox;
            if (pb.Image == null)
            {
                Rectangle rect = pb.ClientRectangle;
                
                // Vẽ background gradient
                using (LinearGradientBrush brush = new LinearGradientBrush(rect, 
                    Color.FromArgb(240, 240, 240), Color.FromArgb(220, 220, 220), 45f))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
                
                // Vẽ icon camera
                using (Font font = new Font("Segoe UI", 24, FontStyle.Regular))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(150, 150, 150)))
                {
                    string text = "📷";
                    SizeF textSize = e.Graphics.MeasureString(text, font);
                    PointF textPos = new PointF(
                        (rect.Width - textSize.Width) / 2,
                        (rect.Height - textSize.Height) / 2 - 20
                    );
                    e.Graphics.DrawString(text, font, textBrush, textPos);
                }
                
                // Vẽ text "No Image"
                using (Font font = new Font("Segoe UI", 12, FontStyle.Regular))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(120, 120, 120)))
                {
                    string text = "Không có hình ảnh";
                    SizeF textSize = e.Graphics.MeasureString(text, font);
                    PointF textPos = new PointF(
                        (rect.Width - textSize.Width) / 2,
                        (rect.Height - textSize.Height) / 2 + 30
                    );
                    e.Graphics.DrawString(text, font, textBrush, textPos);
                }
            }
        }
    }
}
