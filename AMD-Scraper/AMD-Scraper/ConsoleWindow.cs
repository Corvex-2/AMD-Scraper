#if DEBUG
using System;

namespace AMD_Scraper
{
    public class ConsoleWindow
    {
        public static void Main(string[] args)
        {
            Console.Title = "AMD Scraper";

            foreach(var Item in AMDShop.GetItems())
            {
                Console.WriteLine(Item);
            }

            Console.ReadLine();
        }
    }
}
#endif
