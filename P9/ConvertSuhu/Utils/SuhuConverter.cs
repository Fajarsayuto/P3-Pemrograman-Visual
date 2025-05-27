namespace ConvertSuhu.Utils
{
    public static class SuhuConverter
    {
        public static double Konversi(double nilaiAwal, string dariSatuan, string keSatuan)
        {
            double inCelcius = nilaiAwal;

            switch (dariSatuan)
            {
                case "Fahrenheit": inCelcius = (nilaiAwal - 32) * 5 / 9; break;
                case "Kelvin": inCelcius = nilaiAwal - 273.15; break;
                case "Reamur": inCelcius = nilaiAwal * 5 / 4; break;
                case "Celcius": default: break;
            }

            switch (keSatuan)
            {
                case "Fahrenheit": return (inCelcius * 9 / 5) + 32;
                case "Kelvin": return inCelcius + 273.15;
                case "Reamur": return inCelcius * 4 / 5;
                case "Celcius": default: return inCelcius;
            }
        }
    }
}
