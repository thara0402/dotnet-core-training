using PrimeLibrary;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("The current time is " + DateTime.Now);

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
