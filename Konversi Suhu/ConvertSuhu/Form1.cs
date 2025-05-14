using System;
using System.Windows.Forms;

namespace ConvertSuhu
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
                if (userActivity.ValidateLogin(email, password, out int userId))
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
