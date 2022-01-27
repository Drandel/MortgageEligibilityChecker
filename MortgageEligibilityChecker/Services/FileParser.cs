using MortgageEligibilityChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MortgageEligibilityChecker.Services
{
    public static class FileParser
    {
        public static List<Application> ParseApplicationData(string[] data)
        {
            List<string> applicationsRaw = data.Where(x => x.Split(' ')[0] == "APPLICATION").ToList();
            List<string> loansRaw = data.Where(x => x.Split(' ')[0] == "LOAN").ToList();
            List<string> borrowersRaw = data.Where(x => x.Split(' ')[0] == "BORROWER").ToList();
            List<string> coborrowersRaw = data.Where(x => x.Split(' ')[0] == "COBORROWER").ToList();
            List<string> liabilitiesRaw = data.Where(x => x.Split(' ')[0] == "LIABILITY").ToList();
            List<string> incomeRaw = data.Where(x => x.Split(' ')[0] == "INCOME").ToList();

            List<Application> applications = applicationsRaw.Select(a => new Application(a.Split(' ')[1])).ToList();

            AddLoansToApplications(applications, loansRaw);
            AddBorrowersToApplications(applications, borrowersRaw);
            AddCoborrowersToApplications(applications, coborrowersRaw);
            AddLiabilitiesToApplications(applications, liabilitiesRaw);
            AddIncomesToBorrowers(applications, incomeRaw);

            return applications;
        }

        private static void AddLoansToApplications(List<Application> applications, List<string> loansRaw)
        {
            foreach (string _loan in loansRaw)
            {
                string[] parsedLoan = _loan.Split(' ');
                Application application = applications[applications.FindIndex(x => x.Id == parsedLoan[1])];
                application.Loan = new Loan(parsedLoan[1], parsedLoan[2], parsedLoan[3], parsedLoan[4], parsedLoan[5]);
            }
            
        }
        private static void AddBorrowersToApplications(List<Application> applications, List<string> borrowersRaw)
        {
            foreach (string _borrower in borrowersRaw)
            {
                string[] parsedBorrower = _borrower.Split(' ');
                Application application = applications[applications.FindIndex(x => x.Id == parsedBorrower[1])];
                application.Borrower = new Borrower(parsedBorrower[1], parsedBorrower[2], parsedBorrower[3]);
            }
        }
        private static void AddCoborrowersToApplications(List<Application> applications, List<string> coborrowersRaw)
        {
            foreach (string _coborrower in coborrowersRaw)
            {
                string[] parsedCoborrower = _coborrower.Split(' ');
                Application application = applications[applications.FindIndex(x => x.Id == parsedCoborrower[1])];
                application.Coborrower = new Borrower(parsedCoborrower[1], parsedCoborrower[2], parsedCoborrower[3]);
            }
        }
        private static void AddLiabilitiesToApplications(List<Application> applications, List<string> liabilitiesRaw)
        {
            foreach (string _liability in liabilitiesRaw)
            {
                string[] parsedLiability = _liability.Split(' ');
                Application application = applications[applications.FindIndex(x => x.Id == parsedLiability[1])];
                Liability liability = new Liability(parsedLiability[1], parsedLiability[2], parsedLiability[3], parsedLiability[4], parsedLiability[5]);
                application.Liabilities.Add(liability);
            }
        }

        private static void AddIncomesToBorrowers(List<Application> applications, List<string> incomeRaw)
        {
            foreach (string _income in incomeRaw)
            {
                string[] parsedIncome = _income.Split(' ');
                Application application = applications[applications.FindIndex(x => x.Id == parsedIncome[1])];
                Borrower borrower = null;
                if (application.Borrower.Name == parsedIncome[2])
                {
                    AddIncomeToBorrower(application, parsedIncome);
                }
                if (application.Coborrower.Name == parsedIncome[2])
                {
                    borrower = application.Coborrower;
                    AddIncomeToCoborrower(application, parsedIncome);
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
