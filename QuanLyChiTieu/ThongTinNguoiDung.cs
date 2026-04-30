using Org.BouncyCastle.Asn1.X509;
using ShareddData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Validate;
namespace QuanLyChiTieu
{
    public partial class ThongTinNguoiDung : Form
    {

        KiemTra validate = new KiemTra();
        KiemTra.UserRespository userRespository = new KiemTra.UserRespository();
        AuthService authService = new AuthService();


        public ThongTinNguoiDung()
        {
            InitializeComponent();
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            lblUserNameError.Visible = false;
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            lblLoiSoDienThoai.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            lblEmailError.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Bạn có chắc muốn đăng xuất?",
               "Xác nhận",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // đóng form hiện tại
                this.Close();


            }
        }

        private bool KiemTraSoDienThoai(string sdt)
        {
            string pattern = @"^0\d{9}$";
            return Regex.IsMatch(sdt, pattern);
        }
        private bool KiemTraEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng cài đặt đang phát triển!");
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            string userName = txtTenHienThi.Text.Trim();
            string email = txtEmail.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();

            int loi = 0;
            //Validate cho các trường
            // validate username
            if (validate.validateUserName(userName) == false)
            {
                lblUserNameError.Text = "Tên đăng nhập phải bắt đầu\nbằng chữ và có ít nhất 6 ký tự (chỉ gồm chữ và số).";
                lblUserNameError.Visible = true;
                loi += 1;
            }
            //Phải viết check trùng
            if (userRespository.IsUserNameTaken(userName))
            {
                lblUserNameError.Text = "Tên đăng nhập đã tồn tại, vui lòng chọn tên khác.";
                lblUserNameError.Visible = true;
                loi += 1;
            }
            //Validate email
            if (validate.validateEmail(email) == false)
            {
                lblEmailError.Text = "Email không đúng định dạng (ví dụ: example@gmail.com).";
                lblEmailError.Visible = true;
                loi += 1;
            }
            if (userRespository.IsEmailTaken(email))
            {
                lblEmailError.Text = "Email đã tồn tại, vui lòng sử dụng email khác.";
                lblEmailError.Visible = true;
                loi += 1;
            }
            //Validate số điện thoại
            if (validate.validatePhoneNumber(sdt) == false)
            {
                lblLoiSoDienThoai.Text = "Số điện thoại phải bắt đầu bằng 0 và có 10 chữ số.";
                lblLoiSoDienThoai.Visible = true;
                loi += 1;
            }

            if (loi > 0) return;

            try
            {
                bool ok = authService.UpDateInformation(userName,email,sdt);
                if (ok)
                {
                    MessageBox.Show("Cập nhật thông tin thành công!");
                    this.Refresh();
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại!");
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }


        }

        private void ThongTinNguoiDung_Load_1(object sender, EventArgs e)
        {
            lblHoTen.Text = AppSession.userName;
            txtTenHienThi.Text = AppSession.userName;
            txtEmail.Text =AppSession.Email;
            txtSoDienThoai.Text = AppSession.phoneNumber;
        }

        private void btnViTien_Click(object sender, EventArgs e)
        {
            FormQuanLyChiTieu formQuanLyChiTieu = new FormQuanLyChiTieu();
            formQuanLyChiTieu.Show();
            this.Hide();
        }
    }
}
