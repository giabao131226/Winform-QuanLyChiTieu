using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShareddData;
using Validate;

namespace QuanLyChiTieu
{
    public partial class FormDangKy : Form
    {
        AuthService authService = new AuthService();
        KiemTra validate = new KiemTra();
        KiemTra.UserRespository userRespository = new KiemTra.UserRespository();

        public FormDangKy()
        {
            InitializeComponent();
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {

        }

        private void llblDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DangNhap frmDangNhap = new DangNhap();
            frmDangNhap.Show();
            this.Hide();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string u = txtTenDangNhap.Text.Trim();
            string p = txtMatKhau.Text.Trim();
            string email = txtEmail.Text.Trim();

            // reset lỗi
            lblErrUser.Text = "";
            lblErrPass.Text = "";
            lblErrEmail.Text = "";

            bool valid = true;

            // USERNAME
            if (validate.validateUserName(u) == false)
            {
                lblErrUser.Text = "Tên đăng nhập phải bắt đầu\nbằng chữ và có ít nhất 6 ký tự (chỉ gồm chữ và số).";
                valid = false;
            }
            //Phải viết check trùng
            if(userRespository.IsUserNameTaken(u))
            {
                lblErrUser.Text = "Tên đăng nhập đã tồn tại, vui lòng chọn tên khác.";
                valid = false;
            }
            // PASSWORD
            if (validate.validatePassword(p) == false)
            {
                lblErrPass.Text = "Mật khẩu phải có ít nhất 6 ký tự\ngồm chữ hoa, chữ thường, số và ký tự đặc biệt.";
                valid = false;
            }
           
            // EMAIL (chỉ chấp nhận @gmail.com
            if (validate.validateEmail(email) == false)
            {
                lblErrEmail.Text = "Email không đúng định dạng (ví dụ: example@gmail.com).";
                valid = false;
            }
            if(userRespository.IsEmailTaken(email))
            {
                lblErrEmail.Text = "Email đã tồn tại, vui lòng sử dụng email khác.";
                valid = false;
            }

            if (!valid)
            {
                return;
            }

            try
            {
                bool ok = authService.SignUp(u, p, email);
                if (ok)
                {
                    MessageBox.Show("Chúc Mừng Bạn Đã Đăng Ký Tài Khoản Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DangNhap formDangNhap = new DangNhap();
                    formDangNhap.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        


        //private void llblDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    this.Owner.Show();
        //    this.Close();
        //}

        //private void frmDangKy_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    this.Owner.Show();
        //}
    }
}
