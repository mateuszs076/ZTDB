using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ZTDB.SQLDatabase;
using ZTDB.SQLDatabase.Models;

namespace ZTDB.Import
{
    internal class Program
    {
        const int NUMBER_OF_RECORDS = 2000;
        private static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("==== MENU ====");
                Console.WriteLine("1. Import z tabeli DataToImport");
                Console.WriteLine("Q. Wyjście");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    ImportFromTable();
                }
                if (key.Key == ConsoleKey.Q)
                {
                    exit = true;
                }
            }
        }

        private static void ImportFromTable()
        {
            using (var context = new SQLContext())
            {
                context.Database.Migrate();
                bool end = false;
                int i = 0;
                while (!end)
                {
                    var data = context.Set<DataToImport>().Skip(NUMBER_OF_RECORDS*i).Take(NUMBER_OF_RECORDS);
                }
            }
        }
    }
}