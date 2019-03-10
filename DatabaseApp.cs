///////////////////////////////////////////////////////
///TINFO 200 A, Winter 2019
///UWTacoma
///l6oop - C Sharp Programming Lab 6 - A Student Database App
///This program creates a database for holding student information for a school
///The user can look up a student record to view, edit the record and delete it
///The user will be able to also save any record as well as quit the program
///

////////////////////////////////////////////////
// Change History
// Date     Name                   Description
// 2/28/19   jmsnmrtn@uw.edu       
// 3/5/19    jmsnmrtn@uw.edu       
// 3/7/19    jmsnmrtn@uw.edu      
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
Welcome to the wonderful
    ____        __        __                       ___                ___            __  _           
   / __ \____ _/ /_____ _/ /_  ____ _________     /   |  ____  ____  / (_)________ _/ /_(_)___  ____ 
  / / / / __ `/ __/ __ `/ __ \/ __ `/ ___/ _ \   / /| | / __ \/ __ \/ / / ___/ __ `/ __/ / __ \/ __ \
 / /_/ / /_/ / /_/ /_/ / /_/ / /_/ (__  )  __/  / ___ |/ /_/ / /_/ / / / /__/ /_/ / /_/ / /_/ / / / /
/_____/\__,_/\__/\__,_/_.___/\__,_/____/\___/  /_/  |_/ .___/ .___/_/_/\___/\__,_/\__/_/\____/_/ /_/ 
                                                     /_/   /_/                                       

With this application you can create, save, delete, and maintain a database of student records.
Once you have data in the database you can also update or edit records. All searching uses 
email as the primary key. Saving the database will place a file in your debug/release folder
with all the students that were created. If you use T to test you will have to then save the
database to have it store the file. Using C for manual creation will walk you through the process of
making the student however, when you save the record it will also in turn write it to disk as well. 
Inside your debug folder there will be a file called: STUDENT_DATABASE_OUTPUT_FILE.txt. This contains
all the records in the database you have created or otherwise. This data is less human readable than the
console counterpart due to it also being the input file. (Which isnt complete in this assignment)

NOTE: T or t will populate the database with 4 of each type of student. (Student, Undergrad, Gradstudent)
NOTE: Reading in a file WILL NOT WORK.
");
        }
        //FUNCTIONS - DisplayMainMenu fuctions as a way to create and display a menu to the user in a meaningful way. 
        //PRECONDITIONS -
        //INPUT - 
        //OUTPUT - DisplayMainMenu will simply output a menu to the console
        //POSTCONDITIONS - 
        public void DisplayMainMenu()
        {
            int studentCount = students.Count();
            Console.WriteLine($@"
*********************** Main menu ***********************
** [P]rint/Display all records - {studentCount} record(s)
** [I]mport from file
** [C]reate Record
** [F]ind a Record
** [U]pdate Record
** [D]elete Record
** [S]ave the database
** [Q]uit the application
");
        }
        //FUNCTIONS - GoDatabase will take input from the user on what they would like to do with the application.
        //PRECONDITIONS - 
        //INPUT - Method has no input
        //OUTPUT - Method does not return
        //POSTCONDITIONS - 
        private void GoDatabase()
        {
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
                        break;
                    case 'I':
                    case 'i':
                        ReadDataFromInputFile();
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
                    case 'S':
                    case 's':
                        Console.Write("Saving database to disk");
                        WriteDataToOutputFile();
                        Thread.Sleep(700);
                        Console.WriteLine("...Done!");
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
        //FUNCTIONS - ReadDataFromInputFile reads a .txt document and will automatically add students from the file.
        //PRECONDITIONS - You HAVE to have a .txt file in the debug and/or release folders. It also has to have a
        //                specific type of vaiable on that line or it will crash.
        //INPUT - Method itself has no input however, there is file input here.
        //OUTPUT - Method has no return
        //POSTCONDITIONS - You will have to have read/write on file close or I would imagine it would not save correctly.
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
            }
            //CLOSE THE FILE
            inFile.Close();
        }
        //FUNCTIONS - DeleteRecord will ask the user to search for the record they want to delete by email.
        //            Once found it will confirm with the user that they wish to delete the record.
        //PRECONDITIONS - There must be a record in the database before this becomes usful.
        //INPUT - Method has no input
        //OUTPUT - Method has no return.
        //POSTCONDITIONS - 
        private void DeleteRecord()
        {
            while (true)
            {
                // I used this link to help me search and ignore the case of the letters
                // https://stackoverflow.com/questions/6371150/comparing-two-strings-ignoring-case-in-c-sharp

                Console.Write("Enter the email address of the record you would like to delete: ");
                string searchTerm = Console.ReadLine();
                var foundRecord = (students.FirstOrDefault(lookup => lookup.EmailAddress.Equals(searchTerm, StringComparison.InvariantCultureIgnoreCase))); // USING THE SEARCH TERM FROM USER  TO LOOKUP IF THERE IS A RECORD OF THAT PERSON BY LAST NAME.
                int foundRecordIndex = students.FindIndex(lookup => lookup.EmailAddress.Equals(searchTerm, StringComparison.InvariantCultureIgnoreCase)); // GETTING THE INDEX OF THE RECORD THAT WAS SEARCHED TO USE FOR FURTHER MANIPULATION

                if (foundRecord != null)
                {
                    // RECORD FOUND
                    Console.WriteLine($"\n** Displaying record for {foundRecord.FirstName} {foundRecord.LastName} **\n");
                    Console.WriteLine(foundRecord.ToStringForConsole());
                    Console.WriteLine($"Would you like to delete this record?");//prompt user if they want
                    Console.Write("[Y]es [N]o: ");
                    ConsoleKeyInfo selection = Console.ReadKey();
                    if (selection.KeyChar == 'Y' || selection.KeyChar == 'y')
                    {
                        // DELETE
                        students.RemoveAt(foundRecordIndex);
                        Console.WriteLine($"\n\n** {foundRecord.FirstName} {foundRecord.LastName} was deleted from the database. **");
                        Thread.Sleep(1000);
                        break;
                    }
                    else if (selection.KeyChar == 'N' || selection.KeyChar == 'n')
                    {
                        break;
                        // NO DELETE
                    }
                }
                else
                {
                    break;
                }
            }
        }
        //FUNCTIONS - DeleteRecord will delete any record at the recieved index. This is here to have quick delete from record update.
        //            So on the off chance they go into update and just want to delete all together. Cheap quick and highly distructive.
        //PRECONDITIONS - 
        //INPUT - This method takes in an int for the index. This is used to find the record at that index and delete it.
        //OUTPUT - Method has no return, however a message will appear in the console stating the record was removed.
        //POSTCONDITIONS - 
        private void DeleteRecord(int index)
        {
            Console.WriteLine($"\n\n** {students[index].FirstName} {students[index].LastName} was removed from the database. **");
            students.RemoveAt(index);
            Thread.Sleep(1000);
        }
        //FUNCTIONS - UpdateRecord will send the user to find record because we need to find the record before we can update it.
        //PRECONDITIONS - The database will need some type of data otherwise it wont find anything.
        //INPUT - Method takes no input
        //OUTPUT - Method has no return
        //POSTCONDITIONS - 
        private void UpdateRecord()
        {
            // Sort of a bridge because to update we need to find the file first. And from main menu we have no index 
            // so I cheaply hand it off to FindRecord to get the index we want to edit and then pass that to
            // UpdateRecord(int index).
            FindRecord();
        }
        //FUNCTIONS - UpdateRecord(int index) will take in an int and will get the students type. It will check the type and
        //            walk the user through the process of updating the record.   
        //PRECONDITIONS - 
        //INPUT - This method takes in an int for index. With it we will get the student type at the index and check to see what edit menu
        //        to display on screen. 
        //OUTPUT - Method has no return
        //POSTCONDITIONS - 
        private void UpdateRecord(int index)
        {
            // If statement to see of the student is a Student, Undergrad, or Gradstudent. 
            // Doing this so that I can use all the get / set options for each student.
            if (students[index] is Undergrad)// Chuck told me about the is and as. This works far better then the convoluted way I tried farther down.
            {
                var theUndergradStudent = (Undergrad)students[index]; // Casting as an Undergrad so I can use the get / set for Undergrad
                // Making a defensive copy of the current record in the event that the user only updates one field.
                // This way all the data isnt wiped out when saved.
                string newFirstName = theUndergradStudent.FirstName;
                string newLastName = theUndergradStudent.LastName;
                string newEmail = theUndergradStudent.EmailAddress;
                int newStudentID = theUndergradStudent.StudentID;

                float newGPA = theUndergradStudent.GradePointAverage;
                YearRank newRank = theUndergradStudent.Rank;

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

Currently Editting: {theUndergradStudent.FirstName} {theUndergradStudent.LastName} 

* Student ID:     {newStudentID}
[F]irst Name:     {newFirstName}
[L]ast Name:      {newLastName}
[E]mail Address:  {newEmail}
[G]PA:            {newGPA}
[Y]ear Rank:      {newRank}

[X]Cancel
[D]elete
[Q]uit and Save
");
                    Console.WriteLine("What would you like to update? ");
                    Console.Write("ENTER SELECTION: ");
                    ConsoleKeyInfo selection = Console.ReadKey();
                    //char selection = char.Parse(Console.ReadLine());
                    if (selection.KeyChar == 'Q' || selection.KeyChar == 'q')//Quitting the edit menu and saving all the new values.
                    {

                        theUndergradStudent.FirstName = newFirstName;
                        theUndergradStudent.LastName = newLastName;
                        theUndergradStudent.EmailAddress = newEmail;
                        theUndergradStudent.StudentID = newStudentID;
                        theUndergradStudent.GradePointAverage = newGPA;
                        theUndergradStudent.Rank = newRank;
                        //theStudent.GPA = newGPA;

                        break;
                    }
                    else if (selection.KeyChar == 'X' || selection.KeyChar == 'x')
                    {
                        //Console.WriteLine("Cancelling...");
                        break;
                    }
                    else if (selection.KeyChar == 'D' || selection.KeyChar == 'd')
                    {
                        DeleteRecord(index);

                        break;
                    }
                    switch (selection.KeyChar)
                    {
                        //case 'S':
                        //case 's':
                        //    Console.Write("\nEnter new student ID: ");
                        //    newStudentID = int.Parse(Console.ReadLine());
                        //    break;
                        case 'F':
                        case 'f':
                            Console.Write("\nEnter new first name: ");
                            newFirstName = Console.ReadLine();
                            break;
                        case 'L':
                        case 'l':
                            Console.Write("\nEnter new last name: ");
                            newLastName = Console.ReadLine();
                            break;
                        case 'E':
                        case 'e':
                            Console.Write("\nEnter new email: ");
                            newEmail = Console.ReadLine();
                            break;
                        case 'G':
                        case 'g':
                            Console.Write("\nEnter new GPA: ");
                            newGPA = float.Parse(Console.ReadLine());
                            break;
                        case 'Y':
                        case 'y':
                            Console.WriteLine("\n" +@"
[1] - Freshman
[2] - Sophomore
[3] - Junior
[4] - Senior");
                            Console.Write("Enter new year rank: ");
                            ConsoleKeyInfo yearSelector = Console.ReadKey();
                            switch (yearSelector.KeyChar)
                            {
                                case '1':
                                    newRank = YearRank.Freshman;
                                    break;
                                case '2':
                                    newRank = YearRank.Sophomore;
                                    break;
                                case '3':
                                    newRank = YearRank.Junior;
                                    break;
                                case '4':
                                    newRank = YearRank.Senior;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            Console.WriteLine("** ERROR: Incorrect selection. Please try again **");
                            Thread.Sleep(1000);// Sleep so the user can see the error message
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
            else if (students[index] is Gradstudent)
            {
                Console.WriteLine("Gradstudent");
                var theGradStudent = (Gradstudent)students[index]; // Casting as an Gradstudent so I can use the get / set for Gradstudent     
                // Making a defensive copy of the current record in the event that the user only updates one field.
                // This way all the data isnt wiped out when saved.
                string newFirstName = theGradStudent.FirstName;
                string newLastName = theGradStudent.LastName;
                string newEmail = theGradStudent.EmailAddress;
                int newStudentID = theGradStudent.StudentID;
                string newAdvisor = theGradStudent.AdvisorName;
                decimal newTuitionCredit = theGradStudent.TuitionCredit;

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

Currently Editting: {theGradStudent.FirstName} {theGradStudent.LastName} 

* Student ID:     {newStudentID}
[F]irst Name:     {newFirstName}
[L]ast Name:      {newLastName}
[E]mail Address:  {newEmail}
[A]visor:         {newAdvisor}
[T]uition Credit: {newTuitionCredit}

[X]Cancel
[D]elete
[Q]uit and Save
");
                    Console.WriteLine("What would you like to update? ");
                    Console.Write("ENTER SELECTION: ");
                    ConsoleKeyInfo selection = Console.ReadKey();
                    //char selection = char.Parse(Console.ReadLine());
                    if (selection.KeyChar == 'Q' || selection.KeyChar == 'q')//Quitting the edit menu and saving all the new values.
                    {

                        theGradStudent.FirstName = newFirstName;
                        theGradStudent.LastName = newLastName;
                        theGradStudent.EmailAddress = newEmail;
                        theGradStudent.StudentID = newStudentID;
                        theGradStudent.AdvisorName = newAdvisor;
                        theGradStudent.TuitionCredit = newTuitionCredit;


                        break;
                    }
                    else if (selection.KeyChar == 'X' || selection.KeyChar == 'x')
                    {
                        //Console.WriteLine("Cancelling...");
                        break;
                    }
                    else if (selection.KeyChar == 'D' || selection.KeyChar == 'd')
                    {
                        DeleteRecord(index);

                        break;
                    }
                    switch (selection.KeyChar)
                    {

                        //case 'S':
                        //case 's':
                        //    Console.Write("\nEnter new student ID: ");
                        //    newStudentID = int.Parse(Console.ReadLine());
                        //    break;
                        case 'F':
                        case 'f':
                            Console.Write("\nEnter new first name: ");
                            newFirstName = Console.ReadLine();
                            break;
                        case 'L':
                        case 'l':
                            Console.Write("\nEnter new last name: ");
                            newLastName = Console.ReadLine();
                            break;
                        case 'E':
                        case 'e':
                            Console.Write("\nEnter new email: ");
                            newEmail = Console.ReadLine();
                            break;
                        case 'A':
                        case 'a':
                            Console.Write("\nEnter new advisor name: ");
                            newAdvisor = Console.ReadLine();
                            break;
                        case 'T':
                        case 't':
                            Console.Write("\nEnter new tuition credit: ");
                            newTuitionCredit = decimal.Parse(Console.ReadLine());
                            break;
                        default:
                            Console.WriteLine("** ERROR: Incorrect selection. Please try again **");
                            Thread.Sleep(1000);// Sleep so the user can see the error message
                            break;
                    }
                }
            }
            else
            {
                var theStudent = students[index];//Make the student at the specific index a var 
                // Making a defensive copy of the current record in the event that the user only updates one field.
                // This way all the data isnt wiped out when saved.
                string newFirstName = theStudent.FirstName;
                string newLastName = theStudent.LastName;
                string newEmail = theStudent.EmailAddress;
                int newStudentID = theStudent.StudentID;

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

* Student ID:     {newStudentID}
[F]irst Name:     {newFirstName}
[L]ast Name:      {newLastName}
[E]mail Address:  {newEmail}

[X]Cancel
[D]elete
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

                        //theStudent.GPA = newGPA;

                        break;
                    }
                    else if (selection.KeyChar == 'X' || selection.KeyChar == 'x')
                    {
                        //Console.WriteLine("Cancelling...");
                        break;
                    }
                    else if (selection.KeyChar == 'D' || selection.KeyChar == 'd')
                    {
                        DeleteRecord(index);

                        break;
                    }
                    switch (selection.KeyChar)
                    {
                        //case 'S':
                        //case 's':
                        //    Console.Write("\nEnter new student ID: ");
                        //    newStudentID = int.Parse(Console.ReadLine());
                        //    break;
                        case 'F':
                        case 'f':
                            Console.Write("\nEnter new first name: ");
                            newFirstName = Console.ReadLine();
                            break;
                        case 'L':
                        case 'l':
                            Console.Write("\nEnter new last name: ");
                            newLastName = Console.ReadLine();
                            break;
                        case 'E':
                        case 'e':
                            Console.Write("\nEnter new email: ");
                            newEmail = Console.ReadLine();
                            break;
                        case 'G':
                        case 'g':
                            //Console.Write("Enter new GPA: ");
                            //newGPA = float.Parse(Console.ReadLine());
                            break;
                        case 'C':
                        case 'c':
                            //Console.Write("Enter new credits earned: ");
                            //newCreditsEarned = int.Parse(Console.ReadLine());
                            break;
                        default:
                            Console.WriteLine("** ERROR: Incorrect selection. Please try again **");
                            Thread.Sleep(1000);// Sleep so the user can see the error message
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
            /* THIS WAS OLD AND A VERY WRONG WAY TO DO IT. LEFT IT HERE TO DOCUMENT MY STRUGGLE
             * 
             * 
             * 
             * 
             I could not for the life of me figure out how to get Undergrad GPA and YearRank.
             It seems like it could be straight forward but was not. Chuck said something about
             casting the student as a Undergrad but I couldnt figure it out. He also said I could
             try to use Is and As but that was just as confusing as this method.
             I used this http://mcgivery.com/c-reflection-get-property-value-of-nested-classes/
             I'm sure this is a wildly wrong way to do it but I found no other way.
             float newGPA = (float)theStudent.GetType().GetProperty("GradePointAverage").GetValue(theStudent, null);
             */
        }
        //FUNCTIONS - FindRecord will walk the user through finding a record by the email of the student. 
        //            Once the record is found the user is asked if they would like to edit the record.
        //PRECONDITIONS - 
        //INPUT - Method has no input
        //OUTPUT - 
        //POSTCONDITIONS - 
        private void FindRecord()
        {
            while (true)
            {
                //Get the search term they want using last name for lookup.
                Console.Write("\nPlease enter the email address of the person you're trying to find: ");
                string searchTerm = Console.ReadLine();

                // FOUND THIS CODE HERE https://stackoverflow.com/questions/3154310/search-list-of-objects-based-on-object-variable
                var foundRecord = students.FirstOrDefault(lookup => lookup.EmailAddress.TrimEnd().Equals(searchTerm, StringComparison.InvariantCultureIgnoreCase)); // USING THE SEARCH TERM FROM USER  TO LOOKUP IF THERE IS A RECORD OF THAT PERSON BY LAST NAME.
                int foundRecordIndex = students.FindIndex(lookup => lookup.EmailAddress.Equals(searchTerm, StringComparison.InvariantCultureIgnoreCase)); // GETTING THE INDEX OF THE RECORD THAT WAS SEARCHED TO USE FOR FURTHER MANIPULATION

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
        //FUNCTIONS - CreateRecord will walk the user through the creation of one of three record. When the record has been created it is written to 
        //            a .txt file located in the debug folder of this project.
        //PRECONDITIONS - 
        //INPUT - Method has no input
        //OUTPUT - Method has no return
        //POSTCONDITIONS - 
        private void CreateRecord()
        {
            Console.WriteLine("Let's gather some information");
            Console.WriteLine("What type of student record would you like to create: ");
            Console.WriteLine(@"
[1] - Student
[2] - Undergraduate Student
[3] - Graduate Student
");
            Console.Write("ENTER SELECTION: ");
            ConsoleKeyInfo yearSelector = Console.ReadKey();
            switch (yearSelector.KeyChar)
            {
                case '1':
                    // STUDENT: First, Last, Email.
                    string stuEmail = string.Empty;
                    bool stuEmailBool = false;

                    do
                    {
                        Console.Write("\nPlease enter an email address you would like to use: ");
                        stuEmail = Console.ReadLine();

                        for (int i = 0; i < students.Count; i++)
                        {
                            if (students[i].EmailAddress == stuEmail)
                            {
                                stuEmailBool = true;
                                Console.WriteLine($"\n** {stuEmail} is already in use. Please enter a different email. **");
                                Thread.Sleep(1000);
                                break;
                            }
                        }
                    } while (stuEmailBool);
                    Console.Write("Please enter student's first name: ");
                    string stuFirstName = Console.ReadLine();

                    Console.Write("Please enter student's last name: ");
                    string stuLastName = Console.ReadLine();

                    //Console.Write("Please enter student's email: ");
                    //string stuEmail = Console.ReadLine();

                    Console.WriteLine($@"
Does this information look correct?

    First name: {stuFirstName}
     Last name: {stuLastName}
         Email: {stuEmail}");
                    Console.Write(@"[Y]es [N]o: ");
                    ConsoleKeyInfo stuSelection = Console.ReadKey();

                    //char selection = char.Parse(Console.ReadLine());
                    if (stuSelection.KeyChar == 'Y' || stuSelection.KeyChar == 'y')
                    {
                        Student createdStudent = new Student(stuFirstName, stuLastName, stuEmail);
                        students.Add(createdStudent);
                        Console.WriteLine($"\n\n** {stuFirstName} {stuLastName} was added to the database. **");
                        Console.WriteLine($@"Your student ID is: {createdStudent.StudentID}");
                        WriteDataToOutputFile();
                        Thread.Sleep(1000);
                        /* Sleep for 1 seconds. I did this to delay the main menu popping in
                         * and missing the prompt saying that it was removed successfully.
                         */
                    }
                    else if (stuSelection.KeyChar == 'N' || stuSelection.KeyChar == 'n')
                    {
                        // If I had more time I would add the record anyway then get the index of 
                        // the record that was just created and send that to UpdateRecord. That way
                        // the user doesnt have to enter all the other info back in to fix it. And 
                        // we wouldnt be touching the student ID either making it easier to keep track of IDs.
                        Console.WriteLine("\n");
                        CreateRecord();
                    }

                    break;
                case '2':
                    // UNDERGRAD: STUDENT + YearRank, GPA
                    string undEmail = string.Empty;
                    bool undEmailBool = false;

                    do
                    {
                        Console.Write("\nPlease enter an email address you would like to use: ");
                        undEmail = Console.ReadLine();

                        for (int i = 0; i < students.Count; i++)
                        {
                            if (students[i].EmailAddress == undEmail)
                            {
                                undEmailBool = true;
                                Console.WriteLine($"\n** {undEmail} is already in use. Please enter a different email. **");
                                Thread.Sleep(1000);
                                break;
                            }
                        }
                    } while (undEmailBool);

                    Console.Write("Please enter student's first name: ");
                    string undFirstName = Console.ReadLine();

                    Console.Write("Please enter student's last name: ");
                    string undLastName = Console.ReadLine();

                    //Console.Write("Please enter student's email: ");
                    //string undEmail = Console.ReadLine();

                    Console.Write("Please enter student GPA Ex.(2.0, 3.5, 4.0): ");
                    float undGPA = float.Parse(Console.ReadLine());

                    // Made comment on this in the Undergrad.cs
                    YearRank rank = YearRank.Empty;

                    Console.WriteLine(@"
[1] - Freshman
[2] - Sophomore
[3] - Junior
[4] - Senior
");
                    Console.Write("Please enter student year rank: ");
                    ConsoleKeyInfo rankSelector = Console.ReadKey();
                    switch (rankSelector.KeyChar)
                    {
                        case '1':
                            rank = YearRank.Freshman;
                            break;
                        case '2':
                            rank = YearRank.Sophomore;
                            break;
                        case '3':
                            rank = YearRank.Junior;
                            break;
                        case '4':
                            rank = YearRank.Senior;
                            break;
                        default:
                            Console.WriteLine("** Invalid option. Please try again. **");
                            break;
                    }
                    Console.WriteLine($@"
Does this information look correct?


    First name: {undFirstName}
     Last name: {undLastName}
         Email: {undEmail}
           GPA: {undGPA}
     Year rank: {rank}
");
                    Console.Write(@"[Y]es [N]o: ");
                    ConsoleKeyInfo undSelection = Console.ReadKey();

                    //char selection = char.Parse(Console.ReadLine());
                    if (undSelection.KeyChar == 'Y' || undSelection.KeyChar == 'y')
                    {
                        Student createdStudent = new Undergrad(undFirstName, undLastName, undEmail, undGPA, rank);
                        students.Add(createdStudent);
                        Console.WriteLine($"\n\n** {undFirstName} {undLastName} was added to the database. **");
                        Console.WriteLine($@"Your student ID is: {createdStudent.StudentID}");
                        WriteDataToOutputFile();
                        Thread.Sleep(1000);
                        /* Sleep for 1 seconds. I did this to delay the main menu popping in
                         * and missing the prompt saying that it was removed successfully.
                         */
                    }
                    else if (undSelection.KeyChar == 'N' || undSelection.KeyChar == 'n')
                    {
                        // If I had more time I would add the record anyway then get the index of 
                        // the record that was just created and send that to UpdateRecord. That way
                        // the user doesnt have to enter all the other info back in to fix it. And 
                        // we wouldnt be touching the student ID either making it easier to keep track of IDs.
                        Console.WriteLine("\n");
                        CreateRecord();
                    }
                    break;
                case '3':
                    // GRADSTUDENT: STUDENT + Advisor name, Tuition Credit
                    // Gathering input from user
                    string grdEmail = string.Empty;
                    bool grdEmailBool = false;

                    do
                    {
                        Console.Write("\nPlease enter an email address you would like to use: ");
                        grdEmail = Console.ReadLine();

                        for (int i = 0; i < students.Count; i++)
                        {
                            if (students[i].EmailAddress == grdEmail)
                            {
                                grdEmailBool = true;
                                Console.WriteLine($"\n** {grdEmail} is already in use. Please enter a different email. **");
                                Thread.Sleep(1000);
                                break;
                            }
                        }
                    } while (grdEmailBool);
                    Console.Write("Please enter student's first name: ");
                    string grdFirstName = Console.ReadLine();

                    Console.Write("Please enter student's last name: ");
                    string grdLastName = Console.ReadLine();

                    //Console.Write("Please enter student's email: ");
                    //string gradEmail = Console.ReadLine();

                    Console.Write("Please enter advisor name: ");
                    string advName = Console.ReadLine();

                    Console.Write("Enter tuition credit: ");
                    decimal tuitionCredit = decimal.Parse(Console.ReadLine());

                    // Verify with user that the information entered is correct.
                    Console.WriteLine($@"
Does this information look correct?


    First name: {grdFirstName}
     Last name: {grdLastName}
         Email: {grdEmail}
  Advisor Name: {advName}
Tuition Credit: {tuitionCredit}

");
                    Console.Write(@"[Y]es [N]o: ");
                    ConsoleKeyInfo gradSelection = Console.ReadKey();

                    if (gradSelection.KeyChar == 'Y' || gradSelection.KeyChar == 'y')
                    {

                        Student createdStudent = new Gradstudent(grdFirstName, grdLastName, grdEmail, advName, tuitionCredit);
                        students.Add(createdStudent);
                        Console.WriteLine($"\n\n** {grdFirstName} {grdLastName} was added to the database. **");
                        Console.WriteLine($@"Your student ID is: {createdStudent.StudentID}");
                        WriteDataToOutputFile();//When we complete the add to the list we will save right after to have a current record on file
                        Thread.Sleep(1000);
                        /* Sleep for 1 seconds. I did this to delay the main menu popping in
                         * and missing the prompt saying that it was removed successfully.
                         */
                    }
                    else if (gradSelection.KeyChar == 'N' || gradSelection.KeyChar == 'N')
                    {
                        // If I had more time I would add the record anyway then get the index of 
                        // the record that was just created and send that to UpdateRecord. That way
                        // the user doesnt have to enter all the other info back in to fix it. And 
                        // we wouldnt be touching the student ID either making it easier to keep track of IDs.
                        Console.WriteLine("\n");
                        CreateRecord();
                    }
                    break;
                default:
                    break;
            }
        }
        //FUNCTIONS - WriteDataToOutputFile will write the contents of the database to a .txt file located in the debug folder
        //            in this project folder.
        //PRECONDITIONS - 
        //INPUT - Method has no input
        //OUTPUT - Method has no return
        //POSTCONDITIONS - 
        public void WriteDataToOutputFile()
        {
            // Create a file name and save database to the file
            StreamWriter outfile = new StreamWriter("STUDENT_DATABASE_OUTPUT_FILE.txt");
            for (int i = 0; i < students.Count; i++)
            {
                outfile.WriteLine(students[i].ToString());
            }
            outfile.Close();
        }
        //FUNCTIONS - WriteDataToConsole will print the database to the console window in a human readable format.
        //PRECONDITIONS - 
        //INPUT - Method has no input
        //OUTPUT - Method has no reurn
        //POSTCONDITIONS - 
        public void WriteDataToConsole()
        {
            for (int i = 0; i < students.Count; i++)
            {
                // Will go through all the students and print them to the console
                Console.WriteLine((students[i].ToStringForConsole()));
            }
        }
        //FUNCTIONS - QuitApplication will close the program
        //PRECONDITIONS -
        //INPUT - Method takes no input
        //OUTPUT - Method has no return
        //POSTCONDITIONS - 
        private void QuitApplication()
        {
            // Quit application
            Environment.Exit(0);
        }
        //FUNCTIONS - TestMain will add 4 of each type of student. It is very useful for adding data quickly to debug the program
        //PRECONDITIONS - 
        //INPUT - Method has no input
        //OUTPUT - Method has no return
        //POSTCONDITIONS - 
        public void TestMain()
        {
            // make some objects of type Student
            //Student stu01 = new Student();
            Student stu01 = new Student("Ralph", "Crouch", "crouch@uw.edu");
            Student stu02 = new Student("Ewen", "Bullock", "bullock@gmail.com");
            Student stu03 = new Student("Nisha", "Brewer", "brewer@yahoo.com");
            Student stu04 = new Student("Milo","Estes","estes@gmail.com");
           
            //Student und01 = new Undergrad();
            Student und01 = new Undergrad("James", "Martin", "jmsnmrtn@uw.edu", 3.6f, YearRank.Junior);
            Student und02 = new Undergrad("Vick", "Vinegar", "vinegar@icloud.com", 2.1f, YearRank.Freshman);
            Student und03 = new Undergrad("Hugh", "Honey", "honey@live.com", 1.5f, YearRank.Sophomore);
            Student und04 = new Undergrad("Ronald", "McDonald", "mcdonald@aol.com", 4.0f, YearRank.Senior);

            //Student grd01 = new Gradstudent();
            Student grd01 = new Gradstudent("Bob", "Bobman", "bobman@uw.edu", "Dr. Donald Chin", 1111.99m);
            Student grd02 = new Gradstudent("Vinnie","Zimmerman","Zimmerman@uw.edu","Dr. Steve Branch",2222.99m);
            Student grd03 = new Gradstudent("Clark","Gilmore","gilmore@uw.edu","Dr. Darla Summers",3333.99m);
            Student grd04 = new Gradstudent("Missy","Monroe","monroe@uw.edu","Dr. Patys Cain",4444.99m);
            // Add all created students
            students.Add(stu01);
            students.Add(stu02);
            students.Add(stu03);
            students.Add(stu04);
            students.Add(und01);
            students.Add(und02);
            students.Add(und03);
            students.Add(und04);
            students.Add(grd01);
            students.Add(grd02);
            students.Add(grd03);
            students.Add(grd04);
        }
    }
}
