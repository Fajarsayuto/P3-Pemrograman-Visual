using System;
using System.Windows.Forms;
using ConvertSuhu.Controllers;
using System.Data;
using System.Collections.Generic;
using ConvertSuhu.Models;

namespace ConvertSuhu.Views
{
    public partial class History : Form
    {
        private int userId;


        public History(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            Load += History_Load;
        }

        private void History_Load(object sender, EventArgs e)
        {
            LoadUserHistory();
        }

        private void LoadUserHistory()
        {
            dataGridView2.DataSource = UserActivity.GetConversionHistoryTable(Form1.LoggedInUserId);

            if (dataGridView2.Columns["UserId"] != null)
                dataGridView2.Columns["UserId"].Visible = false; // sembunyikan id
        }






        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Form1.LoggedInUserId = -1;
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Hide();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HOME homeForm = new HOME(userId);
            homeForm.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadUserHistory();
        }



        private void LOGIN_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Columns["UserId"] == null)
            {
                MessageBox.Show("Kolom ID tidak ditemukan dalam data. Tidak bisa menghapus.");
                return;
            }

            if (dataGridView2.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        int id = Convert.ToInt32(row.Cells["UserId"].Value);
                        HistoryController.HapusRiwayat(id);
                    }
                }

                LoadUserHistory();
            }
            else
            {
                MessageBox.Show("Pilih baris yang ingin dihapus.");
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void History_Load_1(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

    private void button2_Click(object sender, EventArgs e)
    {
      HistoryController.ExportToCsv(userId);

  }
}
}
