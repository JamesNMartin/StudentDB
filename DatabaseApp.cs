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
                        break;
                    case 'D'://TODO DELETE RECORD
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

        private void FindRecord()
        {
            //Get the search term they want using last name for lookup.
            Console.Write("\nPlease enter the last name of the person you're trying to find: ");
            string searchTerm = Console.ReadLine();//TODO MAKE IT SO YOU DONT HAVE TO MATCH CASE WHEN SEARCHING

            // FOUND THIS CODE HERE https://stackoverflow.com/questions/3154310/search-list-of-objects-based-on-object-variable
            var foundRecord = students.FirstOrDefault(lookup => lookup.LastName == searchTerm); // USING THE SEARCH TERM FROM USER TO LOOKUP IF THERE IS A RECORD OF THAT PERSON BY LAST NAME.
            var recordIndex = students.FindIndex(lookup => lookup.LastName == searchTerm); // GETTING THE INDEX OF THE RECORD THAT WAS SEARCHED TO USE FOR FURTHER MANIPULATION

            if (foundRecord != null)
            {
                Console.WriteLine($"\n** Displaying record for {foundRecord.FirstName} {foundRecord.LastName} **\n");
                Console.WriteLine($"Record Index: {recordIndex}");
                Console.WriteLine(foundRecord.ToString());
            } else
            {
                Console.WriteLine("Record not found");
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
                /* Sleep for 2 seconds. I did this to delay the main menu popping in
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
            Student stu01 = new Student("Alice", "Anderson", "aanderson@uw.edu",3.0m, 90);
            Student stu02 = new Student("Bob", "Bradshaw", "BBradshaw@uw.edu", 4.0m, 80);
            Student stu03 = new Student();

            // mutate the objects in some way
            stu03.FirstName = "Chuck";
            stu03.LastName = "Costarella";
            stu03.EmailAddress = "costarec@uw.edu";

            students.Add(stu01);
            students.Add(stu02);
            students.Add(stu03);

            // display the data in the objects (easily)
            //Console.WriteLine(stu01);
            //Console.WriteLine(stu02);
            //Console.WriteLine(stu03);
        }
    }
}
