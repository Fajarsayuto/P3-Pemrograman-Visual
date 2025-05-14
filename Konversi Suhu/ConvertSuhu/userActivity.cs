using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace ConvertSuhu
{
    internal class userActivity
    {
        public static bool RegisterUser(string email, string username, string password)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO Users (Email, Username, PasswordHash) VALUES (@Email, @Username, @Password)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saat registrasi: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public static bool ValidateLogin(string email, string password, out int userId)
        {
            userId = -1;

            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT UserID FROM Users WHERE Email = @Email AND PasswordHash = @PasswordHash";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", password);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            userId = Convert.ToInt32(result);
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal saat login: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return false;
        }

        public static bool UpdateUser(int userId, string username, string email, string passwordHash)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE users SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash WHERE UserID = @UserID";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal memperbarui data: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public static void Simpan(int userId, double suhuAwal, string dariSatuan, string keSatuan, double hasil)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO history (UserID, SuhuAwal, DariSatuan, KeSatuan, Hasil) " +
                               "VALUES (@UserID, @SuhuAwal, @DariSatuan, @KeSatuan, @Hasil)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@SuhuAwal", suhuAwal);
                    cmd.Parameters.AddWithValue("@DariSatuan", dariSatuan);
                    cmd.Parameters.AddWithValue("@KeSatuan", keSatuan);
                    cmd.Parameters.AddWithValue("@Hasil", hasil);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menyimpan history: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public static DataTable view(int userId)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT SuhuAwal, DariSatuan, KeSatuan, Hasil, TanggalWaktu FROM history WHERE UserID = @UserID ORDER BY TanggalWaktu DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    try
                    {
                        conn.Open();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Gagal mengambil data riwayat: " + ex.Message);
                    }
                }
            }

            return dt;
        }

        public static int Delete(int userId)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM history WHERE UserID = @UserID";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Gagal menghapus riwayat: " + ex.Message);
                    }
                }
            }
        }


    }
}
