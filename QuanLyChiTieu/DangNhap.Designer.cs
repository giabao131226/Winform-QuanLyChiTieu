namespace QuanLyChiTieu
{
    partial class DangNhap
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
            this.llblDangKy = new System.Windows.Forms.LinkLabel();
            this.lblErrPass = new System.Windows.Forms.Label();
            this.lblErrUser = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.lblChuaCoTaiKhoan = new System.Windows.Forms.Label();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.lblDangNhapHeThong = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.llblDangKy);
            this.panel1.Controls.Add(this.lblErrPass);
            this.panel1.Controls.Add(this.lblErrUser);
            this.panel1.Controls.Add(this.txtMatKhau);
            this.panel1.Controls.Add(this.txtTenDangNhap);
            this.panel1.Controls.Add(this.lblMatKhau);
            this.panel1.Controls.Add(this.lblChuaCoTaiKhoan);
            this.panel1.Controls.Add(this.lblTenDangNhap);
            this.panel1.Controls.Add(this.btnDangNhap);
            this.panel1.Controls.Add(this.lblDangNhapHeThong);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(334, 367);
            this.panel1.TabIndex = 1;
            // 
            // llblDangKy
            // 
            this.llblDangKy.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.llblDangKy.AutoSize = true;
            this.llblDangKy.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.llblDangKy.Location = new System.Drawing.Point(178, 290);
            this.llblDangKy.Name = "llblDangKy";
            this.llblDangKy.Size = new System.Drawing.Size(76, 13);
            this.llblDangKy.TabIndex = 14;
            this.llblDangKy.TabStop = true;
            this.llblDangKy.Text = "Đăng Ký Ngay";
            this.llblDangKy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblDangKy_LinkClicked);
            // 
            // lblErrPass
            // 
            this.lblErrPass.AutoSize = true;
            this.lblErrPass.ForeColor = System.Drawing.Color.Red;
            this.lblErrPass.Location = new System.Drawing.Point(63, 218);
            this.lblErrPass.Name = "lblErrPass";
            this.lblErrPass.Size = new System.Drawing.Size(0, 13);
            this.lblErrPass.TabIndex = 13;
            // 
            // lblErrUser
            // 
            this.lblErrUser.AutoSize = true;
            this.lblErrUser.ForeColor = System.Drawing.Color.Red;
            this.lblErrUser.Location = new System.Drawing.Point(63, 151);
            this.lblErrUser.Name = "lblErrUser";
            this.lblErrUser.Size = new System.Drawing.Size(0, 13);
            this.lblErrUser.TabIndex = 13;
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(55, 193);
            this.txtMatKhau.Margin = new System.Windows.Forms.Padding(2);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(226, 20);
            this.txtMatKhau.TabIndex = 10;
            this.txtMatKhau.TextChanged += new System.EventHandler(this.txtMatKhau_TextChanged);
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Location = new System.Drawing.Point(55, 126);
            this.txtTenDangNhap.Margin = new System.Windows.Forms.Padding(2);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(226, 20);
            this.txtTenDangNhap.TabIndex = 11;
            this.txtTenDangNhap.TextChanged += new System.EventHandler(this.txtTenDangNhap_TextChanged);
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatKhau.Location = new System.Drawing.Point(53, 177);
            this.lblMatKhau.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(62, 15);
            this.lblMatKhau.TabIndex = 9;
            this.lblMatKhau.Text = "Mật khẩu:";
            // 
            // lblChuaCoTaiKhoan
            // 
            this.lblChuaCoTaiKhoan.AutoSize = true;
            this.lblChuaCoTaiKhoan.Font = new System.Drawing.Font("Arial", 8F);
            this.lblChuaCoTaiKhoan.Location = new System.Drawing.Point(80, 290);
            this.lblChuaCoTaiKhoan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChuaCoTaiKhoan.Name = "lblChuaCoTaiKhoan";
            this.lblChuaCoTaiKhoan.Size = new System.Drawing.Size(103, 14);
            this.lblChuaCoTaiKhoan.TabIndex = 8;
            this.lblChuaCoTaiKhoan.Text = "Chưa có tài khoản? ";
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenDangNhap.Location = new System.Drawing.Point(53, 110);
            this.lblTenDangNhap.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(93, 15);
            this.lblTenDangNhap.TabIndex = 8;
            this.lblTenDangNhap.Text = "Tên đăng nhập:";
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.btnDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangNhap.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnDangNhap.Location = new System.Drawing.Point(103, 256);
            this.btnDangNhap.Margin = new System.Windows.Forms.Padding(2);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(105, 25);
            this.btnDangNhap.TabIndex = 7;
            this.btnDangNhap.Text = "Đăng Nhập";
            this.btnDangNhap.UseVisualStyleBackColor = false;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // lblDangNhapHeThong
            // 
            this.lblDangNhapHeThong.AutoSize = true;
            this.lblDangNhapHeThong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDangNhapHeThong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(42)))), ((int)(((byte)(80)))));
            this.lblDangNhapHeThong.Location = new System.Drawing.Point(80, 66);
            this.lblDangNhapHeThong.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDangNhapHeThong.Name = "lblDangNhapHeThong";
            this.lblDangNhapHeThong.Size = new System.Drawing.Size(174, 19);
            this.lblDangNhapHeThong.TabIndex = 6;
            this.lblDangNhapHeThong.Text = "Đăng Nhập Hệ Thống";
            // 
            // DangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 367);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "DangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Nhập";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Label lblDangNhapHeThong;
        private System.Windows.Forms.Label lblChuaCoTaiKhoan;
        private System.Windows.Forms.Label lblErrPass;
        private System.Windows.Forms.Label lblErrUser;
        private System.Windows.Forms.LinkLabel llblDangKy;

    }

}
    
