using MortgageEligibilityChecker.Models;
using System;
using System.Collections.Generic;

namespace MortgageEligibilityChecker.Services
{
    public static class EligibilityService
    {
        public static void CheckEligibility(Application app)
        {
            app.EvaluatedCreditScore = getCreditScore(app);
            app.EvaluatedDtiRatio = getDtiRatio(app);

            if(app.EvaluatedDtiRatio == -1)
            {
                app.Approved = false;
            }
            else
            {
                app.Approved = app.EvaluatedDtiRatio < 0.50 && app.EvaluatedCreditScore > 620 ? true : false;
            }
            
        }

        private static float getDtiRatio(Application app)
        {
            decimal totalMonthlyIncome = GetBorrowersIncome(app);
            decimal borrowerMonthlyLiability = GetMonthlyLiability(app.Liabilities);
            decimal totalMonthlyLiability = borrowerMonthlyLiability + app.Loan.MonthlyPayment;

            if(totalMonthlyIncome == 0)
            {
                return -1;
            }
            return Convert.ToSingle(totalMonthlyLiability / totalMonthlyIncome); ;
        }

        private static decimal GetMonthlyLiability(List<Liability> liabilities)
        {
            decimal borrowerMonthlyLiability = 0.00M;
            foreach (Liability liability in liabilities)
            {
                if (liability.RemainingPayments > 10)
                {
                    borrowerMonthlyLiability += liability.MonthlyPayment;
                }
            }
            return borrowerMonthlyLiability;
        }

        private static decimal GetBorrowersIncome(Application app)
        {
            decimal borrowerIncome = 0;
            foreach (Income income in app.Borrower.Incomes)
            {
                borrowerIncome += income.MonthlyAmount;
            }

            if (!(app.Coborrower is null))
            {
                foreach (Income income in app.Coborrower.Incomes)
                {
                    borrowerIncome += income.MonthlyAmount;
                }
            }
            return borrowerIncome;
        }

        private static int getCreditScore(Application app)
        {
            int creditScore = app.Borrower.CreditScore;
            if (!(app.Coborrower is null))
            {
                creditScore = creditScore < app.Coborrower.CreditScore ? creditScore : app.Coborrower.CreditScore;
            }
            return creditScore;
        }
    }
}
