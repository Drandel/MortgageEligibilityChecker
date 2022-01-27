using MortgageEligibilityChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageEligibilityChecker.Services
{
    public class LoanEligibilityReportGenerator
    {
        public int ApprovedApplications { get; set; }
        public int ReceivedApplications { get; set; }
        public decimal ApprovalRate { get; set; }

        public LoanEligibilityReportGenerator(string[] fileContents)
        {
            List<Application> applications = FileParser.ParseApplicationData(fileContents);
            applications.ForEach(CheckEligibility);
        }

        private void CheckEligibility(Application application)
        {
            // DTI ratio < 50%
                // DTI = All borrowers monthly liability (including loan) / borrowers monthly income
                // Any liability which will be fully paid off in 10 payments does not contribute to DTI
                // Joint liabilities (tied to both the borrower and coborrower) should only be counted once per application
            // Credit score is > 620
                // User lower value if two borrowers are present.
        }
    }
}
