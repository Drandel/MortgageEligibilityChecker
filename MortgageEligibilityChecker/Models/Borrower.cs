using System;
using System.Collections.Generic;

namespace MortgageEligibilityChecker.Models
{
    public class Borrower
    {
        public string ApplicationId { get; set; }
        public string Name { get; set; }
        public int CreditScore { get; set; }
        public List<Income> Incomes { get; set; }

        public Borrower(string applicationId, string name, string creditScore)
        {
            ApplicationId = applicationId;
            Name = name;
            CreditScore = Convert.ToInt32(creditScore);
            Incomes = new List<Income>();
        }
    }
}
