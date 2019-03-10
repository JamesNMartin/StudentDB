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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    public class Gradstudent : Student
    {
        public virtual string AdvisorName { get; set; }
        public virtual decimal TuitionCredit { get; set; }

        public Gradstudent (int id, string fName, string lName, string email, DateTime enroll, string advisor, decimal tuitionCredit) : base(id, fName, lName, email, enroll)
        {
            AdvisorName = advisor;
            TuitionCredit = tuitionCredit;
        }
        public Gradstudent(string fName, string lName, string email, string advisor, decimal tuitionCredit) : base(fName, lName, email)
        {
            AdvisorName = advisor;
            TuitionCredit = tuitionCredit;
        }
        public override string ToString()
        {
            string str = string.Empty;
            str += $"{StudentID}";
            str += "\n" + FirstName;
            str += "\n" + LastName;
            str += "\n" + EmailAddress;
            str += "\n" + EnrollmentDate;
            str += $"\n{AdvisorName}";
            str += $"\n{TuitionCredit}";

            return str;
        }
        public override string ToStringForConsole()
        {
            string str = string.Empty;
            str += $"    Student ID: {StudentID}";
            str += "\n    First name: " + FirstName;
            str += "\n     Last name: " + LastName;
            str += "\n    Email addr: " + EmailAddress;
            str += "\n      Enrolled: " + EnrollmentDate;
            str += $"\n  Advisor Name: {AdvisorName}";
            str += $"\nTuition Credit: {TuitionCredit}\n";

            return str;
        }
    }

    /* GradStudent(s) have a graduate faculty Advisor and they have a financial tuition credit for the teaching they do while in their grad programs. */
}
