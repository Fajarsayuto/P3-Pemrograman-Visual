using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ConvertSuhu
{
    public partial class EditProfil : Form
    {
        private string connectionString = "Server=localhost;Database=convertSuhu;Uid=root;Pwd=;";

        private int userId;

        public EditProfil(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void EditProfil_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void LoadUserData()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT Username, Email, PasswordHash FROM users WHERE UserID = @UserID";

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
                                textBox1.Text = reader["Username"].ToString();
                                textBox3.Text = reader["Email"].ToString();
                                textBox2.Text = reader["PasswordHash"].ToString(); 
                            }
                            else
                            {
                                MessageBox.Show("Data pengguna tidak ditemukan.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal memuat data: " + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string email = textBox3.Text.Trim();
            string passwordHash = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwordHash))
            {
                MessageBox.Show("Semua field harus diisi.");
                return;
            }

            DialogResult dialogResult = MessageBox.Show(
            "Apakah Anda yakin ingin menyimpan perubahan?",
            "Konfirmasi Simpan",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
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
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data berhasil diperbarui.");
                                HOME homeForm = new HOME(userId);
                                homeForm.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Tidak ada data yang diperbarui.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Gagal memperbarui data: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HOME homeForm = new HOME(userId);
            homeForm.Show();
            this.Close();
        }
    }
}
