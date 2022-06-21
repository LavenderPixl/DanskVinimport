using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DanskVinimport.DVIService;

namespace DanskVinimport
{
    class Program
    {
        static void underMin(monitorSoapClient d) //Stock items, that are currently under minimum, get displayed.
        {
            Console.SetCursorPosition(44, 3);
            Console.WriteLine("Varer under minimum:");
            Console.ForegroundColor = ConsoleColor.White;
            int horizontal = 44;
            int vertical = 5;
            foreach (string line in d.StockItemsUnderMin()) //Each variable located in the array in StockItemsUnderMin, gets displayed until there is none left.
            {
                Console.SetCursorPosition(horizontal, vertical);
                Console.WriteLine(line);
                vertical++;
            }
        }
        static void overMax(monitorSoapClient d) //Stock items, that are currently over maximum, get displayed.
        {
            //Over Max
            Console.SetCursorPosition(44, 11);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Varer over maksimum");
            Console.ForegroundColor = ConsoleColor.White;
            int horizontal = 44;
            int vertical = 13;
            foreach (string line in d.StockItemsOverMax()) //Each variable located in the array in StockItemsOverMax, gets displayed until there is none left.
            {
                Console.SetCursorPosition(horizontal, vertical);
                Console.WriteLine(line);
                vertical++;
            }
        }
        static void mostSold(monitorSoapClient d) //Most sold Stock items, get displayed.
        {
            Console.SetCursorPosition(44, 19);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Mest solgte i dag");
            Console.ForegroundColor = ConsoleColor.White;
            int horizontal = 44;
            int vertical = 21;
            foreach (string line in d.StockItemsMostSold()) //Each variable located in the array in mostSold, gets displayed until there is none left.
            {
                Console.SetCursorPosition(horizontal, vertical);
                Console.WriteLine(line);
                vertical++;
            }
        }
        static void lagerStatus(monitorSoapClient d) //Lagerstatus gets displayed. underMin, overMax and mostSold get called.
        {
            Console.SetCursorPosition(56, 1);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Lagerstatus");

            underMin(d);
            overMax(d);
            mostSold(d);
        }
        static void datoTid() //Date and time, get displayed.
        {
            //Dato & Tid
            Console.SetCursorPosition(14, 15);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" Dato / Tid");

            string day = DateTime.Now.ToString("dddd", new CultureInfo("da-DK"));
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(2, 17);
            Console.WriteLine("København:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, 18);
            DateTime local = DateTime.Now;
            Console.WriteLine($"{day} {local}"); //Displays the day, date and time. Ex: mandag 14/06/2022 20:05:43

            Console.SetCursorPosition(2, 20);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("London:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, 21);
            DateTime dateUk = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time")); //Creates a variable called dateUk, that gets the current time from DateTime.Now (aka. the local time on the computer, and converts it to GMT Standard Time.
            day = dateUk.ToString("dddd", new CultureInfo("da-DK"));
            Console.WriteLine($"{day} {dateUk}"); //Displays the day, date and time. Ex: mandag 14/06/2022 20:05:43

            Console.SetCursorPosition(2, 23);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Singapore:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, 24);
            DateTime dateSing = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time")); //Creates a variable called dateSing, that gets the current time from DateTime.Now (aka. the local time on the computer, and converts it to Singapore Standard Time.
            day = dateSing.ToString("dddd", new CultureInfo("da-DK"));
            Console.WriteLine($"{day} {dateSing}"); //Displays the Day, date and Time. Ex: mandag 14/06/2022 20:05:43
        }
        static void tempHumidity(monitorSoapClient d) //Temperature and Humidity, gets displayed.
        {
            //Temp & Humidity
            Console.SetCursorPosition(8, 1);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Temperatur og fugtighed");
            //Lager
            Console.SetCursorPosition(2, 4);
            Console.WriteLine("Lager:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, 5);
            Console.WriteLine($"Temp: {d.StockTemp()}°C");
            Console.SetCursorPosition(2, 6);
            Console.WriteLine($"Fugt: {d.StockHumidity()} %");
            //Underfor
            Console.SetCursorPosition(2, 8);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Udenfor:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, 9);
            Console.WriteLine($"Temp: {d.OutdoorTemp()}°C");
            Console.SetCursorPosition(2, 10);
            Console.WriteLine($"Fugt: {d.OutdoorHumidity()} %");
        }
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
        static void horizontalLine(int xcood, int ycood, int length) //Creates a horizontal line.
        {
            int line = 0;
            while (line != length)
            {
                Console.SetCursorPosition(xcood + line, ycood);
                Console.WriteLine("_");
                line++;
            }
        }
        static void generateUI() //Generates the UI
        {
            horizontalLine(42, 9, 42);
            horizontalLine(42, 17, 42);
            horizontalLine(0, 13, 42);
            verticalLine(42, 0, 26);
        }
        static void Main(string[] args)
        {
            monitorSoapClient ds = new monitorSoapClient();

            Console.CursorVisible = false; //Turns cursor invisibile.
            generateUI();
            lagerStatus(ds);
            tempHumidity(ds);

            DateTime timer = DateTime.Now.AddMinutes(5); //Creates a timer, that takes the local time, and adds 5 Minutes to it.
            do
            {
                if (DateTime.Compare(timer, DateTime.Now) == -1) //Compares timer, with the local time. If timer is past the local time, adds 5 minutes to timer and resets lagerStatus & tempHumidity.
                {
                    timer = DateTime.Now.AddMinutes(5);
                    lagerStatus(ds);
                    tempHumidity(ds);
                }
                datoTid();
            } while (true); //Repeats the datoTid(), every second, causing the date and time to be live updated.
        }
    } 
}
