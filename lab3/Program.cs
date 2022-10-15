using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_2
{   
    class Uklon
    {
        public static void MakeOrder(string begin, string end)
        {
            Console.WriteLine($"begin: {begin}, end: {end}");
        }
    }
    class Bolt
    {
        public static void MakeOrderBegin(string detail, string begin)
        {
            if (detail == "begin")
            {
                Console.WriteLine($"begin: {begin}");
            }
            else
            {
                throw new Exception("smth wrong");
            }
        }

        public static void MakeOrderEnd(string detail, string end)
        {
            if (detail == "end")
            {
                Console.WriteLine($"end: {end}");
            }
            else
            {
                throw new Exception("smth wrong");
            }
        }
    }
    class Uber
    {
        public static void MakeOrder(string begin, string end, DateTime date)
        {
            Console.WriteLine($"begin: {begin}, end: {end}, time: {date}");
        }
    }
    class TaxiFacade
    {
        public static void MakeOrder(string taxi, string begin, string end)
        {
            switch (taxi)
            {
                case "Uklon":
                    Uklon.MakeOrder(begin, end);
                    break;
                case "Bolt":
                    Bolt.MakeOrderEnd("end", end);
                    Bolt.MakeOrderBegin("begin", begin);
                    break;
                case "Uber":
                    Uber.MakeOrder(begin, end, DateTime.Now);
                    break;
                default:
                    throw new Exception("Invalid taxi");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            TaxiFacade.MakeOrder("Uklon", "8 Березня", "Ректорат Ужну");
            Console.WriteLine();
            TaxiFacade.MakeOrder("Bolt", "8 Березня", "Ректорат Ужну");
            Console.WriteLine();
            TaxiFacade.MakeOrder("Uber", "8 Березня", "Ректорат Ужну");
            Console.WriteLine();
        }
    }
}
