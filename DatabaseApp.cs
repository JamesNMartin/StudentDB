//CHANGE HISTORY
//DATE          DEVELOPER          DESCRIPTION 
//TODO CHANGE HISTORY
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentDB
{
    class DatabaseApp
    {
        // info about the List class in the FCL chapter 9
        // actual storage for the students in the DB
        private List<Student> students = new List<Student>();
        static void Main(string[] args)
        {
            DatabaseApp dbApp = new DatabaseApp();
        }
        public DatabaseApp()
        {
            UserInterface();
            GoDatabase();
        }
        private void UserInterface()
        {
            Console.WriteLine(@"
    ____        __        __                       ___                ___            __  _           
   / __ \____ _/ /_____ _/ /_  ____ _________     /   |  ____  ____  / (_)________ _/ /_(_)___  ____ 
  / / / / __ `/ __/ __ `/ __ \/ __ `/ ___/ _ \   / /| | / __ \/ __ \/ / / ___/ __ `/ __/ / __ \/ __ \
 / /_/ / /_/ / /_/ /_/ / /_/ / /_/ (__  )  __/  / ___ |/ /_/ / /_/ / / / /__/ /_/ / /_/ / /_/ / / / /
/_____/\__,_/\__/\__,_/_.___/\__,_/____/\___/  /_/  |_/ .___/ .___/_/_/\___/\__,_/\__/_/\____/_/ /_/ 
                                                     /_/   /_/                                       
");
        }
        public void DisplayMainMenu()
        {
            Console.WriteLine(@"
*************** Main menu ***************
** [P]rint all records
** [C]reate Record
** [F]ind a Record
** [U]pdate Record
** [D]elete Record
** [S]ave the database
** [Q]uit the application
");
        }
        private void GoDatabase()
        {
            //TODO FIX ReadDataFromInputFile

            //ReadDataFromInputFile();
            while (true)
            {
                // display the main menu
                DisplayMainMenu();
                Console.Write("ENTER SELECTION: ");
                ConsoleKeyInfo selection = Console.ReadKey();
                Console.WriteLine("\n");

                switch (selection.KeyChar)
                {
                    case 'P':
                    case 'p':
                        WriteDataToConsole();
                        WriteDataToOutputFile();
                        //PrintAllRecords();
                        break;
                    case 'C':
                    case 'c':
                        CreateRecord();
                        break;
                    case 'F':
                    case 'f':
                        FindRecord();
                        break;
                    case 'U':
                    case 'u':
                        UpdateRecord();
                        break;
                    case 'D':
                    case 'd':
                        DeleteRecord();
                        break;
                    case 'Q':
                    case 'q':
                        QuitApplication();
                        break;
                    case 'T':
                    case 't':
                        TestMain();
                        break;
                    default:
                        Console.WriteLine("** ERROR: Incorrect selection. Please try again **");
                        Thread.Sleep(1000);// Sleep so the user can see the error message
                        break;
                }
            }
        }
        private void GetIndexFromList()
        {
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i} :: {students[i].EmailAddress}");
            }
        }
        private void ReadDataFromInputFile()
        {
            //CONSTRUCT AN OBJECT CONNECTED TO THE INPUT FILE
            StreamReader inFile = new StreamReader("STUDENT_DATABASE_INPUT_FILE");
            //READ THE DATA IN FROM THE FILE AND STORE IT IN THE LIST
            string str = string.Empty;
            while ((str = inFile.ReadLine()) != null)
            {
                int id = int.Parse(str);
                string firstName = inFile.ReadLine();
                string lastName = inFile.ReadLine();
                string emailAddress = inFile.ReadLine();
                DateTime enrolled = DateTime.Parse(inFile.ReadLine());
                //NEED TO MAKE A NEW CTOR FOR THE FILE INPUT


            }
            //CLOSE THE FILE
            inFile.Close();
        }
        //FUNCTIONS - 
        //PRECONDITIONS - 
        //INPUT - 
        //OUTPUT - 
        //POSTCONDITIONS - 
        private void DeleteRecord()
        {
            while (true)
            {
                // I used this link to help me search and ignore the case of the letters
                // https://stackoverflow.com/questions/6371150/comparing-two-strings-ignoring-case-in-c-sharp

                Console.Write("Enter the last name of the record you would like to delete: ");
                string searchTerm = Console.ReadLine();
                var foundRecord = (students.FirstOrDefault(lookup => lookup.LastName.Equals(searchTerm, StringComparison.InvariantCultureIgnoreCase))); // USING THE SEARCH TERM FROM USER  TO LOOKUP IF THERE IS A RECORD OF THAT PERSON BY LAST NAME.
                int foundRecordIndex = students.FindIndex(lookup => lookup.LastName.Equals(searchTerm, StringComparison.InvariantCultureIgnoreCase)); // GETTING THE INDEX OF THE RECORD THAT WAS SEARCHED TO USE FOR FURTHER MANIPULATION

                if (foundRecord != null)
                {
                    //RECORD FOUND
                    Console.WriteLine($"\n** Displaying record for {foundRecord.FirstName} {foundRecord.LastName} **\n");
                    //Console.WriteLine($"Record Index: {recordIndex}");
                    Console.WriteLine(foundRecord.ToStringForConsole());
                    Console.WriteLine($"Would you like to delete this record?");//prompt user if they want
                    Console.Write("[Y]es [N]o: ");
                    ConsoleKeyInfo selection = Console.ReadKey();
                    //char selection = char.Parse(Console.ReadLine());
                    if (selection.KeyChar == 'Y' || selection.KeyChar == 'y')
                    {
                        students.RemoveAt(foundRecordIndex);
                        Console.WriteLine($"\n** {foundRecord.FirstName} {foundRecord.LastName} was deleted from the database. **");
                        Thread.Sleep(1000);
                        break;
                        //EDIT RECORD
                    }
                    else if (selection.KeyChar == 'N' || selection.KeyChar == 'n')
                    {
                        break;
                        //NO EDIT
                    }
                }
                else
                {
                    break;
                    //RECORD NOT FOUND
                }
            }
        }
        private void UpdateRecord()
        {
            FindRecord();
        }
        private void UpdateRecord(int index)
        {
            var theStudent = students[index];//Make the student at the specific index a var 

            //MAKING A COPY OF THE STUDENT TO HOLD JUST IN CASE THE USER UPDATES ONE PIECE OF THE RECORD
            string newFirstName = theStudent.FirstName;
            string newLastName = theStudent.LastName;
            string newEmail = theStudent.EmailAddress;
            int newStudentID = theStudent.StudentID;
            decimal newGPA = theStudent.GPA;
            int newCreditsEarned = theStudent.CreditsEarned;

            while (true)
            {
                //DisplayEditMenu(index);
                /*
                 * THE LINE BELOW THAT PRINTS "CURRENTLY EDITTING" WILL DISPALAY WHAT THE RECORD IS UNTIL IT IS SAVED
                 * THE VALUES NEXT TO THE OPTIONS WILL CHANGE BASED ON WHAT YOU JUST CHANGED. 
                 * THEY WILL NOT SAVE UNTIL YOU QUIT AND SAVE AND THE VALUES WILL BE UPDATED.
                 */
                Console.WriteLine($@"
*************** Edit menu ***************

Currently Editting: {theStudent.FirstName} {theStudent.LastName} 

[S]tudent ID:     {newStudentID}
[F]irst Name:     {newFirstName}
[L]ast Name:      {newLastName}
[E]mail Address:  {newEmail}
[G]PA:            {newGPA}
[C]redits Earned: {newCreditsEarned}

[X]Cancel
[Q]uit and Save
");
                Console.WriteLine("What would you like to update? ");
                Console.Write("ENTER SELECTION: ");
                ConsoleKeyInfo selection = Console.ReadKey();
                //char selection = char.Parse(Console.ReadLine());
                if (selection.KeyChar == 'Q' || selection.KeyChar == 'q')//Quitting the edit menu and saving all the new values.
                {

                    theStudent.FirstName = newFirstName;
                    theStudent.LastName = newLastName;
                    theStudent.EmailAddress = newEmail;
                    theStudent.StudentID = newStudentID;
                    theStudent.GPA = newGPA;
                    theStudent.CreditsEarned = newCreditsEarned;

                    break;
                }
                else if (selection.KeyChar == 'X' || selection.KeyChar == 'x')
                {
                    //Console.WriteLine("Cancelling...");
                    break;
                }
                switch (selection.KeyChar)
                {
                    case 'S':
                    case 's':
                        Console.Write("Enter new student ID: ");
                        newStudentID = int.Parse(Console.ReadLine());
                        break;
                    case 'F':
                    case 'f':
                        Console.Write("Enter new first name: ");
                        newFirstName = Console.ReadLine();
                        break;
                    case 'L':
                    case 'l':
                        Console.Write("Enter new last name: ");
                        newLastName = Console.ReadLine();
                        break;
                    case 'E':
                    case 'e':
                        Console.Write("Enter new email: ");
                        newEmail = Console.ReadLine();
                        break;
                    case 'G':
                    case 'g':
                        Console.Write("Enter new GPA: ");
                        newGPA = decimal.Parse(Console.ReadLine());
                        break;
                    case 'C':
                    case 'c':
                        Console.Write("Enter new credits earned: ");
                        newCreditsEarned = int.Parse(Console.ReadLine());
                        break;
                    default:
                        break;
                }
                /* [S]tudent ID:
                 * [F]irst Name:
                 * [L]ast Name:
                 * [E]mail Address:
                 * [G]PA:
                 * [C]redits Earned: */
            }
        }
        private void FindRecord()
        {
            while (true)
            {
                //Get the search term they want using last name for lookup.
                Console.Write("\nPlease enter the last name of the person you're trying to find: ");
                string searchTerm = Console.ReadLine();

                // FOUND THIS CODE HERE https://stackoverflow.com/questions/3154310/search-list-of-objects-based-on-object-variable
                var foundRecord = students.FirstOrDefault(lookup => lookup.LastName.TrimEnd().Equals(searchTerm, StringComparison.InvariantCultureIgnoreCase)); // USING THE SEARCH TERM FROM USER  TO LOOKUP IF THERE IS A RECORD OF THAT PERSON BY LAST NAME.
                int foundRecordIndex = students.FindIndex(lookup => lookup.LastName.Equals(searchTerm, StringComparison.InvariantCultureIgnoreCase)); // GETTING THE INDEX OF THE RECORD THAT WAS SEARCHED TO USE FOR FURTHER MANIPULATION

                if (foundRecord != null)//if the record is found we display it to the console.
                {
                    Console.WriteLine($"\n** Displaying record for {foundRecord.FirstName} {foundRecord.LastName} **\n");
                    //Console.WriteLine($"Record Index: {recordIndex}");
                    Console.WriteLine(foundRecord.ToStringForConsole());
                    Console.WriteLine($"Would you like to edit this record?");
                    Console.Write("[Y]es [N]o: ");

                    ConsoleKeyInfo selection = Console.ReadKey();
                    //char selection = char.Parse(Console.ReadLine());
                    if (selection.KeyChar == 'Y' || selection.KeyChar == 'y')
                    {
                        UpdateRecord(foundRecordIndex);
                        break;
                        //EDIT RECORD
                    }
                    else if (selection.KeyChar == 'N' || selection.KeyChar == 'n')
                    {
                        break;
                        //NO EDIT
                    }
                }
                else
                {
                    Console.WriteLine("Record not found");
                    break;
                }
            }
        }
        private void CreateRecord()
        {
            Console.WriteLine("Let's gather some information");

            Console.Write("Please enter student's first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Please enter student's last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Please enter student's email: ");
            string email = Console.ReadLine();

            Console.Write("Please enter student's GPA: ");
            decimal gpa = decimal.Parse(Console.ReadLine());

            Console.Write("Please enter student's credits earned: ");
            int earnedCredits = int.Parse(Console.ReadLine());

            Console.WriteLine($@"
Does this information look correct?

    First name: {firstName}
     Last name: {lastName}
         Email: {email}
           GPA: {gpa}
Credits earned: {earnedCredits}
");
            Console.Write(@"[Y]es [N]o: ");
            ConsoleKeyInfo selection = Console.ReadKey();

            //char selection = char.Parse(Console.ReadLine());
            if (selection.KeyChar == 'Y' || selection.KeyChar == 'y')
            {
                Student createdStudent = new Student(firstName, lastName, email, gpa, earnedCredits);
                students.Add(createdStudent);
                Console.WriteLine($"\n** {firstName} {lastName} was added to the database. **");

                Thread.Sleep(1000);
                /* Sleep for 1 seconds. I did this to delay the main menu popping in
                 * and missing the prompt saying that it was removed successfully.
                 */
            }
            else if (selection.KeyChar == 'N' || selection.KeyChar == 'n')
            {
                //TODO Make this fuction better. Maybe add the incorrect record but then call updated to edit and save it?
                Console.WriteLine("\n");
                CreateRecord();
            }
        }

        public void WriteDataToOutputFile()
        {
            StreamWriter outfile = new StreamWriter("STUDENT_DATABASE_OUTPUT_FILE.txt");
            for (int i = 0; i < students.Count; i++)
            {
                outfile.WriteLine(students[i].ToString());
            }
            outfile.Close();
        }
        public void WriteDataToConsole()
        {
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine((students[i].ToStringForConsole()));
            }
        }
        private void PrintAllRecords()
        {
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine(students[i]);
            }
        }

        private void QuitApplication()
        {
            Environment.Exit(0);
        }
        // code we probably wont use
        public void TestMain()
        {
            // make some objects of type Student
            Student stu01 = new Student("Alice", "Anderson", "aanderson@uw.edu", 3.5m, 90);
            //Student stu02 = new Student("Bob", "Bradshaw", "BBradshaw@uw.edu", 4.0m, 100);
            //Student stu03 = new Student("Steve", "Billman", "SteveBill@uw.edu", 3.0m, 50);
            //Student stu04 = new Student("Bob", "Bobman", "BBobman@uw.edu", 2.8m, 35);

            Undergrad und01 = new Undergrad("Fred", "Fredson", "Fredson@gmail.com", 1.0m, 30, "Freshman");
            //Undergrad und02 = new Undergrad("Mike", "Mikeson", "Mikeson@gmail.com", 2.0m, 50, "Sophomore");
            //Undergrad und03 = new Undergrad("Rick", "Rickson", "Rickson@gmail.com", 3.0m, 70, "Junior");
            //Undergrad und04 = new Undergrad("Tim", "Timson", "Timson@gmail.com", 4.0m, 90, "Senior");

            Gradstudent grd01 = new Gradstudent("James", "Jamesman", "jjamesman@uw.edu", 4.0m, 100, "Beth", "Bethwoman", 200);
            //Gradstudent grd02 = new Gradstudent("Chon", "Chonman", "cchonman@uw.edu", 3.0m, 200, "Roger", "Rogerman", 300);
            //Gradstudent grd03 = new Gradstudent("Howard", "Howardman", "jjman@uw.edu", 2.0m, 300, "Justin", "Justinman", 400);
            //Gradstudent grd04 = new Gradstudent("Max", "Maxman", "mmaxman@uw.edu", 1.0m, 400, "Beth", "Bethwoman", 500);

            students.Add(stu01);
            //students.Add(stu02);
            //students.Add(stu03);
            //students.Add(stu04);
            students.Add(und01);
            //students.Add(und02);
            //students.Add(und03);
            //students.Add(und04);
            students.Add(grd01);
            //students.Add(grd02);
            //students.Add(grd03);
            //students.Add(grd04);
        }
    }
}
