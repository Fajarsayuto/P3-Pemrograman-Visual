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
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT SuhuAwal, DariSatuan, KeSatuan, Hasil, TanggalWaktu FROM history WHERE UserID = @UserID ORDER BY TanggalWaktu DESC";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    var adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView2.AutoGenerateColumns = true;
                    dataGridView2.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengambil data: " + ex.Message);
                }
            }
        }


        private MySql.Data.MySqlClient.MySqlConnection GetConnection()
        {
            string connectionString = "Server=localhost;Database=convertSuhu;Uid=root;Pwd=;";
            return new MySql.Data.MySqlClient.MySqlConnection(connectionString);
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

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void LOGIN_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Apakah Anda yakin ingin menghapus semua riwayat?",
        "Konfirmasi Hapus",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (var conn = GetConnection())
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM history WHERE UserID = @UserID";
                        var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil menghapus " + rowsAffected + " data riwayat.");

                        History_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus data: " + ex.Message);
                    }
                }
            }
        }
    }
}
