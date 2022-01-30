using MortgageEligibilityChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageEligibilityChecker.Services
{
    public class LoanEligibilityReportService
    {
        public int ApprovedApplications { get; set; }
        public int ReceivedApplications { get; set; }
        public decimal ApprovalRate { get; set; }
        public List<Application> Applications { get; set;  }

        public LoanEligibilityReportService(string path)
        {
            Applications = FileParserService.ParseApplicationData(path);
        }

        public void GenerateReport()
        {
            Applications.ForEach(EligibilityService.CheckEligibility);
            GenerateOutput();
        }


        private void GenerateOutput()
        {
            float approvedApplications = Applications.Count(x => x.Approved == true); ;
            float applicationCount = Applications.Count();
            float approvalRate = (approvedApplications/applicationCount) * 100;

            Console.WriteLine("Summary: {0} application approved, {1} applications received, {2}% approval rate", approvedApplications, applicationCount, approvalRate);
            Console.WriteLine("");
            foreach (Application application in Applications)
            {
                string approvalStatus = application.Approved ? "approved" : "denied";
                if(application.EvaluatedDtiRatio == -1)
                {
                    Console.WriteLine("{0}: {1} (No Income), DTI: N/A, Credit Score: {2}", application.Id, approvalStatus, application.EvaluatedCreditScore);
                }
                else
                {
                    string dtiRatio = application.EvaluatedDtiRatio.ToString("0.000");
                    Console.WriteLine("{0}: {1}, DTI: {2}, Credit Score: {3}", application.Id, approvalStatus, dtiRatio, application.EvaluatedCreditScore);
                }
                
            }
        }

        
    }
}
