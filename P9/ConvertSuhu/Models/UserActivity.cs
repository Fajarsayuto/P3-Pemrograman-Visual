using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ConvertSuhu.Models
{
    public class UserActivity
    {
        public bool ValidateLogin(string email, string password, out int userId)
        {
            userId = -1;

            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT UserID FROM users WHERE Email = @Email AND PasswordHash = @PasswordHash";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", password); // asumsi masih hash biasa

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

        public void Simpan(int userId, double input, string from, string to, double result)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = @"INSERT INTO history (UserID, InputValue, FromUnit, ToUnit, ResultValue, CreatedAt) 
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
                        Console.WriteLine("Error saving activity: " + ex.Message);
                    }
                }
            }
        }

        public List<string> GetHistory(int userId)
        {
            var historyList = new List<string>();

            using (MySqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT InputValue, FromUnit, ToUnit, ResultValue, CreatedAt FROM history WHERE UserID = @UserID ORDER BY CreatedAt DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string line = $"{reader["InputValue"]} {reader["FromUnit"]} → {reader["ToUnit"]} = {reader["ResultValue"]} ({reader["CreatedAt"]})";
                                historyList.Add(line);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error retrieving history: " + ex.Message);
                    }
                }
            }

            return historyList;
        }
    }
}