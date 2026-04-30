using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShareddData;
using Validate;

namespace QuanLyChiTieu
{
    public partial class DangNhap : Form
    {
        
        AuthService authService = new AuthService();
        KiemTra validate = new KiemTra();

        public DangNhap()
        {
            InitializeComponent();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnDangNhap;
            //llblDangKy.LinkVisited = false; //Tắt VisitedLinkColor -> Chậm
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {

            string userName = txtTenDangNhap.Text.Trim();
            string passWord = txtMatKhau.Text.Trim();

            // reset lỗi
            lblErrUser.Text = "";
            lblErrPass.Text = "";

            bool valid = true;

            // validate username
            if (validate.validateUserName(userName) == false)
            {
                lblErrUser.Text = "Tên đăng nhập phải bắt đầu\nbằng chữ và có ít nhất 6 ký tự (chỉ gồm chữ và số).";
                valid = false;
            }

            // validate password
            if (validate.validatePassword(passWord) == false)
            {
                lblErrPass.Text = "Mật khẩu phải có ít nhất 6 ký tự\ngồm chữ hoa, chữ thường, số và ký tự đặc biệt.";
                //valid = false;
            }

            if (!valid) return;

            // kiểm tra login
            try
            {
                bool ok = authService.Login(userName, passWord);
                if (ok)
                {
                    MessageBox.Show("Đăng nhập thành công");
                    FormQuanLyChiTieu f = new FormQuanLyChiTieu();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {
            lblErrUser.Text = "";
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            lblErrPass.Text = "";
        }

        private void llblDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormDangKy frmDangKy = new FormDangKy();
            frmDangKy.Show();
            this.Hide();
        }

       
    }
}
