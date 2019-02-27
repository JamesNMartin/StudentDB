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
            while (true)
            {
                // display the main menu
                DisplayMainMenu();                
                Console.Write("ENTER SELECTION: ");
                char selection = char.Parse(Console.ReadLine());

                switch (selection)
                {
                    case 'P':
                        //WriteDataToConsole();
                        WriteDataToOutputFile();
                        //PrintAllRecords();
                        break;
                    case 'C':
                        CreateRecord();
                        break;
                    case 'F':
                        FindRecord();
                        break;
                    case 'U'://TODO UPDATE RECORD
                        UpdateRecord();
                        break;
                    case 'D'://TODO DELETE RECORD
                        DeleteRecord();
                        break;
                    case 'Q':
                        QuitApplication();
                        break;
                    case 'T':
                        TestMain();
                        break;
                    default:
                        break;
                }
            }
        }

        private void DeleteRecord()
        {
            
        }
        private void UpdateRecord()
        {

        }
        private void UpdateRecord(int index)
        {
            string newFirstName = students[index].FirstName;
            string newLastName = students[index].LastName;
            string newEmail = students[index].EmailAddress;
            int newStudentID = students[index].StudentID;
            decimal newGPA = students[index].GPA;
            int newCreditsEarned = students[index].CreditsEarned;

            while (true)
            {

                //DisplayEditMenu(index);
                Console.WriteLine($@"
*************** Edit menu ***************

Currently Editting: {students[index].FirstName} {students[index].LastName}

[S]tudent ID: {newStudentID}
[F]irst Name: {newFirstName}
[L]ast Name: {newLastName}
[E]mail Address: {newEmail}
[G]PA: {newGPA}
[C]redits Earned: {newCreditsEarned}

[X]Cancel
[Q]uit and Save
");
                Console.WriteLine("What would you like to update? ");
                Console.Write("ENTER SELECTION: ");
                char selection = char.Parse(Console.ReadLine());
                if (selection == 'Q')//Quitting the edit menu and saving all the new values.
                {
                    students[index].FirstName = newFirstName;
                    students[index].LastName = newLastName;
                    students[index].EmailAddress = newEmail;
                    students[index].StudentID = newStudentID;
                    students[index].GPA = newGPA;
                    students[index].CreditsEarned = newCreditsEarned;

                    break;
                }
                else if (selection == 'X')
                {
                    Console.WriteLine("Cancelling...");
                    break;

                }

                switch (selection)
                {
                    case 'S':
                        Console.Write("Enter new student ID: ");
                        newStudentID = int.Parse(Console.ReadLine());
                        break;
                    case 'F':
                        Console.Write("Enter new first name: ");
                        newFirstName = Console.ReadLine();
                        break;
                    case 'L':
                        Console.Write("Enter new last name: ");
                        newLastName = Console.ReadLine();
                        break;
                    case 'E':
                        Console.Write("Enter new email: ");
                        newEmail = Console.ReadLine();
                        break;
                    case 'G':
                        Console.Write("Enter new GPA: ");
                        newGPA = decimal.Parse(Console.ReadLine());
                        break;
                    case 'C':
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
                string searchTerm = Console.ReadLine();//TODO MAKE IT SO YOU DONT HAVE TO MATCH CASE WHEN SEARCHING

                // FOUND THIS CODE HERE https://stackoverflow.com/questions/3154310/search-list-of-objects-based-on-object-variable
                var foundRecord = students.FirstOrDefault(lookup => lookup.LastName == searchTerm); // USING THE SEARCH TERM FROM USER TO LOOKUP IF THERE IS A RECORD OF THAT PERSON BY LAST NAME.
                var recordIndex = students.FindIndex(lookup => lookup.LastName == searchTerm); // GETTING THE INDEX OF THE RECORD THAT WAS SEARCHED TO USE FOR FURTHER MANIPULATION

                if (foundRecord != null)//if the record is found we display it to the console.
                {
                    Console.WriteLine($"\n** Displaying record for {foundRecord.FirstName} {foundRecord.LastName} **\n");
                    //Console.WriteLine($"Record Index: {recordIndex}");
                    Console.WriteLine(foundRecord.ToString());
                    Console.WriteLine($"Would you like to edit this record?");//prompt user if they want
                    Console.Write("[Y]es [N]o: ");

                    char selection = char.Parse(Console.ReadLine());
                    if (selection == 'Y')
                    {
                        UpdateRecord(recordIndex);
                        break;
                        //EDIT RECORD

                    }
                    else if (selection == 'N')
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
            char selection = char.Parse(Console.ReadLine());
            if (selection == 'Y')//TODO MAYBE MAKE THIS A SWITCH STATEMENT?
            {
                Student createdStudent = new Student(firstName, lastName, email, gpa, earnedCredits);
                students.Add(createdStudent);
                Console.WriteLine($"\n** {firstName} {lastName} was added to the database. **");

                Thread.Sleep(1000);
                /* Sleep for 1 seconds. I did this to delay the main menu popping in
                 * and missing the prompt saying that it was added successfully.
                 */
            }
            else if (selection == 'N')
            {
                //TODO MAKE AN OPTION FOR NO.
                Console.WriteLine("\n");
                CreateRecord();
            }
        }

        public void WriteDataToOutputFile()
        {
            StreamWriter outfile = new StreamWriter("STUDENT_DATABASE_OUTPUT_FILE.txt");
            for (int i = 0; i < students.Count; i++)
            {
                outfile.WriteLine(students[i].ToStringFileFormat());
            }
            outfile.Close();
        }
        public void WriteDataToConsole()
        {
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine((students[i].ToStringFileFormat()));
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
            Student stu01 = new Student("Alice", "Anderson", "aanderson@uw.edu", 3.0m, 90);
            Student stu02 = new Student("Bob", "Bradshaw", "BBradshaw@uw.edu", 4.0m, 80);
            Undergrad und01 = new Undergrad("Bill", "McPoop", "fun@gmail.com", 4.0m, 90, "Senior");
            //Student stu03 = new Student();

            // mutate the objects in some way
            //stu03.FirstName = "Chuck";
            //stu03.LastName = "Costarella";
            //stu03.EmailAddress = "costarec@uw.edu";

            students.Add(stu01);
            students.Add(stu02);
            students.Add(und01);
            //students.Add(stu03);

            // display the data in the objects (easily)
            //Console.WriteLine(stu01);
            //Console.WriteLine(stu02);
            //Console.WriteLine(stu03);
        }
    }
}
