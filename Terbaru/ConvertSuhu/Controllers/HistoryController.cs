using System;
using System.Data;
using System.Windows.Forms;
using ConvertSuhu.Models;

namespace ConvertSuhu.Controllers
{
    public static class HistoryController
    {
        public static void SaveHistory(int userId, double input, string from, string to, double result)
        {
            UserActivity.SaveConversionHistory(userId, input, from, to, result);
        }

        public static DataTable GetHistory(int userId)
        {
            return UserActivity.GetConversionHistoryTable(userId);
        }

        public static void HapusRiwayat(int id)
        {
            UserActivity.HapusRiwayat(id);
        }

        public static void ExportToCsv(int userId)
        {
          DataTable dt = UserActivity.GetConversionHistoryTable(userId);

          if (dt.Rows.Count == 0)
          {
            MessageBox.Show("Tidak ada data untuk diekspor.");
            return;
          }

          SaveFileDialog sfd = new SaveFileDialog
          {
            Filter = "CSV files (*.csv)|*.csv",
            FileName = $"history_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
          };

          if (sfd.ShowDialog() != DialogResult.OK) return;

          try
          {
            csvExport.WriteDataTableToCsv(dt, sfd.FileName, "UserId");
            MessageBox.Show("Data berhasil diekspor ke CSV.");
          }
          catch (Exception ex)
          {
            MessageBox.Show($"Gagal mengekspor data:\n{ex.Message}");
          }
        }
  }
}
