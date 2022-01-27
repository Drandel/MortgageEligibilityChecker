using System;
using System.Collections.Generic;

namespace MortgageEligibilityChecker.Models
{
    public class Application
    {
        public string Id { get; set; }
        public bool Approved { get; set; }
        public Loan Loan { get; set; }
        public Borrower Borrower { get; set; }
        public Borrower Coborrower { get; set; }
        public List<Liability> Liabilities { get; set; }

        public Application(string id)
        {
            Id = id;
            Liabilities = new List<Liability>();
        }
    }

   
}
