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
        public static int LoggedInUserId;


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
            registerForm.Show(); 
            this.Hide();         
        }

        private MySqlConnection GetConnection()
        {
            string connectionString = "Server=localhost;Database=convertSuhu;Uid=root;Pwd=;";
            return new MySqlConnection(connectionString);
        }

        private bool ValidateLogin(string email, string password, out int userId)
        {
            userId = -1;

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = "SELECT UserID FROM Users WHERE Email = @Email AND PasswordHash = @PasswordHash";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", password); 

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);  
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return false;
        }


        private void LOGIN_Click(object sender, EventArgs e)
        {

            string email = textBox1.Text;  
            string password = textBox2.Text; 

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                if (ValidateLogin(email, password, out int userId))
                {
                    LoggedInUserId = userId;

                    HOME homeForm = new HOME(userId);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
