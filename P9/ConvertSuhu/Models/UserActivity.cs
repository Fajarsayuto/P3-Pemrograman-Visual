using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace ConvertSuhu.Models
{
    public class UserActivity
    {
        public static bool ValidateLogin(string email, string password, out int userId)
        {
            userId = -1;

            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT UserID FROM users WHERE Email = @Email AND PasswordHash = @PasswordHash";

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
                        Console.WriteLine("Login error: " + ex.Message);
                    }
                }
            }

            return false;
        }

        public static bool RegisterUser(string email, string username, string password)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = "INSERT INTO users (Username, Email, PasswordHash) VALUES (@Username, @Email, @PasswordHash)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", password); 

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Register error: " + ex.Message);
                        return false;
                    }
                }
            }
        }


        public static void SaveConversionHistory(int userId, double input, string from, string to, double result)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = @"INSERT INTO history (UserID, SuhuAwal, DariSatuan, KeSatuan, Hasil, TanggalWaktu) 
                                 VALUES (@UserID, @InputValue, @FromUnit, @ToUnit, @ResultValue, NOW())";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@InputValue", input);
                    cmd.Parameters.AddWithValue("@FromUnit", from);
                    cmd.Parameters.AddWithValue("@ToUnit", to);
                    cmd.Parameters.AddWithValue("@ResultValue", result);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error saving history: " + ex.Message);
                    }
                }
            }
        }

        public static void HapusRiwayat(int id)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM history WHERE UserId = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static DataTable GetConversionHistoryTable(int userId)
        {
            DataTable dt = new DataTable();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string query = "SELECT UserId, SuhuAwal AS 'Suhu Awal', DariSatuan AS 'Dari', KeSatuan AS 'Ke', Hasil AS 'Hasil', TanggalWaktu AS 'Tanggal & Waktu' FROM history WHERE UserId = @userId ORDER BY TanggalWaktu DESC" ;
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
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
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Update error: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public static User GetUserById(int userId)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM users WHERE UserID = @UserID";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    Username = reader["Username"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PasswordHash = reader["PasswordHash"].ToString()
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("GetUserById error: " + ex.Message);
                    }
                }
            }

            return null;
        }
    }
    }
