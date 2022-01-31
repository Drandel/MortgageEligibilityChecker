using MortgageEligibilityChecker.Models;
using System.Collections.Generic;
using System.Linq;

namespace MortgageEligibilityChecker.Services
{
    public static class FileParserService
    {
        public static List<Application> ParseApplicationData(string path)
        {
            List<Application> applications = new List<Application>();
            List<string> newApplication = new List<string>();
            foreach (string line in System.IO.File.ReadLines(path))
            {
                if (line == "")
                {
                    if(newApplication.Count > 0)
                    {
                        applications.Add(BuildApplication(newApplication));
                    }
                    newApplication.Clear();
                }
                else
                {
                    newApplication.Add(line);
                }
            }
            if (newApplication.Count > 0)
            {
                applications.Add(BuildApplication(newApplication));
            }

            return applications;
        }

        private static Application BuildApplication(List<string> newApplication)
        {
            Application application = new Application(ParseLine(newApplication, "APPLICATION")[0].Split(' ')[1]);

            AddLoansToApplications(application, ParseLine(newApplication, "LOAN"));
            AddBorrowersToApplications(application, ParseLine(newApplication, "BORROWER"));
            AddCoborrowersToApplications(application, ParseLine(newApplication, "COBORROWER"));
            AddLiabilitiesToApplications(application, ParseLine(newApplication, "LIABILITY"));
            AddIncomesToBorrowers(application, ParseLine(newApplication, "INCOME"));

            return application;
        }

        private static List<string> ParseLine(List<string> newApplication, string identifier)
        {
            return newApplication.Where(x => x.Split(' ')[0] == identifier).ToList();
        }

        private static void AddLoansToApplications(Application application, List<string> loansRaw)
        {
            foreach (string _loan in loansRaw)
            {
                string[] parsedLoan = _loan.Split(' ');
                application.Loan = new Loan(parsedLoan[1], parsedLoan[2], parsedLoan[3], parsedLoan[4], parsedLoan[5]);
            }
        }
        private static void AddBorrowersToApplications(Application application, List<string> borrowersRaw)
        {
            foreach (string _borrower in borrowersRaw)
            {
                string[] parsedBorrower = _borrower.Split(' ');
                application.Borrower = new Borrower(parsedBorrower[1], parsedBorrower[2], parsedBorrower[3]);
            }
        }
        private static void AddCoborrowersToApplications(Application application, List<string> coborrowersRaw)
        {
            foreach (string _coborrower in coborrowersRaw)
            {
                string[] parsedCoborrower = _coborrower.Split(' ');
                application.Coborrower = new Borrower(parsedCoborrower[1], parsedCoborrower[2], parsedCoborrower[3]);
            }
        }
        private static void AddLiabilitiesToApplications(Application application, List<string> liabilitiesRaw)
        {
            foreach (string _liability in liabilitiesRaw)
            {
                string[] parsedLiability = _liability.Split(' ');
                Liability liability = new Liability(parsedLiability[1], parsedLiability[2], parsedLiability[3], parsedLiability[4], parsedLiability[5]);
                application.Liabilities.Add(liability);
            }
        }

        private static void AddIncomesToBorrowers(Application application, List<string> incomeRaw)
        {
            foreach (string _income in incomeRaw)
            {
                string[] parsedIncome = _income.Split(' ');
                if (application.Borrower.Name == parsedIncome[2])
                {
                    AddIncomeToBorrower(application, parsedIncome);
                }
                if (application.Coborrower != null)
                {
                    if (application.Coborrower.Name == parsedIncome[2])
                    {
                        AddIncomeToCoborrower(application, parsedIncome);
                    }
                }
            }
        }
        private static void AddIncomeToBorrower(Application application, string[] parsedIncome)
        {
            Income income = new Income(parsedIncome[1], parsedIncome[2], parsedIncome[3], parsedIncome[4]);
            application.Borrower.Incomes.Add(income);
        }
        private static void AddIncomeToCoborrower(Application application, string[] parsedIncome)
        {
            Income income = new Income(parsedIncome[1], parsedIncome[2], parsedIncome[3], parsedIncome[4]);
            application.Coborrower.Incomes.Add(income);
        }
    }
}
