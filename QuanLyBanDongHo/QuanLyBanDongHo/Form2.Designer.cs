namespace QuanLyBanDongHo
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonQLKhachHang = new System.Windows.Forms.Button();
            this.buttonQLKho = new System.Windows.Forms.Button();
            this.buttonLSThanhToan = new System.Windows.Forms.Button();
            this.buttonCapNhat = new System.Windows.Forms.Button();
            this.buttonXuatNS = new System.Windows.Forms.Button();
            this.buttonNhapNS = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panel1.Controls.Add(this.panelContent);
            this.panel1.Controls.Add(this.panelSidebar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 600);
            this.panel1.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.White;
            this.panelContent.Controls.Add(this.labelWelcome);
            this.panelContent.Controls.Add(this.labelSubtitle);
            this.panelContent.Location = new System.Drawing.Point(280, 20);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(900, 560);
            this.panelContent.TabIndex = 10;
            this.panelContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContent_Paint);
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.labelWelcome.Location = new System.Drawing.Point(200, 200);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(500, 62);
            this.labelWelcome.TabIndex = 0;
            this.labelWelcome.Text = "CHÀO MỪNG ĐẾN VỚI";
            this.labelWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.labelSubtitle.Location = new System.Drawing.Point(280, 280);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(340, 41);
            this.labelSubtitle.TabIndex = 1;
            this.labelSubtitle.Text = "HỆ THỐNG QUẢN LÝ ĐỒNG HỒ";
            this.labelSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panelSidebar.Controls.Add(this.labelTitle);
            this.panelSidebar.Controls.Add(this.buttonQLKhachHang);
            this.panelSidebar.Controls.Add(this.buttonQLKho);
            this.panelSidebar.Controls.Add(this.buttonLSThanhToan);
            this.panelSidebar.Controls.Add(this.buttonCapNhat);
            this.panelSidebar.Controls.Add(this.buttonXuatNS);
            this.panelSidebar.Controls.Add(this.buttonNhapNS);
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(260, 600);
            this.panelSidebar.TabIndex = 9;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(30, 30);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(200, 37);
            this.labelTitle.TabIndex = 7;
            this.labelTitle.Text = "WATCH STORE";
            // 
            // buttonQLKhachHang
            // 
            this.buttonQLKhachHang.BackColor = System.Drawing.Color.Transparent;
            this.buttonQLKhachHang.FlatAppearance.BorderSize = 0;
            this.buttonQLKhachHang.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.buttonQLKhachHang.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.buttonQLKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonQLKhachHang.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonQLKhachHang.ForeColor = System.Drawing.Color.White;
            this.buttonQLKhachHang.Location = new System.Drawing.Point(0, 340);
            this.buttonQLKhachHang.Margin = new System.Windows.Forms.Padding(4);
            this.buttonQLKhachHang.Name = "buttonQLKhachHang";
            this.buttonQLKhachHang.Size = new System.Drawing.Size(260, 60);
            this.buttonQLKhachHang.TabIndex = 6;
            this.buttonQLKhachHang.Text = "👥 Quản lý khách hàng";
            this.buttonQLKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonQLKhachHang.UseVisualStyleBackColor = false;
            this.buttonQLKhachHang.Click += new System.EventHandler(this.buttonQLKhachHang_Click);
            // 
            // buttonQLKho
            // 
            this.buttonQLKho.BackColor = System.Drawing.Color.Transparent;
            this.buttonQLKho.FlatAppearance.BorderSize = 0;
            this.buttonQLKho.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.buttonQLKho.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.buttonQLKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonQLKho.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonQLKho.ForeColor = System.Drawing.Color.White;
            this.buttonQLKho.Location = new System.Drawing.Point(0, 280);
            this.buttonQLKho.Margin = new System.Windows.Forms.Padding(4);
            this.buttonQLKho.Name = "buttonQLKho";
            this.buttonQLKho.Size = new System.Drawing.Size(260, 60);
            this.buttonQLKho.TabIndex = 7;
            this.buttonQLKho.Text = "🏪 Quản lý kho";
            this.buttonQLKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonQLKho.UseVisualStyleBackColor = false;
            this.buttonQLKho.Click += new System.EventHandler(this.buttonQLKho_Click);
            // 
            // buttonLSThanhToan
            // 
            this.buttonLSThanhToan.BackColor = System.Drawing.Color.Transparent;
            this.buttonLSThanhToan.FlatAppearance.BorderSize = 0;
            this.buttonLSThanhToan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.buttonLSThanhToan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.buttonLSThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLSThanhToan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLSThanhToan.ForeColor = System.Drawing.Color.White;
            this.buttonLSThanhToan.Location = new System.Drawing.Point(0, 460);
            this.buttonLSThanhToan.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLSThanhToan.Name = "buttonLSThanhToan";
            this.buttonLSThanhToan.Size = new System.Drawing.Size(260, 60);
            this.buttonLSThanhToan.TabIndex = 5;
            this.buttonLSThanhToan.Text = "📋 Lịch sử thanh toán";
            this.buttonLSThanhToan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLSThanhToan.UseVisualStyleBackColor = false;
            this.buttonLSThanhToan.Click += new System.EventHandler(this.buttonLSThanhToan_Click);
            // 
            // buttonCapNhat
            // 
            this.buttonCapNhat.BackColor = System.Drawing.Color.Transparent;
            this.buttonCapNhat.FlatAppearance.BorderSize = 0;
            this.buttonCapNhat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.buttonCapNhat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.buttonCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCapNhat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCapNhat.ForeColor = System.Drawing.Color.White;
            this.buttonCapNhat.Location = new System.Drawing.Point(0, 400);
            this.buttonCapNhat.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCapNhat.Name = "buttonCapNhat";
            this.buttonCapNhat.Size = new System.Drawing.Size(260, 60);
            this.buttonCapNhat.TabIndex = 4;
            this.buttonCapNhat.Text = "💾 Sao lưu dữ liệu";
            this.buttonCapNhat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCapNhat.UseVisualStyleBackColor = false;
            this.buttonCapNhat.Click += new System.EventHandler(this.buttonCapNhat_Click);
            // 
            // buttonXuatNS
            // 
            this.buttonXuatNS.BackColor = System.Drawing.Color.Transparent;
            this.buttonXuatNS.FlatAppearance.BorderSize = 0;
            this.buttonXuatNS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.buttonXuatNS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.buttonXuatNS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonXuatNS.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonXuatNS.ForeColor = System.Drawing.Color.White;
            this.buttonXuatNS.Location = new System.Drawing.Point(0, 220);
            this.buttonXuatNS.Margin = new System.Windows.Forms.Padding(4);
            this.buttonXuatNS.Name = "buttonXuatNS";
            this.buttonXuatNS.Size = new System.Drawing.Size(260, 60);
            this.buttonXuatNS.TabIndex = 2;
            this.buttonXuatNS.Text = "💳 Thanh toán";
            this.buttonXuatNS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonXuatNS.UseVisualStyleBackColor = false;
            this.buttonXuatNS.Click += new System.EventHandler(this.buttonXuatNS_Click);
            // 
            // buttonNhapNS
            // 
            this.buttonNhapNS.BackColor = System.Drawing.Color.Transparent;
            this.buttonNhapNS.FlatAppearance.BorderSize = 0;
            this.buttonNhapNS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.buttonNhapNS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.buttonNhapNS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNhapNS.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNhapNS.ForeColor = System.Drawing.Color.White;
            this.buttonNhapNS.Location = new System.Drawing.Point(0, 160);
            this.buttonNhapNS.Margin = new System.Windows.Forms.Padding(4);
            this.buttonNhapNS.Name = "buttonNhapNS";
            this.buttonNhapNS.Size = new System.Drawing.Size(260, 60);
            this.buttonNhapNS.TabIndex = 1;
            this.buttonNhapNS.Text = "📦 Nhập hàng";
            this.buttonNhapNS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNhapNS.UseVisualStyleBackColor = false;
            this.buttonNhapNS.Click += new System.EventHandler(this.buttonNhapNS_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Watch Store - Hệ thống quản lý";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonQLKhachHang;
        private System.Windows.Forms.Button buttonQLKho;
        private System.Windows.Forms.Button buttonLSThanhToan;
        private System.Windows.Forms.Button buttonCapNhat;
        private System.Windows.Forms.Button buttonXuatNS;
        private System.Windows.Forms.Button buttonNhapNS;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Label labelSubtitle;
    }
}