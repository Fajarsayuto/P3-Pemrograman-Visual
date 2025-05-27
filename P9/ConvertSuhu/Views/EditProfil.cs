using System;
using System.Windows.Forms;
using ConvertSuhu.Controllers;

namespace ConvertSuhu.Views
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
            var user = UserController.GetUserById(userId);
            if (user != null)
            {
                textBox1.Text = user.Username;
                textBox3.Text = user.Email;
                textBox2.Text = user.PasswordHash;
            }
            else
            {
                MessageBox.Show("Data pengguna tidak ditemukan.");
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

            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menyimpan perubahan?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                bool success = UserController.UpdateProfile(userId, username, email, passwordHash);
                if (success)
                {
                    MessageBox.Show("Data berhasil diperbarui.");
                    HOME homeForm = new HOME(userId);
                    homeForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Gagal memperbarui data.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HOME homeForm = new HOME(userId);
            homeForm.Show();
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}