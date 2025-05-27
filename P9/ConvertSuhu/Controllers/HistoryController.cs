using ConvertSuhu.Models;
using System.Collections.Generic;

namespace ConvertSuhu.Controllers
{
    public class HistoryController
    {
        public static void SaveHistory(int userId, string conversion)
        {
            UserActivity.SaveConversionHistory(userId, conversion);
        }

        public static List<string> GetHistory(int userId)
        {
            return UserActivity.GetConversionHistory(userId);
        }
    }
}