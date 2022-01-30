using MortgageEligibilityChecker.Services;
using System;

namespace MortgageEligibilityChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string path = args[0];
                if (path.Length > 0)
                {
                    LoanEligibilityReportService reportGenerator = new LoanEligibilityReportService(path);
                    reportGenerator.GenerateReport();
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
