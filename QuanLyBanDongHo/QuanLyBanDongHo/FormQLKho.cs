using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using QuanLyBanDongHo.Model;

namespace QuanLyBanDongHo
{
    public partial class FormQLKho : Form
    {
        XMLFile XmlFile = new XMLFile();
        XmlDocument XDoc;
        int selectedRowIndex = -1;

        public FormQLKho()
        {
            InitializeComponent();
        }

        private void FormQLKho_Load(object sender, EventArgs e)
        {
            try
            {
                // Đảm bảo các control đã được khởi tạo
                if (dataGridViewKho == null || comboBoxDanhMuc == null)
                {
                    MessageBox.Show("Form chưa được khởi tạo đầy đủ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                LoadDanhMuc();
                LoadDataToGrid();
                SetupTooltips();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataToGrid()
        {
            try
            {
                dataGridViewKho.Rows.Clear();
                
                // Kiểm tra file tồn tại
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
                
                int stt = 0;
                foreach (XmlNode x in nodeList)
                {
                    try
                    {
                        stt++;
                        
                        // Kiểm tra node có đủ child nodes không (cần 7 nodes: 0-6)
                        if (x.ChildNodes.Count < 7)
                        {
                            Console.WriteLine($"Node {stt} không đủ child nodes: {x.ChildNodes.Count}");
                            continue;
                        }
                        
                        // Kiểm tra và parse giá nhập và giá bán
                        string giaNhapText = x.ChildNodes[5]?.InnerText ?? "0";
                        string giaBanText = x.ChildNodes[6]?.InnerText ?? "0";
                        int giaNhap = 0, giaBan = 0;
                        int.TryParse(giaNhapText, out giaNhap);
                        int.TryParse(giaBanText, out giaBan);
                        
                        string tenDanhMuc = GetTenDanhMuc(x.ChildNodes[4]?.InnerText ?? "");
                        
                        dataGridViewKho.Rows.Add(
                            stt,                                      // STT
                            x.ChildNodes[0]?.InnerText ?? "",        // Mã SP
                            x.ChildNodes[1]?.InnerText ?? "",        // Tên SP  
                            tenDanhMuc,                              // Danh mục
                            x.ChildNodes[2]?.InnerText ?? "0",       // Số lượng
                            string.Format("{0:N0}", giaNhap),        // Giá nhập
                            string.Format("{0:N0}", giaBan),         // Giá bán
                            x.ChildNodes[3]?.InnerText ?? ""         // Chi tiết
                        );
                    }
                    catch (Exception nodeEx)
                    {
                        Console.WriteLine($"Lỗi khi xử lý node {stt}: {nodeEx.Message}");
                        MessageBox.Show($"Lỗi khi xử lý sản phẩm {stt}: {nodeEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message + "\n" + ex.StackTrace, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhMuc()
        {
            try
            {
                comboBoxDanhMuc.Items.Clear();
                XmlDocument docDM = XmlFile.getXmlDocument("DanhMucSanPhams.xml");
                XmlNodeList nodeListDM = docDM.SelectNodes("/DanhMucSanPhams/DanhMucSanPham");
                
                foreach (XmlNode x in nodeListDM)
                {
                    comboBoxDanhMuc.Items.Add(x.ChildNodes[1].InnerText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetTenDanhMuc(string maDM)
        {
            try
            {
                if (string.IsNullOrEmpty(maDM))
                    return "Không xác định";
                    
                if (!System.IO.File.Exists("DanhMucSanPhams.xml"))
                    return "Không xác định";
                    
                XmlDocument docDM = XmlFile.getXmlDocument("DanhMucSanPhams.xml");
                if (docDM == null)
                    return "Không xác định";
                    
                XmlNodeList nodeListDM = docDM.SelectNodes($"/DanhMucSanPhams/DanhMucSanPham[maDM='{maDM}']");
                
                if (nodeListDM != null && nodeListDM.Count > 0 && nodeListDM[0].ChildNodes.Count > 1)
                    return nodeListDM[0].ChildNodes[1].InnerText ?? "Không xác định";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi GetTenDanhMuc: {ex.Message}");
            }
            
            return "Không xác định";
        }

        private string GetMaDanhMuc(string tenDM)
        {
            try
            {
                XmlDocument docDM = XmlFile.getXmlDocument("DanhMucSanPhams.xml");
                XmlNodeList nodeListDM = docDM.SelectNodes($"/DanhMucSanPhams/DanhMucSanPham[tenDM='{tenDM}']");
                
                if (nodeListDM.Count > 0)
                    return nodeListDM[0].ChildNodes[0].InnerText;
            }
            catch { }
            
            return "";
        }

        private void dataGridViewKho_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các control đã được khởi tạo chưa
                if (textBoxMaSP == null || textBoxTenSP == null || comboBoxDanhMuc == null || 
                    textBoxSoLuong == null || textBoxGiaNhap == null || textBoxGia == null || textBoxChiTiet == null)
                {
                    return;
                }
                
                if (dataGridViewKho.CurrentRow != null && dataGridViewKho.CurrentRow.Index >= 0)
                {
                    selectedRowIndex = dataGridViewKho.CurrentRow.Index;
                    DataGridViewRow row = dataGridViewKho.CurrentRow;
                    
                    // Kiểm tra row có đủ cells không (8 cột: 0-7)
                    if (row.Cells.Count < 8)
                    {
                        Console.WriteLine($"Row không đủ cells: {row.Cells.Count}");
                        return;
                    }
                    
                    textBoxMaSP.Text = row.Cells[1].Value?.ToString() ?? "";      // Cột 1: Mã SP
                    textBoxTenSP.Text = row.Cells[2].Value?.ToString() ?? "";     // Cột 2: Tên SP
                    comboBoxDanhMuc.Text = row.Cells[3].Value?.ToString() ?? "";  // Cột 3: Danh mục
                    textBoxSoLuong.Text = row.Cells[4].Value?.ToString() ?? "";   // Cột 4: Số lượng
                    
                    // Xử lý giá nhập - loại bỏ tất cả ký tự định dạng
                    string giaNhapText = row.Cells[5].Value?.ToString() ?? "";    
                    string giaNhapClean = giaNhapText.Replace(",", "").Replace(".", "").Replace(" ", "");
                    textBoxGiaNhap.Text = giaNhapClean;
                    
                    // Xử lý giá bán - loại bỏ tất cả ký tự định dạng  
                    string giaBanText = row.Cells[6].Value?.ToString() ?? "";     
                    string giaBanClean = giaBanText.Replace(",", "").Replace(".", "").Replace(" ", "");
                    textBoxGia.Text = giaBanClean;
                    
                    textBoxChiTiet.Text = row.Cells[7].Value?.ToString() ?? "";   // Cột 7: Chi tiết
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi chọn dòng: " + ex.Message);
            }
        }

        private void buttonCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedRowIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput())
                    return;

                string maSP = textBoxMaSP.Text.Trim();
                XDoc = XmlFile.getXmlDocument("ChiTietSanPhams.xml");
                XmlNodeList nodeList = XDoc.SelectNodes($"/ChiTietSanPhams/ChiTietSanPham[maSP='{maSP}']");
                
                if (nodeList.Count > 0)
                {
                    XmlNode node = nodeList[0];
                    node.ChildNodes[1].InnerText = textBoxTenSP.Text.Trim();
                    node.ChildNodes[2].InnerText = textBoxSoLuong.Text.Trim();
                    node.ChildNodes[3].InnerText = textBoxChiTiet.Text.Trim();
                    node.ChildNodes[4].InnerText = GetMaDanhMuc(comboBoxDanhMuc.Text);
                    node.ChildNodes[5].InnerText = textBoxGiaNhap.Text.Trim(); // Lưu giá nhập
                    node.ChildNodes[6].InnerText = textBoxGia.Text.Trim();     // Lưu giá bán
                    
                    XDoc.Save("ChiTietSanPhams.xml");
                    LoadDataToGrid();
                    ClearInputs();
                    
                    // Refresh Form4 nếu đang mở
                    RefreshForm4IfOpen();
                    
                    MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedRowIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    string maSP = textBoxMaSP.Text.Trim();
                    XDoc = XmlFile.getXmlDocument("ChiTietSanPhams.xml");
                    XmlNodeList nodeList = XDoc.SelectNodes($"/ChiTietSanPhams/ChiTietSanPham[maSP='{maSP}']");
                    
                    if (nodeList.Count > 0)
                    {
                        XmlNode nodeToRemove = nodeList[0];
                        nodeToRemove.ParentNode.RemoveChild(nodeToRemove);
                        
                        XDoc.Save("ChiTietSanPhams.xml");
                        LoadDataToGrid();
                        ClearInputs();
                        
                        // Refresh Form4 nếu đang mở
                        RefreshForm4IfOpen();
                        
                        MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxTenSP.Focus();
                return false;
            }

            if (comboBoxDanhMuc.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxDanhMuc.Focus();
                return false;
            }

            if (!int.TryParse(textBoxSoLuong.Text, out int soLuong) || soLuong < 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên không âm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxSoLuong.Focus();
                return false;
            }

            // Kiểm tra ít nhất một trong hai giá phải được nhập
            bool hasGiaNhap = !string.IsNullOrWhiteSpace(textBoxGiaNhap.Text);
            bool hasGiaBan = !string.IsNullOrWhiteSpace(textBoxGia.Text);
            
            if (!hasGiaNhap && !hasGiaBan)
            {
                MessageBox.Show("Vui lòng nhập ít nhất một trong hai: Giá nhập hoặc Giá bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxGiaNhap.Focus();
                return false;
            }

            int giaNhap = 0, giaBan = 0;
            
            // Validate giá nhập nếu có nhập
            if (hasGiaNhap)
            {
                string giaNhapClean = textBoxGiaNhap.Text.Replace(",", "").Replace(".", "").Trim();
                if (!int.TryParse(giaNhapClean, out giaNhap) || giaNhap <= 0)
                {
                    MessageBox.Show($"Giá nhập không hợp lệ: '{textBoxGiaNhap.Text}'\nVui lòng nhập số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxGiaNhap.Focus();
                    textBoxGiaNhap.SelectAll();
                    return false;
                }
                // Cập nhật lại textbox với giá trị đã clean
                textBoxGiaNhap.Text = giaNhap.ToString();
            }

            // Validate giá bán nếu có nhập
            if (hasGiaBan)
            {
                string giaBanClean = textBoxGia.Text.Replace(",", "").Replace(".", "").Trim();
                if (!int.TryParse(giaBanClean, out giaBan) || giaBan <= 0)
                {
                    MessageBox.Show($"Giá bán không hợp lệ: '{textBoxGia.Text}'\nVui lòng nhập số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxGia.Focus();
                    textBoxGia.SelectAll();
                    return false;
                }
                // Cập nhật lại textbox với giá trị đã clean
                textBoxGia.Text = giaBan.ToString();
            }

            // Tự động tính giá còn thiếu nếu chỉ nhập một giá
            if (hasGiaNhap && !hasGiaBan)
            {
                giaBan = (int)(giaNhap * 1.25); // Giá bán = 125% giá nhập (lãi 25%)
                textBoxGia.Text = giaBan.ToString();
                MessageBox.Show($"Đã tự động tính giá bán: {giaBan:N0} VND (lãi 25%)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (hasGiaBan && !hasGiaNhap)
            {
                giaNhap = (int)(giaBan * 0.8); // Giá nhập = 80% giá bán (lãi 25%)
                textBoxGiaNhap.Text = giaNhap.ToString();
                MessageBox.Show($"Đã tự động tính giá nhập: {giaNhap:N0} VND (lãi 25%)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Kiểm tra ràng buộc: giá nhập < giá bán (khi cả hai đều có giá trị)
            if (hasGiaNhap && hasGiaBan && giaNhap >= giaBan)
            {
                DialogResult result = MessageBox.Show(
                    $"Giá nhập ({giaNhap:N0} VND) phải nhỏ hơn giá bán ({giaBan:N0} VND)!\n\n" +
                    "Bạn muốn:\n" +
                    "• YES: Tự động điều chỉnh giá nhập = 80% giá bán\n" +
                    "• NO: Tự động điều chỉnh giá bán = 125% giá nhập\n" +
                    "• CANCEL: Tự nhập lại",
                    "Xung đột giá - Chọn cách xử lý", 
                    MessageBoxButtons.YesNoCancel, 
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // Điều chỉnh giá nhập dựa trên giá bán
                    giaNhap = (int)(giaBan * 0.8);
                    textBoxGiaNhap.Text = giaNhap.ToString();
                    MessageBox.Show($"Đã điều chỉnh giá nhập thành: {giaNhap:N0} VND", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == DialogResult.No)
                {
                    // Điều chỉnh giá bán dựa trên giá nhập
                    giaBan = (int)(giaNhap * 1.25);
                    textBoxGia.Text = giaBan.ToString();
                    MessageBox.Show($"Đã điều chỉnh giá bán thành: {giaBan:N0} VND", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // User chọn Cancel - yêu cầu nhập lại
                    textBoxGiaNhap.Focus();
                    return false;
                }
            }
            
            // Kiểm tra lại ràng buộc sau khi tự động tính hoặc điều chỉnh
            if (giaNhap > 0 && giaBan > 0 && giaNhap >= giaBan)
            {
                MessageBox.Show($"Vẫn có lỗi: Giá nhập ({giaNhap:N0} VND) phải nhỏ hơn giá bán ({giaBan:N0} VND)!", "Lỗi ràng buộc giá", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxGiaNhap.Focus();
                return false;
            }

            return true;
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                // Tạo mã sản phẩm mới
                string maSPMoi = GenerateNewMaSP();
                
                XDoc = XmlFile.getXmlDocument("ChiTietSanPhams.xml");
                
                XmlElement newNode = XDoc.CreateElement("ChiTietSanPham");
                
                XmlElement maSP = XDoc.CreateElement("maSP");
                maSP.InnerText = maSPMoi;
                
                XmlElement tenSP = XDoc.CreateElement("tenSP");
                tenSP.InnerText = textBoxTenSP.Text.Trim();
                
                XmlElement soLuong = XDoc.CreateElement("soLuong");
                soLuong.InnerText = textBoxSoLuong.Text.Trim();
                
                XmlElement chiTiet = XDoc.CreateElement("chiTiet");
                chiTiet.InnerText = textBoxChiTiet.Text.Trim();
                
                XmlElement maDMSP = XDoc.CreateElement("maDMSP");
                maDMSP.InnerText = GetMaDanhMuc(comboBoxDanhMuc.Text);
                
                // Sử dụng giá nhập và giá bán từ textbox
                XmlElement giaNhapElement = XDoc.CreateElement("giaNhap");
                giaNhapElement.InnerText = textBoxGiaNhap.Text.Trim();
                
                XmlElement gia = XDoc.CreateElement("gia");
                gia.InnerText = textBoxGia.Text.Trim();
                
                newNode.AppendChild(maSP);
                newNode.AppendChild(tenSP);
                newNode.AppendChild(soLuong);
                newNode.AppendChild(chiTiet);
                newNode.AppendChild(maDMSP);
                newNode.AppendChild(giaNhapElement);
                newNode.AppendChild(gia);
                
                XDoc.DocumentElement.AppendChild(newNode);
                XDoc.Save("ChiTietSanPhams.xml");
                
                LoadDataToGrid();
                ClearInputs();
                
                // Refresh Form4 nếu đang mở
                RefreshForm4IfOpen();
                
                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateNewMaSP()
        {
            try
            {
                XDoc = XmlFile.getXmlDocument("ChiTietSanPhams.xml");
                XmlNodeList nodeList = XDoc.SelectNodes("/ChiTietSanPhams/ChiTietSanPham");
                
                int maxNumber = 0;
                foreach (XmlNode node in nodeList)
                {
                    string maSP = node.ChildNodes[0].InnerText;
                    if (maSP.StartsWith("SP"))
                    {
                        string numberPart = maSP.Substring(2);
                        if (int.TryParse(numberPart, out int number))
                        {
                            if (number > maxNumber)
                                maxNumber = number;
                        }
                    }
                }
                
                // Tạo mã SP với format 5 chữ số để khớp với tên file ảnh
                return "SP" + (maxNumber + 1).ToString("00000");
            }
            catch
            {
                return "SP00001";
            }
        }

        private void ClearInputs()
        {
            textBoxMaSP.Text = "";
            textBoxTenSP.Text = "";
            textBoxSoLuong.Text = "";
            textBoxGiaNhap.Text = "";
            textBoxGia.Text = "";
            textBoxChiTiet.Text = "";
            comboBoxDanhMuc.SelectedIndex = -1;
            selectedRowIndex = -1;
        }

        private void SetupTooltips()
        {
            try
            {
                ToolTip toolTip = new ToolTip();
                toolTip.SetToolTip(textBoxGiaNhap, "Nhập giá nhập (tùy chọn). Nếu để trống, sẽ tự động tính từ giá bán.");
                toolTip.SetToolTip(textBoxGia, "Nhập giá bán (tùy chọn). Nếu để trống, sẽ tự động tính từ giá nhập.");
                toolTip.SetToolTip(buttonThem, "Chỉ cần nhập một trong hai giá, hệ thống sẽ tự động tính giá còn lại");
                toolTip.SetToolTip(buttonCapNhat, "Cập nhật thông tin sản phẩm đã chọn");
                
                // Thêm event handlers để xử lý format số
                textBoxGiaNhap.Leave += TextBoxGia_Leave;
                textBoxGia.Leave += TextBoxGia_Leave;
                textBoxGiaNhap.Enter += TextBoxGia_Enter;
                textBoxGia.Enter += TextBoxGia_Enter;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi setup tooltips: " + ex.Message);
            }
        }

        private void TextBoxGia_Enter(object sender, EventArgs e)
        {
            // Khi focus vào textbox, loại bỏ format để dễ chỉnh sửa
            TextBox textBox = sender as TextBox;
            if (textBox != null && !string.IsNullOrEmpty(textBox.Text))
            {
                string cleanText = textBox.Text.Replace(",", "").Replace(".", "").Replace(" ", "");
                textBox.Text = cleanText;
            }
        }

        private void TextBoxGia_Leave(object sender, EventArgs e)
        {
            // Khi rời khỏi textbox, format lại số cho dễ đọc
            TextBox textBox = sender as TextBox;
            if (textBox != null && !string.IsNullOrEmpty(textBox.Text))
            {
                string cleanText = textBox.Text.Replace(",", "").Replace(".", "").Replace(" ", "");
                if (int.TryParse(cleanText, out int value) && value > 0)
                {
                    textBox.Text = value.ToString("N0");
                }
            }
        }

        private void RefreshForm4IfOpen()
        {
            try
            {
                // Tìm Form4 đang mở trong ứng dụng
                foreach (Form form in Application.OpenForms)
                {
                    if (form is Form4)
                    {
                        Form4 form4 = form as Form4;
                        form4.RefreshData();
                        Console.WriteLine("Đã refresh Form4 từ FormQLKho");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi refresh Form4: " + ex.Message);
            }
        }
    }
}