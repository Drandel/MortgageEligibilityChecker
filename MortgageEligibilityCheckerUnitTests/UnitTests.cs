using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageEligibilityChecker.Models;
using MortgageEligibilityChecker.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MortgageEligibilityCheckerUnitTests
{
    [TestClass]
    public class FileParserServiceTests
    {
        [TestMethod]
        public void BuildApplicationTest()
        {
            List<Liability> ExpectedLiabilities = new List<Liability>();
            ExpectedLiabilities.Add(new Liability("A1", "Elon,Jeff", "BusinessLoan", "5252.30", "252110.40"));
            
            List<Income> ExpectedCoborrowerIncomes = new List<Income>();
            ExpectedCoborrowerIncomes.Add(new Income("A1", "Jeff", "Salary", "83000"));
            Borrower ExpextedCoborrower = new Borrower("A1", "Jeff", "698");
            ExpextedCoborrower.Incomes = ExpectedCoborrowerIncomes;

            List<Income> ExpectedBorrowerIncomes = new List<Income>();
            ExpectedBorrowerIncomes.Add(new Income("A1", "Elon", "Salary", "90000"));
            Borrower ExpextedBorrower = new Borrower("A1", "Elon", "721");
            ExpextedBorrower.Incomes = ExpectedBorrowerIncomes;

            Loan ExpectedLoan = new Loan("A1", "2000000", "4", "2.5", "41666.67");
            Application ExpectedApplication = new Application("A1");
            ExpectedApplication.Loan = ExpectedLoan;
            ExpectedApplication.Borrower = ExpextedBorrower;
            ExpectedApplication.Coborrower = ExpextedCoborrower;
            ExpectedApplication.Liabilities = ExpectedLiabilities;

            // Run test file against FileParserService
            // test file located at MortgageEligibilityChecker\MortgageEligibilityCheckerUnitTests\bin\Debug\testData.txt
            List<Application> Applications = FileParserService.ParseApplicationData("../testData.txt");
            Application ActualApplication = Applications[0];

            //Serialize objects to allow for easy equality assertion
            string _ActualApplication = JsonSerializer.Serialize(ActualApplication);
            string _ExpectedApplication = JsonSerializer.Serialize(ExpectedApplication);

            Assert.AreEqual(_ExpectedApplication, _ActualApplication);
        }
    }

    [TestClass]
    public class EligibilityServiceTests
    {
        [TestMethod]
        public void CheckEligibilityTest()
        {
            const int ExpectedEvaluatedCreditScore = 698;
            const float ExpectedEvaluatedDtiRatio = .27120793F;
            const bool ExpectedApprovalStatus = true;

            List<Liability> ExpectedLiabilities = new List<Liability>();
            ExpectedLiabilities.Add(new Liability("A1", "Elon,Jeff", "BusinessLoan", "5252.30", "252110.40"));

            List<Income> ExpectedCoborrowerIncomes = new List<Income>();
            ExpectedCoborrowerIncomes.Add(new Income("A1", "Jeff", "Salary", "83000"));
            Borrower ExpextedCoborrower = new Borrower("A1", "Jeff", "698");
            ExpextedCoborrower.Incomes = ExpectedCoborrowerIncomes;

            List<Income> ExpectedBorrowerIncomes = new List<Income>();
            ExpectedBorrowerIncomes.Add(new Income("A1", "Elon", "Salary", "90000"));
            Borrower ExpextedBorrower = new Borrower("A1", "Elon", "721");
            ExpextedBorrower.Incomes = ExpectedBorrowerIncomes;

            Loan ExpectedLoan = new Loan("A1", "2000000", "4", "2.5", "41666.67");
            Application ActualApplication = new Application("A1");
            ActualApplication.Loan = ExpectedLoan;
            ActualApplication.Borrower = ExpextedBorrower;
            ActualApplication.Coborrower = ExpextedCoborrower;
            ActualApplication.Liabilities = ExpectedLiabilities;

            EligibilityService.CheckEligibility(ActualApplication);

            Assert.AreEqual(ActualApplication.Approved, ExpectedApprovalStatus);
            Assert.AreEqual(ActualApplication.EvaluatedCreditScore, ExpectedEvaluatedCreditScore);
            Assert.AreEqual(ActualApplication.EvaluatedDtiRatio, ExpectedEvaluatedDtiRatio);
        }
    }

    [TestClass]
    public class LoanEligibilityReportServiceTests
    {
        [TestMethod]
        public void GenerateReportTest()
        {
            string ExpectedOutput = string.Format("Summary: 1 application approved, 1 applications received, 100% approval rate{0}{0}A1: approved, DTI: 0.271, Credit Score: 698", Environment.NewLine);
            List<Liability> ExpectedLiabilities = new List<Liability>();
            ExpectedLiabilities.Add(new Liability("A1", "Elon,Jeff", "BusinessLoan", "5252.30", "252110.40"));

            List<Income> ExpectedCoborrowerIncomes = new List<Income>();
            ExpectedCoborrowerIncomes.Add(new Income("A1", "Jeff", "Salary", "83000"));
            Borrower ExpextedCoborrower = new Borrower("A1", "Jeff", "698");
            ExpextedCoborrower.Incomes = ExpectedCoborrowerIncomes;

            List<Income> ExpectedBorrowerIncomes = new List<Income>();
            ExpectedBorrowerIncomes.Add(new Income("A1", "Elon", "Salary", "90000"));
            Borrower ExpextedBorrower = new Borrower("A1", "Elon", "721");
            ExpextedBorrower.Incomes = ExpectedBorrowerIncomes;

            Loan ExpectedLoan = new Loan("A1", "2000000", "4", "2.5", "41666.67");
            Application ActualApplication = new Application("A1");
            ActualApplication.Loan = ExpectedLoan;
            ActualApplication.Borrower = ExpextedBorrower;
            ActualApplication.Coborrower = ExpextedCoborrower;
            ActualApplication.Liabilities = ExpectedLiabilities;

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                LoanEligibilityReportService lers = new LoanEligibilityReportService("../testData.txt");
                lers.GenerateReport();
                Assert.AreEqual(ExpectedOutput.Trim(), sw.ToString().Trim());
            }
        }
    }

    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void LoanTest()
        {
            const string ExpectedApplicationId = "A1";
            const decimal ExpectedPrincipleAmount = 9999;
            const int ExpectedYears = 2;
            const decimal ExpectedRate = 2.5M;
            const decimal ExpectedMonthlyPayment = 123;

            const string ApplicationId = "A1";
            const string PrincipleAmount = "9999";
            const string Years = "2";
            const string Rate = "2.5";
            const string MonthlyPayment = "123";

            Loan ActualLoan = new Loan(ApplicationId, PrincipleAmount, Years, Rate, MonthlyPayment);

            Assert.AreEqual(ActualLoan.ApplicationId, ExpectedApplicationId);
            Assert.AreEqual(ActualLoan.PrincipalAmount, ExpectedPrincipleAmount);
            Assert.AreEqual(ActualLoan.Years, ExpectedYears);
            Assert.AreEqual(ActualLoan.Rate, ExpectedRate);
            Assert.AreEqual(ActualLoan.MonthlyPayment, ExpectedMonthlyPayment);
        }

        [TestMethod]
        public void LiabilityTest()
        {
            const string ExpectedApplicationId = "A1";
            List<string> ExpectedNames = new List<string>();
            ExpectedNames.Add("Elon");
            ExpectedNames.Add("Jeff");
            const string ExpectedKind = "CreditCard";
            const decimal ExpectedMonthlyPayment = 5252.30M;
            const decimal ExpectedOutstandingBalance = 252110.40M;
            const int ExpectedRemaingPayments = 48;

            const string ApplicationId = "A1";
            const string Names = "Elon,Jeff";
            const string Kind = "CreditCard";
            const string MonthlyPayment = "5252.30";
            const string OutstandingBalance = "252110.40";

            Liability ActualLiability = new Liability(ApplicationId, Names, Kind, MonthlyPayment, OutstandingBalance);

            Assert.AreEqual(ActualLiability.ApplicationId, ExpectedApplicationId);
            Assert.AreEqual(JsonSerializer.Serialize(ActualLiability.Names), JsonSerializer.Serialize(ExpectedNames));
            Assert.AreEqual(ActualLiability.Kind, ExpectedKind);
            Assert.AreEqual(ActualLiability.MonthlyPayment, ExpectedMonthlyPayment);
            Assert.AreEqual(ActualLiability.OutstandingBalance, ExpectedOutstandingBalance);
            Assert.AreEqual(ActualLiability.RemainingPayments , ExpectedRemaingPayments);

        }

        [TestMethod]
        public void IncomeTest()
        {
            const string ExpectedApplicationId = "A1";
            const string ExpectedName = "Elon";
            const string ExpectedKind = "Salary";
            const decimal ExpectedMonthlyAmount = 999999;

            const string ApplicationId = "A1";
            const string Name = "Elon";
            const string Kind = "Salary";
            const string MonthlyAmount = "999999";

            Income ActualIncome = new Income(ApplicationId, Name, Kind, MonthlyAmount);

            Assert.AreEqual(ActualIncome.ApplicationId, ExpectedApplicationId);
            Assert.AreEqual(ActualIncome.Name, ExpectedName);
            Assert.AreEqual(ActualIncome.Kind, ExpectedKind);
            Assert.AreEqual(ActualIncome.MonthlyAmount, ExpectedMonthlyAmount);

        }

        [TestMethod]
        public void BorrowerTest()
        {
            const string ExpectedApplicationId = "A1";
            const string ExpectedName = "Elon";
            const int ExpectedCreditScore = 720;
            List<Income> ExpectedIncomes = new List<Income>();
            ExpectedIncomes.Add(new Income("A1", "Elon", "Salary", "999999"));

            const string ApplicationId = "A1";
            const string Name = "Elon";
            const string CreditScore = "720";

            Borrower ActualBorrower = new Borrower(ApplicationId, Name, CreditScore);
            ActualBorrower.Incomes.Add(new Income("A1", "Elon", "Salary", "999999"));

            Assert.AreEqual(ActualBorrower.ApplicationId, ExpectedApplicationId);
            Assert.AreEqual(ActualBorrower.Name, ExpectedName);
            Assert.AreEqual(ActualBorrower.CreditScore, ExpectedCreditScore);
            Assert.AreEqual(JsonSerializer.Serialize(ActualBorrower.Incomes), JsonSerializer.Serialize(ExpectedIncomes));
        }

        [TestMethod]
        public void ApplicationTest()
        {
            const string ExpectedId = "A1";

            const string Id = "A1";

            Application ActualApplication = new Application(Id);

            Assert.AreEqual(ActualApplication.Id, ExpectedId);
            Assert.AreEqual(ActualApplication.Approved, false);
            Assert.AreEqual(typeof(List<Liability>), ActualApplication.Liabilities.GetType());
        }
    }
}
