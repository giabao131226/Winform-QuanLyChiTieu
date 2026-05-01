using System;
using System.Collections.Generic;
using System.Data;
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

    public class DanhMuc
    {
        public int Id { get; set; }
        public string TenDanhMuc { get; set; }
        public DateTime NgayTao { get; set; }
        public string Loai { get; set; }
        public decimal TienDuKien { get; set; }
        public int? IDGiaoDich { get; set; }
        public int IDBangDuKien { get; set; }
    }

    public class BangDuKienDL
    {
        private string connectionString =
        @"Server=.;Database=QuanLyChiTieu;Trusted_Connection=True;";

        public static int IdBangDuKien { get; set; }

        public BangDuKienDL()
        {
        }

        public bool createBangDuKien(string thang, string nam)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO BangDuKien (NgayTao, IDTaiKhoan) VALUES (@ngayTao, @userId);";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    DateTime now = DateTime.Now;
                    string ngay = now.Day.ToString("D2");
                    if (int.Parse(thang) < 10) thang = "0" + thang;

                    cmd.Parameters.AddWithValue("@ngayTao", DateTime.ParseExact(ngay + "/" + thang + "/" + nam, "dd/MM/yyyy", null));
                    cmd.Parameters.AddWithValue("@userId", AppSession.UserId);
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
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

        public List<DanhMuc> hienThiBangDuKien(string thang, string nam)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM DanhMuc WHERE IDBangDuKien = @idBangDuKien;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idBangDuKien", IdBangDuKien);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<DanhMuc> danhMucs = new List<DanhMuc>();
                    while (reader.Read())
                    {
                        DanhMuc dm = new DanhMuc
                        {
                            Id = reader.GetInt32(0),
                            TenDanhMuc = reader.GetString(1),
                            NgayTao = reader.GetDateTime(2),
                            Loai = reader.GetString(3),
                            TienDuKien = reader.GetDecimal(4),
                            IDGiaoDich = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                            IDBangDuKien = reader.GetInt32(6)
                        };
                        danhMucs.Add(dm);
                    }
                    return danhMucs;

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

        public bool isTakenDuKien(string thang, string nam)
        {
            using (SqlConnection conn = new SqlConnection(@"Server=.;Database=QuanLyChiTieu;Trusted_Connection=True;"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM BangDuKien WHERE MONTH(NgayTao) = @thang AND YEAR(NgayTao) = @nam AND IDTaiKhoan = @userId;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@thang", int.Parse(thang));
                    cmd.Parameters.AddWithValue("@nam", int.Parse(nam));
                    cmd.Parameters.AddWithValue("@userId", AppSession.UserId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        IdBangDuKien = reader.GetInt32(0);
                        count++;
                    }
                    return count > 0;
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

        public bool addCategory(string tenDanhMuc, string loaiGiaoDich, decimal soTien, string ngayTao)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO DanhMuc (tenDanhMuc, NgayTao, Loai, tienDuKien, IDGiaoDich, IDBangDuKien) VALUES (@tenDanhMuc, @ngayTao, @loaiGiaoDich, @soTien, NULL, @idBangDuKien);";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", AppSession.UserId);
                    cmd.Parameters.AddWithValue("@tenDanhMuc", tenDanhMuc);
                    cmd.Parameters.AddWithValue("@loaiGiaoDich", loaiGiaoDich);
                    cmd.Parameters.AddWithValue("@soTien", soTien);
                    cmd.Parameters.AddWithValue("@ngayTao", DateTime.ParseExact(ngayTao, "dd/MM/yyyy", null));
                    cmd.Parameters.AddWithValue("@idBangDuKien", IdBangDuKien);
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
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

        public bool removeCategory(int idDanhMuc)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM DanhMuc WHERE ID = @idDanhMuc";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idDanhMuc", idDanhMuc);
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
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
    }
}
    

