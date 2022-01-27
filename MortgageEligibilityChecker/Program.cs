using MortgageEligibilityChecker.Services;
using System;
using System.Collections.Generic;
using System.IO;


namespace MortgageEligibilityChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string[] fileContents = File.ReadAllLines(args[0]);
                if (fileContents.Length > 0)
                {
                   LoanEligibilityReportGenerator reportGenerator = new LoanEligibilityReportGenerator(fileContents);
                }
                else
                {
                    Console.WriteLine("File contains no data! Try again with another file!");
                }
            }
            else
            {
                Console.WriteLine("Please specify a file to read!");
            }
            Console.ReadKey();
        }
    }
}
