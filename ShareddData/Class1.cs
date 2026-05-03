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
            //return false;
        }

        
    }

    public class CategoryService {

        public string getIdByCategoryName(string tenDanhMuc)
        {
            using (SqlConnection conn = new SqlConnection(@"Server=.;Database=QuanLyChiTieu;Trusted_Connection=True;"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID FROM DanhMuc WHERE tenDanhMuc = @tenDanhMuc;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenDanhMuc", tenDanhMuc);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader.GetInt32(0).ToString();
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
            return null;
        }
        public List<DanhMuc> hienThiDanhMuc(string loai, string thang, string nam)
        {
            using (SqlConnection conn = new SqlConnection(@"Server=.;Database=QuanLyChiTieu;Trusted_Connection=True;"))
            {
                try
                {
                    List<DanhMuc> danhMucs = new List<DanhMuc>();
                    conn.Open();
                    
                    string query = "SELECT dm.* FROM TaiKhoan as tk,DanhMuc as dm,BangDuKien as bdk WHERE tk.ID = @userID AND tk.ID = bdk.IDTaiKhoan AND bdk.ID = dm.IDBangDuKien AND MONTH(dm.NgayTao) = @thang AND YEAR(dm.NgayTao) = @nam ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@thang", int.Parse(thang));
                    cmd.Parameters.AddWithValue("@nam", int.Parse(nam));
                    cmd.Parameters.AddWithValue("@userID", AppSession.UserId);
                    if (loai.Length > 0)
                    {
                        cmd.CommandText += "AND dm.Loai = @loai;";
                        cmd.Parameters.AddWithValue("@loai", loai);
                    }
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DanhMuc newData = new DanhMuc
                        {
                            Id = Convert.ToInt32(reader["ID"]),
                            TenDanhMuc = Convert.ToString(reader["TenDanhMuc"]),
                            NgayTao = Convert.ToDateTime(reader["NgayTao"]),
                            Loai = Convert.ToString(reader["Loai"]),
                            TienDuKien = Convert.ToDecimal(reader["TienDuKien"]),
                            TienThucChi = Convert.ToDecimal(reader["TienThucChi"]),
                            IDBangDuKien = Convert.ToInt32(reader["IDBangDuKien"])

                        };
                        danhMucs.Add(newData);
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

        public bool editCategory(int id,string tenDanhMuc,string loai,string ngayTao,decimal tienDuKien,decimal tienThucChi)
        {
            using(SqlConnection conn = new SqlConnection(@"Server=.;Database=QuanLyChiTieu;Trusted_Connection=True;"))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE DanhMuc SET tenDanhMuc = @tenDanhMuc ,Loai = @loai ,NgayTao = @ngayTao ,tienDuKien = @tienDuKien ,tienThucChi = @tienThucChi WHERE ID = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenDanhMuc", tenDanhMuc);
                    cmd.Parameters.AddWithValue("@loai", loai);
                    cmd.Parameters.AddWithValue("@ngayTao", DateTime.ParseExact(ngayTao,"dd/MM/yyyy",null));
                    cmd.Parameters.AddWithValue("@tienDuKien", tienDuKien);
                    cmd.Parameters.AddWithValue("@tienThucChi", tienThucChi);
                    cmd.Parameters.AddWithValue("@id", id);

                    int row = cmd.ExecuteNonQuery();
                    return row > 0;

                } catch (Exception ex) {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                
            }    
        }
    }

    public class DanhMuc
    {
        public int Id { get; set; }
        public string TenDanhMuc { get; set; }
        public DateTime NgayTao { get; set; }
        public string Loai { get; set; }
        public decimal TienDuKien { get; set; }
        public decimal TienThucChi { get; set; }
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
                            TienThucChi = reader.GetDecimal(5),
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
                    string query = "INSERT INTO DanhMuc (tenDanhMuc, NgayTao, Loai, tienDuKien,tienThucChi , IDBangDuKien) VALUES (@tenDanhMuc, @ngayTao, @loaiGiaoDich, @soTien, 0, @idBangDuKien);";
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

    public class Transition
    {
        public int Id { get; set; }
        public string TenGiaoDich { get; set; }
        public DateTime NgayTao { get; set; }
        public string Loai { get; set; }
        public decimal SoTien { get; set; }
        public int? IDDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public int IDTaiKhoan { get; set; }
    }
    public class TransitionService
    {
        public List<Transition> loadTransition(string thang, string nam,string loai,string textSearch)
        {
            using (SqlConnection conn = new SqlConnection(@"Server=.;Database=QuanLyChiTieu;Trusted_Connection=True;"))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT gd.*, dm.tenDanhMuc
                    FROM TaiKhoan as tk, GiaoDich as gd, DanhMuc as dm
                    WHERE tk.ID = @userID 
                    AND gd.IDDanhMuc = dm.ID 
                    AND tk.ID = gd.IDTaiKhoan 
                    AND MONTH(gd.NgayGiaoDich) = @thang 
                    AND YEAR(gd.NgayGiaoDich) = @nam";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@thang", int.Parse(thang));
                    cmd.Parameters.AddWithValue("@nam", int.Parse(nam));
                    cmd.Parameters.AddWithValue("@userID", AppSession.UserId);
                    
                    if(loai != "Tất cả" && loai!="")
                    {
                        cmd.CommandText += " AND gd.Loai = @loai";
                        cmd.Parameters.AddWithValue("@loai", loai);
                    }
                    if(!string.IsNullOrEmpty(textSearch))
                    {
                        cmd.CommandText += " AND gd.TenGiaoDich LIKE @textSearch";
                        cmd.Parameters.AddWithValue("@textSearch", "%" + textSearch + "%");
                    }
                    Console.WriteLine(cmd.CommandText);
                    Console.WriteLine(thang);
                    Console.WriteLine(nam);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Transition> transitions = new List<Transition>();
                    while (reader.Read())
                    {
                        Transition transition = new Transition
                        {
                            Id = Convert.ToInt32(reader["ID"]),
                            TenGiaoDich = Convert.ToString(reader["tenGiaoDich"]),
                            NgayTao = Convert.ToDateTime(reader["NgayGiaoDich"]),
                            Loai = Convert.ToString(reader["Loai"]),
                            SoTien = Convert.ToDecimal(reader["soTien"]),
                            IDDanhMuc = reader["IDDanhMuc"] != DBNull.Value
                    ? Convert.ToInt32(reader["IDDanhMuc"])
                    : (int?)null,
                            TenDanhMuc = reader["tenDanhMuc"] != DBNull.Value
                    ? Convert.ToString(reader["tenDanhMuc"])
                    : "",
                            IDTaiKhoan = Convert.ToInt32(reader["IDTaiKhoan"])
                        };
                        transitions.Add(transition);
                    }
                    
                    return transitions;
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

        public bool addTransition(string tenGiaoDich, string loai, decimal soTien, string ngayTao, string tenDanhMuc)
        {
            using (SqlConnection conn = new SqlConnection(@"Server=.;Database=QuanLyChiTieu;Trusted_Connection=True;"))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO GiaoDich (tenGiaoDich, NgayGiaoDich, soTien, Loai, IDTaiKhoan, IDDanhMuc) VALUES (@tenGiaoDich, @ngayTao, @soTien, @loai, @userID, (SELECT ID FROM DanhMuc WHERE tenDanhMuc = @tenDanhMuc));";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenGiaoDich", tenGiaoDich);
                    cmd.Parameters.AddWithValue("@loai", loai);
                    cmd.Parameters.AddWithValue("@soTien", soTien);
                    cmd.Parameters.AddWithValue("@ngayTao", DateTime.ParseExact(ngayTao, "dd/MM/yyyy", null));
                    cmd.Parameters.AddWithValue("@userID", AppSession.UserId);
                    cmd.Parameters.AddWithValue("@tenDanhMuc", tenDanhMuc);
                    int row = cmd.ExecuteNonQuery();
                    if (row > 0)
                    {
                        string updateTienThucChi = "UPDATE DanhMuc SET tienThucChi = tienThucChi + @soTien WHERE tenDanhMuc = @tenDanhMuc;";
                        SqlCommand updateCmd = new SqlCommand(updateTienThucChi, conn);
                        updateCmd.Parameters.AddWithValue("@soTien", soTien);
                        updateCmd.Parameters.AddWithValue("@tenDanhMuc", tenDanhMuc);
                        int check = updateCmd.ExecuteNonQuery();
                        return check > 0;
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

        public bool removeTransition(int id,string tenDanhMuc)
        {
            using (SqlConnection conn = new SqlConnection(@"Server=.;Database=QuanLyChiTieu;Trusted_Connection=True;"))
            {
                try
                {
                    conn.Open();
                    string query = @"DELETE GiaoDich WHERE ID = @id;";
                    string query2 = @"UPDATE DanhMuc
SET tienThucChi = tienThucChi - (SELECT soTien
								FROM GiaoDich WHERE ID = @id)
WHERE tenDanhMuc = @tenDanhMuc;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    cmd2.Parameters.AddWithValue("@id", id);
                    cmd2.Parameters.AddWithValue("@tenDanhMuc", tenDanhMuc);
                    cmd2.ExecuteNonQuery();
                    cmd.Parameters.AddWithValue("@id", id);
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

        public bool editTransition(int id,string tenGiaoDich,string loai, decimal soTien, string ngayTao, string tenDanhMuc)
        {
            using (SqlConnection conn = new SqlConnection(@"Server=.;Database=QuanLyChiTieu;Trusted_Connection=True;"))
            {
                try
                {
                    conn.Open();
                    string queryOldMoney = @"SELECT soTien FROM GiaoDich WHERE @id = id";
                    decimal oldMoney = 0;
                    string query = @"
                            UPDATE gd
                            SET 
                            gd.tenGiaoDich = @tenGiaoDich,
                            gd.Loai = @loai,
                            gd.soTien = @soTien,
                            gd.NgayGiaoDich = @ngayTao,
                            gd.IDDanhMuc = dm.ID
                            FROM GiaoDich gd
                            JOIN DanhMuc dm ON dm.tenDanhMuc = @tenDanhMuc
                            WHERE gd.ID = @id AND gd.IDTaiKhoan = @userID
                            ";
                
                    SqlCommand cmd2 = new SqlCommand(query, conn);
                    cmd2.Parameters.AddWithValue("@tenGiaoDich", tenGiaoDich);
                    cmd2.Parameters.AddWithValue("@loai", loai);
                    cmd2.Parameters.AddWithValue("@soTien", soTien);
                    cmd2.Parameters.AddWithValue("@ngayTao", DateTime.ParseExact(ngayTao,"dd/MM/yyyy",null));
                    cmd2.Parameters.AddWithValue("@tenDanhMuc", tenDanhMuc);
                    cmd2.Parameters.AddWithValue("@userID", AppSession.UserId);
                    cmd2.Parameters.AddWithValue("@id", id);
                    
                    SqlCommand cmd = new SqlCommand(queryOldMoney, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        oldMoney = Convert.ToDecimal(reader["soTien"]);
                        Console.WriteLine(oldMoney);
                        reader.Close();
                        queryOldMoney = @"UPDATE DanhMuc SET tienThucChi = tienThucChi + @soTien - @oldMoney WHERE tenDanhMuc = @tenDanhMuc";
                       
                        cmd = new SqlCommand (queryOldMoney, conn);
                        cmd.Parameters.AddWithValue("@soTien", soTien);
                        cmd.Parameters.AddWithValue("@oldMoney", oldMoney);
                        cmd.Parameters.AddWithValue("@tenDanhMuc", tenDanhMuc);
                        cmd.ExecuteNonQuery();
                    }
                    int row = cmd2.ExecuteNonQuery();
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
    

