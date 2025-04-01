using System;
using TemperatureLibrary;

namespace TemperatureProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            
            Console.WriteLine("Konversi Suhu\r");
            Console.WriteLine("----------------------\n");

            while (!endApp)
            {
                string temp = "";
                double result = 0;

                Console.WriteLine("Masukkan Suhu, Kemudian tekan enter.");
                temp = Console.ReadLine();

                double cleanNum = 0;
                while (!double.TryParse(temp, out cleanNum))
                {
                    Console.WriteLine("Input tidak valid. Harap masukkan angka: ");
                    temp = Console.ReadLine();
                }

                Console.WriteLine("Pilih opsi (konversi dari - konversi ke):");
                Console.WriteLine("\ta) Fahrenheit ke Celsius");
                Console.WriteLine("\tb) Fahrenheit ke Kelvin");
                Console.WriteLine("\tc) Celsius ke Fahrenheit");
                Console.WriteLine("\td) Celsius ke Kelvin");
                Console.WriteLine("\te) Kelvin ke Fahrenheit");
                Console.WriteLine("\tf) Kelvin ke Celsius");
                Console.Write("Masukkan Pilihan Anda? ");

                string op = Console.ReadLine();

                try
                {
                    result = TempConvert.Conversion(cleanNum, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("Operasi ini akan menghasilkan nilai yang tidak valid.\n");
                    }
                    else Console.WriteLine($"Hasil konversi Anda: {result:F2}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh tidak! Terjadi kesalahan! " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                Console.WriteLine("Tekan 'n' dan 'Enter' untuk keluar dari aplikasi, atau tombol lain dan 'Enter' untuk melanjutkan: ");

                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");

            }
            return;
           
        }

    }
}


