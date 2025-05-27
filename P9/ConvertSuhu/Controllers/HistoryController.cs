using ConvertSuhu.Models;
using System.Collections.Generic;
using System.Data;

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

    }
}