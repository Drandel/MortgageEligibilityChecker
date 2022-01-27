using System;

namespace MortgageEligibilityChecker.Models
{
    public class Income
    {
        public string ApplicationId { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public decimal MonthlyAmount { get; set; }

        public Income(string applicationId, string name, string kind, string monthlyAmount)
        {
            ApplicationId = applicationId;
            Name = name;
            Kind = kind;
            MonthlyAmount = Convert.ToDecimal(monthlyAmount);
        }
    }
}