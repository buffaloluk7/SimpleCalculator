using System;
using SimpleCalculator.Core;

namespace SimpleCalculator.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var calculationService = new CalculationService();

            Console.Write("Input: ");

            string input;
            while ((input = Console.ReadLine()) != "exit")
            {
                try
                {
                    var result = calculationService.Calculate<double>(input);
                    Console.WriteLine($"Result: {result}");
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine($"\tError while doing the math.\n\t{exception.Message}");
                }

                Console.Write("Input: ");
            }
        }
    }
}