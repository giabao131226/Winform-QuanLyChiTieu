namespace QuanLyChiTieu
{
    partial class FormDangKy
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
            this.lblErrPass = new System.Windows.Forms.Label();
            this.lblErrEmail = new System.Windows.Forms.Label();
            this.lblErrUser = new System.Windows.Forms.Label();
            this.llblDangNhap = new System.Windows.Forms.LinkLabel();
            this.lblDaCoTaiKhoan = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.lblDangKyTaiKhoan = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblErrPass);
            this.panel1.Controls.Add(this.lblErrEmail);
            this.panel1.Controls.Add(this.lblErrUser);
            this.panel1.Controls.Add(this.llblDangNhap);
            this.panel1.Controls.Add(this.lblDaCoTaiKhoan);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.txtMatKhau);
            this.panel1.Controls.Add(this.txtTenDangNhap);
            this.panel1.Controls.Add(this.lblEmail);
            this.panel1.Controls.Add(this.lblMatKhau);
            this.panel1.Controls.Add(this.lblTenDangNhap);
            this.panel1.Controls.Add(this.btnDangKy);
            this.panel1.Controls.Add(this.lblDangKyTaiKhoan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 333);
            this.panel1.TabIndex = 3;
            // 
            // lblErrPass
            // 
            this.lblErrPass.AutoSize = true;
            this.lblErrPass.ForeColor = System.Drawing.Color.Red;
            this.lblErrPass.Location = new System.Drawing.Point(54, 181);
            this.lblErrPass.Name = "lblErrPass";
            this.lblErrPass.Size = new System.Drawing.Size(0, 13);
            this.lblErrPass.TabIndex = 16;
            // 
            // lblErrEmail
            // 
            this.lblErrEmail.AutoSize = true;
            this.lblErrEmail.ForeColor = System.Drawing.Color.Red;
            this.lblErrEmail.Location = new System.Drawing.Point(54, 252);
            this.lblErrEmail.Name = "lblErrEmail";
            this.lblErrEmail.Size = new System.Drawing.Size(0, 13);
            this.lblErrEmail.TabIndex = 16;
            // 
            // lblErrUser
            // 
            this.lblErrUser.AutoSize = true;
            this.lblErrUser.ForeColor = System.Drawing.Color.Red;
            this.lblErrUser.Location = new System.Drawing.Point(54, 111);
            this.lblErrUser.Name = "lblErrUser";
            this.lblErrUser.Size = new System.Drawing.Size(0, 13);
            this.lblErrUser.TabIndex = 16;
            // 
            // llblDangNhap
            // 
            this.llblDangNhap.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.llblDangNhap.AutoSize = true;
            this.llblDangNhap.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.llblDangNhap.Location = new System.Drawing.Point(188, 310);
            this.llblDangNhap.Name = "llblDangNhap";
            this.llblDangNhap.Size = new System.Drawing.Size(62, 13);
            this.llblDangNhap.TabIndex = 15;
            this.llblDangNhap.TabStop = true;
            this.llblDangNhap.Text = "Đăng Nhập";
            this.llblDangNhap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblDangNhap_LinkClicked);
            // 
            // lblDaCoTaiKhoan
            // 
            this.lblDaCoTaiKhoan.AutoSize = true;
            this.lblDaCoTaiKhoan.Font = new System.Drawing.Font("Arial", 8F);
            this.lblDaCoTaiKhoan.Location = new System.Drawing.Point(95, 310);
            this.lblDaCoTaiKhoan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDaCoTaiKhoan.Name = "lblDaCoTaiKhoan";
            this.lblDaCoTaiKhoan.Size = new System.Drawing.Size(88, 14);
            this.lblDaCoTaiKhoan.TabIndex = 13;
            this.lblDaCoTaiKhoan.Text = "Đã có tài khoản?";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(57, 227);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(243, 20);
            this.txtEmail.TabIndex = 10;
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(57, 159);
            this.txtMatKhau.Margin = new System.Windows.Forms.Padding(2);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(243, 20);
            this.txtMatKhau.TabIndex = 10;
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Location = new System.Drawing.Point(57, 87);
            this.txtTenDangNhap.Margin = new System.Windows.Forms.Padding(2);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(243, 20);
            this.txtTenDangNhap.TabIndex = 11;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(54, 210);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(41, 15);
            this.lblEmail.TabIndex = 9;
            this.lblEmail.Text = "Email:";
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatKhau.Location = new System.Drawing.Point(54, 140);
            this.lblMatKhau.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(62, 15);
            this.lblMatKhau.TabIndex = 9;
            this.lblMatKhau.Text = "Mật khẩu:";
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenDangNhap.Location = new System.Drawing.Point(54, 70);
            this.lblTenDangNhap.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(93, 15);
            this.lblTenDangNhap.TabIndex = 8;
            this.lblTenDangNhap.Text = "Tên đăng nhập:";
            // 
            // btnDangKy
            // 
            this.btnDangKy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.btnDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangKy.ForeColor = System.Drawing.Color.White;
            this.btnDangKy.Location = new System.Drawing.Point(132, 283);
            this.btnDangKy.Margin = new System.Windows.Forms.Padding(2);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(81, 25);
            this.btnDangKy.TabIndex = 7;
            this.btnDangKy.Text = "Đăng Ký";
            this.btnDangKy.UseVisualStyleBackColor = false;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // lblDangKyTaiKhoan
            // 
            this.lblDangKyTaiKhoan.AutoSize = true;
            this.lblDangKyTaiKhoan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDangKyTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(42)))), ((int)(((byte)(80)))));
            this.lblDangKyTaiKhoan.Location = new System.Drawing.Point(94, 37);
            this.lblDangKyTaiKhoan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDangKyTaiKhoan.Name = "lblDangKyTaiKhoan";
            this.lblDangKyTaiKhoan.Size = new System.Drawing.Size(157, 19);
            this.lblDangKyTaiKhoan.TabIndex = 6;
            this.lblDangKyTaiKhoan.Text = "Đăng Ký Tài Khoản";
            // 
            // FormDangKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 333);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormDangKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Ký";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDaCoTaiKhoan;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Label lblDangKyTaiKhoan;
        private System.Windows.Forms.LinkLabel llblDangNhap;
        private System.Windows.Forms.Label lblErrPass;
        private System.Windows.Forms.Label lblErrEmail;
        private System.Windows.Forms.Label lblErrUser;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.Label lblTenDangNhap;
    }
}