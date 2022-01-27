using System;
using System.Collections.Generic;
using System.Linq;

namespace MortgageEligibilityChecker.Models
{
    public class Liability
    {
        public string ApplicationId { get; set; }
        public List<string> Names { get; set; }
        public string Kind { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal OutstandingBalance { get; set; }

        public Liability(string applicationId, string names, string kind, string monthlyPayment, string outstandingBalance)
        {
            ApplicationId = applicationId;
            Names = names.Split(',').ToList();
            Kind = kind;
            MonthlyPayment = Convert.ToDecimal(monthlyPayment);
            OutstandingBalance = Convert.ToDecimal(outstandingBalance);
        }

    }
}