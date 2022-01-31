======== Instructions ========
To Run the Application:
- Open MortgageEligibilityChecker.sln in Visual Studio
- Click the Start button or press f5
- I have the project set to run the input given in the prompt, automatically.
  If you wish to change the file, either edit the file found at:
  "MortgageEligibilityChecker\MortgageEligibilityChecker\bin\Debug\data.tx"
  or place a file in the directory above and set it in the Command Line Arguments in the Project Properties.
  (Right Click MortgateEligibiltyChecker Project->Properties->Debug->Command Line Arguments)

To Run the Unit Tests
- Open MortgageEligibilityChecker.sln in Visual Studio
- Navigate to the Test Explorer (View -> Test Explorer or Crtl + E , T)
- Click "Run all Tests in View"

======== Description ========
I chose to go with C# becuase I wanted to have static types while parsing the input file. 
It made the most sense for me to organize my data structure around the Appliaction object. (See "Models UML.png" at root of solution)
I decided to front load the complexity onto the file parser. By parsing the file into the object hierarchy first, it allowed for easy manipulation of the data.
I have the program reading the file by passing the file path as a command line argument.
The file parser reads one line at a time and processes a full Application once all of a given Application's data has been read into a placeholder object. 
This approach prevents the entire file from being read into memory at one time, allowing for bigger file sizes. 
Another benefit of this approach is that it would be easy to persist a new Application to a database after its creaton, ensuring no data is lost if the parser were to fail at some portion of the way through a big file.
I abstracted the EligibilityService, FileParserService, and LoadnEligibilityReportService into thier own classes to allow for easy maintainance and modularity. 

I decided to set the the Evaluated DTI Ratio of an application equal to -1 if the Borrowers had no collective income instead trying to divide the Borrowers total Liability by zero. 
I display this case in the STDOUT as "A#: denied (No Income) DTI: N/A"

I made the following assumptions:
- The input files will always separate Applications by a blank line.
- A given input line will have complete data. (i.e. a LOAN will never be missing a MonthlyPayment or PrincipleAmount, etc.)


Thanks for your time to look over my solution, it was a fun puzzle to solve! 

Dean Randel
Dee.Randel@gmail.com
1/28/2022
Lower Mortgage Interview Coding Exercise
