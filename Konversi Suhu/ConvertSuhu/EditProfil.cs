using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ConvertSuhu
{
    public partial class EditProfil : Form
    {
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
            using (MySqlConnection conn = Database.GetConnection())
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
                bool success = userActivity.UpdateUser(userId, username, email, passwordHash);

                if (success)
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HOME homeForm = new HOME(userId);
            homeForm.Show();
            this.Close();
        }
    }
}
