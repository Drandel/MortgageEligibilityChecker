using System;

namespace MortgageEligibilityChecker.Models
{
    public class Loan
    {
        public string ApplicationId { get; set; }
        public decimal PrincipalAmount { get; set; }
        public int Years { get; set; }
        public decimal Rate { get; set; }
        public decimal MonthlyPayment { get; set; }
    
        public Loan(string applicationId, string principleAmount, string years, string rate, string monthlyPayment)
        {
            ApplicationId = applicationId;
            PrincipalAmount = Convert.ToDecimal(principleAmount);
            Years = Convert.ToInt32(years);
            Rate = Convert.ToDecimal(rate);
            MonthlyPayment = Convert.ToDecimal(monthlyPayment);
        }
    }
}
