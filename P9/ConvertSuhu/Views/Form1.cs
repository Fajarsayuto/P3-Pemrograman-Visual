using System;
using System.Windows.Forms;
using ConvertSuhu.Controllers;

namespace ConvertSuhu.Views
{
    public partial class Form1 : Form
    {
        public static int LoggedInUserId;

        public Form1()
        {
            InitializeComponent();
        }

        private void LOGIN_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                if (AuthController.Login(email, password, out int userId))
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}