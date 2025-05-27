using ConvertSuhu.Models;
using ConvertSuhu.Utils;


namespace ConvertSuhu.Controllers
{
    public class HomeController
    {
        private readonly UserActivity userActivity;

        public HomeController()
        {
            userActivity = new UserActivity();
        }

        public double Konversi(double suhu, string dari, string ke)
        {
            return SuhuConverter.Konversi(suhu, dari, ke);
        }

        public void SimpanRiwayat(int userId, double input, string from, string to, double result)
        {
            UserActivity.SaveConversionHistory(userId, input, from, to, result);
        }
    }
}