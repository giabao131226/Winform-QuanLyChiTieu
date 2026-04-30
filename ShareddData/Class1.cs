using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareddData
{
    public class AppSession
    {
        public static int UserId { get; set; }
        public static string userName { get; set; }
        public static string Email { get; set; }

        public static string phoneNumber { get; set; }
        public static bool IsLoggedIn => UserId > 0;
    }

    public class AuthService
    {
        private string connectionString =
            @"Server=.;Database=QuanLyChiTieu;Trusted_Connection=True;";

        public bool Login(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT Id, userName,email FROM TaiKhoan WHERE userName=@u AND passWord=@p";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);

                    SqlDataReader reader = cmd.ExecuteReader();


                    if (reader.Read())
                    {
                        AppSession.UserId = reader.GetInt32(0);
                        AppSession.userName = reader.GetString(1);
                        AppSession.Email = reader.GetString(2);
                        //AppSession.phoneNumber = reader.GetString(3);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return false;
        }

        public bool SignUp(string username, string password, string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO TaiKhoan(userName, passWord,email) VALUES ( @username, @password,@email);";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@email", email);

                    int row = cmd.ExecuteNonQuery();
                    if (row > 0)
                    {
                        return true;
                    }
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public bool UpDateInformation(string username, string email, string phoneNumber)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE TaiKhoan SET userName=@username, email=@email, phoneNumber=@phoneNumber WHERE Id=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@id", AppSession.UserId);
                    int row = cmd.ExecuteNonQuery();
                    if (row > 0)
                    {
                        return true;
                    }
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return false;
        }
    }
}
