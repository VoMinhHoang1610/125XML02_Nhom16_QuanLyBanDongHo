namespace QuanLyBanDongHo
{
    partial class FormSaoLuuData
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonXML_SQL = new System.Windows.Forms.Button();
            this.buttonKhoiPhuc = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 80);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(100, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "QUẢN LÝ CSDL - SAO LƯU";
            // 
            // buttonXML_SQL
            // 
            this.buttonXML_SQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.buttonXML_SQL.FlatAppearance.BorderSize = 0;
            this.buttonXML_SQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonXML_SQL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonXML_SQL.ForeColor = System.Drawing.Color.White;
            this.buttonXML_SQL.Location = new System.Drawing.Point(150, 120);
            this.buttonXML_SQL.Name = "buttonXML_SQL";
            this.buttonXML_SQL.Size = new System.Drawing.Size(300, 50);
            this.buttonXML_SQL.TabIndex = 2;
            this.buttonXML_SQL.Text = "💾 Sao lưu (XML - SQL)";
            this.buttonXML_SQL.UseVisualStyleBackColor = false;
            this.buttonXML_SQL.Click += new System.EventHandler(this.buttonXML_SQL_Click);
            // 
            // buttonKhoiPhuc
            // 
            this.buttonKhoiPhuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.buttonKhoiPhuc.FlatAppearance.BorderSize = 0;
            this.buttonKhoiPhuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonKhoiPhuc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonKhoiPhuc.ForeColor = System.Drawing.Color.White;
            this.buttonKhoiPhuc.Location = new System.Drawing.Point(150, 200);
            this.buttonKhoiPhuc.Name = "buttonKhoiPhuc";
            this.buttonKhoiPhuc.Size = new System.Drawing.Size(300, 50);
            this.buttonKhoiPhuc.TabIndex = 4;
            this.buttonKhoiPhuc.Text = "🔄 Khôi phục dữ liệu (SQL - XML)";
            this.buttonKhoiPhuc.UseVisualStyleBackColor = false;
            this.buttonKhoiPhuc.Click += new System.EventHandler(this.buttonKhoiPhuc_Click);
            // 
            // FormSaoLuuData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(600, 320);
            this.Controls.Add(this.buttonKhoiPhuc);
            this.Controls.Add(this.buttonXML_SQL);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormSaoLuuData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Watch Store - Sao lưu dữ liệu";
            this.Load += new System.EventHandler(this.FormSaoLuuData_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonXML_SQL;
        private System.Windows.Forms.Button buttonKhoiPhuc;
    }
}