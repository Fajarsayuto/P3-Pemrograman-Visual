using System;
using System.Data;
using System.Windows.Forms;


namespace ConvertSuhu
{
    public partial class History : Form
    {
        private int userId;

        public History(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            this.Load += new System.EventHandler(this.History_Load);

        }

        private void History_Load(object sender, EventArgs e)
        {
            LoadHistoryData();

        }

        private void LoadHistoryData()
        {
            try
            {
                DataTable dt = userActivity.view(userId);
                dataGridView2.AutoGenerateColumns = true;
                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            History_Load(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HOME homeForm = new HOME(Form1.LoggedInUserId);
            homeForm.Show();
            this.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            History_Load(sender, e);
        }

        private void LOGIN_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Apakah Anda yakin ingin menghapus semua riwayat?",
            "Konfirmasi Hapus",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int rowsAffected = userActivity.Delete(userId);
                    MessageBox.Show("Berhasil menghapus " + rowsAffected + " data riwayat.");
                    LoadHistoryData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
           "Apakah Anda yakin ingin keluar?",
           "Konfirmasi Logout",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
