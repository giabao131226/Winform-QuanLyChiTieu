using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Validate
{
    public class KiemTra
    {
        public KiemTra() { }

        public bool validateUserName(string userName)
        {
            return Regex.IsMatch(userName, @"^[a-zA-Z][a-zA-Z0-9]{5,}$");
        }

        public bool validatePassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$");
        }

        public bool validateEmail(string email)
        {
            return Regex.IsMatch(email, @"^[\w.-]+@[\w.-]+\.[a-zA-Z]{2,}$");
        }

        public bool validatePhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^(0|\+84)(3|5|7|8|9)[0-9]{8}$");
        }

        public class UserRespository
        {
            private string connectionString =
                @"Server=.;Database=QuanLyChiTieu;Trusted_Connection=True;";
            public bool IsUserNameTaken(string userName)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM TaiKhoan WHERE userName=@u";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@u", userName);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            public bool IsEmailTaken(string email)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM TaiKhoan WHERE email=@e";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@e", email);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
