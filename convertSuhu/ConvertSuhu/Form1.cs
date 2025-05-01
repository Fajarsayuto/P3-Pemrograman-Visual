using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConvertSuhu;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using MySql.Data.MySqlClient; 
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace ConvertSuhu
{
    public partial class Form1 : Form
    {
        public static string LoggedInEmail;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show(); // Menampilkan form register
            this.Hide();         // Menyembunyikan form login
        }

        private MySqlConnection GetConnection()
        {
            // Gantilah string koneksi di bawah sesuai dengan database Anda
            string connectionString = "Server=localhost;Database=convertSuhu;Uid=root;Pwd=;";
            return new MySqlConnection(connectionString);
        }

        private bool ValidateLogin(string email, string password)
        {
            bool isValid = false;

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();

                    // Query untuk memeriksa apakah email dan password ada di tabel Users
                    string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND PasswordHash = @PasswordHash";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);  // Memeriksa email
                    cmd.Parameters.AddWithValue("@PasswordHash", password);  // Memeriksa password yang di-hash

                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                    // Jika ada user yang cocok, maka login berhasil
                    if (userCount > 0)
                    {
                        isValid = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return isValid;
        }

        private void LOGIN_Click(object sender, EventArgs e)
        {

            string email = textBox1.Text;  // Mengambil nilai email dari textBox1
            string password = textBox2.Text;  // Mengambil nilai password dari textBox2

            // Memeriksa apakah email dan password tidak kosong
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                // Memanggil fungsi untuk validasi login
                if (ValidateLogin(email, password))
                {
                    LoggedInEmail = email; // Simpan email yang login
                    HOME homeForm = new HOME();
                    homeForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email atau password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mohon isi email dan password.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void LOGIN_Click_1(object sender, EventArgs e)
        {
            this.LOGIN.Click += new System.EventHandler(this.LOGIN_Click);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            

        }


    }
}
