//TINFO 200 A, Winter 2019
//l6oop - C Sharp Programming Lab 6 - A Student Database App
//This program creates a database for holding student information for a school
//The user can look up a student record to view, edit the record and delete it
//The user will be able to also save any record as well as quit the program
//
// Change History
// Date         Name                  Description
// 02/19/2019   jmsnmrtn@uw.edu       Project creation and push to github
//                                    Created Student class          
// 02/25/2019   jmsnmrtn@uw.edu       Created Undergrad and Gradstudent classes  
//                                    Added Gradstudent class
//                                    Implimented find record
//                                    Implimented create record
//                                    Updated ToString methods
//                                    Updated application header(Not complete)
// 02/25/2019   jmsnmrtn@uw.edu       Updated many of the methods in the DatabaseApp.cs
// 02/26/2019   jmsnmrtn@uw.edu       Updated UpdateRecord()
//                                    Updated FindRecord()
//                                    Finished ToString()
// 02/28/2019   jmsnmrtn@uw.edu       Finished up DeleteRecord()
//                                    Made changes to update record
// 03/05/2019   jmsnmrtn@uw.edu       Finished Gradstudent Class
//                                    Created DeleteRecord(int index) method
// 03/09/2019   jmsnmrtn@uw.edu       Added Comments to code
//                                    Finished all update methods
//                                    Save record to disk on record creation
//                                    Got project ready for submission
//                                    Final push to github
using System;
using System.Threading;

namespace StudentDB
{
    public class Student : object
    {
        static int Count = 0;
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // ctor for manual creation
        public Student(string fName, string lName, string email)
        {
            StudentID = ++Count;
            FirstName = fName;
            LastName = lName;
            EmailAddress = email;
            EnrollmentDate = DateTime.Now;
        }
        // ctor for file in.
        public Student(int id, string fName, string lName, string email, DateTime enroll)
        {
            //Console.WriteLine($"Generating Student ID for {fName} {lName}");
            //Random rand = new Random(); // Not sure how you wanted to enter student IDs. I just made them random numbers.
            //Thread.Sleep(1000);//SINCE RANDOM NUMBERS ARE MADE BASED ON SYSTEM TIME, I FORCE A WAIT OF ONE SEC TO GET A BETTER RANDOM NUMBER THAT ISNT THE SAME.
            StudentID = id;
            Count = StudentID; // This should make the counter for ID numbers keep going after file import.
            FirstName = fName;
            LastName = lName;
            EmailAddress = email;
            EnrollmentDate = enroll;
        }

        // replace the default ctor with our own default
        public override string ToString()
        {
            string str = string.Empty;
            str += "STUDENT";
            str += $"\n{StudentID}";
            str += "\n" + FirstName;
            str += "\n" + LastName;
            str += "\n" + EmailAddress;
            str += "\n" + EnrollmentDate;

            return str;
        }
        public virtual string ToStringForConsole()
        {
            string str = string.Empty;
            str += $"Student ID: {StudentID}";
            str += "\nFirst name: " + FirstName;
            str += "\n Last name: " + LastName;
            str += "\nEmail addr: " + EmailAddress;
            str += "\n  Enrolled: " + EnrollmentDate + "\n";

            return str;
        }
    }
}