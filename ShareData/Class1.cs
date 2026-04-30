using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ShareData
{
    public class AppSession
    {
        public static int UserId { get; set; }
        public static string userName { get; set; }
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
                conn.Open();

                string query = "SELECT Id, userName FROM TaiKhoan WHERE userName=@u AND passWord=@p";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    AppSession.UserId = reader.GetInt32(0);
                    AppSession.userName = reader.GetString(1);
                    return true;
                }
            }

            return false;
        }
    }
}
