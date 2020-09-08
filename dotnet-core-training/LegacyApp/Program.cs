using PrimeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the Number to check Prime:");
            var number = Convert.ToInt32(Console.ReadLine());

            if (new PrimeService().IsPrime(number))
            {
                Console.WriteLine("Number is Prime.");
            }
            else
            {
                Console.WriteLine("Number is not Prime.");
            }
            Console.ReadLine();
        }
    }
}
