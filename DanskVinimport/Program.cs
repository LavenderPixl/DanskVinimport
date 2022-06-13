using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanskVinimport
{
    class Program
    {
        static void verticalLine(int xcood, int ycood, int length) //Creates a vertical line, down the middle that continues until length(26) is reached.
        {
            int line = 0;
            while (line != length)
            {
                Console.SetCursorPosition(xcood, line + ycood);
                Console.WriteLine("|");
                line++;
            } 
        }
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyInfo;
            do
            {
                DVIService.monitorSoapClient ds = new DVIService.monitorSoapClient();
                Console.Clear();
                Console.SetCursorPosition(7, 1);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Temperatur og fugtighed");
                Console.SetCursorPosition(2, 4);
                Console.WriteLine("Lager:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, 5);
                Console.WriteLine($"Temp: {ds.StockTemp()}°C");
                Console.SetCursorPosition(2, 6);
                Console.WriteLine($"Fugt: {ds.StockHumidity()} %");

                Console.SetCursorPosition(2, 8);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Udenfor:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, 9);
                Console.WriteLine($"Temp: {ds.OutdoorTemp()}°C");
                Console.SetCursorPosition(2, 10);
                Console.WriteLine($"Fugt: {ds.OutdoorHumidity()} %");

                Console.WriteLine("\n\n__________________________________________");
                Console.SetCursorPosition(14, 15);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(" Dato / Tid");
                Console.SetCursorPosition(2, 17);
                Console.WriteLine("København:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, 18);
                string time = DateTime.Now.ToString("HH:mm:ss"); //Creates a variable, called Time. It shows Hour:Minutes:Seconds
                string day = DateTime.Now.ToString("dddd", new CultureInfo("da-DK")); //Creates a variable, called day. It shows which day it is, in Danish.
                string date = DateTime.Now.ToString("dd/MM/yyyy"); //Creates a variable, called date. It shows Day/Month/Year.
                Console.WriteLine($"{day} {date} {time}");
                Console.SetCursorPosition(2, 20);

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("London:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, 21);
                DateTime dateUk = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
                Console.WriteLine($"{day} {dateUk}");
                Console.SetCursorPosition(2, 23);

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Singapore:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, 24);
                DateTime dateSing = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));
                Console.WriteLine($"{day} {dateSing}");


                verticalLine(42, 0, 26);
                keyInfo = Console.ReadKey();
            } while (keyInfo.Key != ConsoleKey.Escape); //The program will run continously until Escape is pressed.
        }
    } 
}
