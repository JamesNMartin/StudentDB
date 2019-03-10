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
// Date     Name        Description
//2/28/19   baint       Added create method as well as update method
//3/5/19    baint       Added to create and update method to get them working 
//3/7/19    baint       Added inheritance for Undergrad and Grad students
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    public enum YearRank
    {
        Empty = 0, // I made this as empty because I needed to intialize Year rank to set it correctly. Empty acts as a placeholder for no value.
        Freshman = 1,
        Sophomore = 2,
        Junior = 3,
        Senior = 4
    }
    public class Undergrad : Student
    {
        public virtual float GradePointAverage { get; set; }
        public virtual YearRank Rank { get; set; }

        // ctor for file input.
        public Undergrad (int id, string fName, string lName, string email, DateTime enroll, float gpa, YearRank rank) 
            : base(id, fName, lName, email, enroll)
        {
            GradePointAverage = gpa;
            Rank = rank;
        }
        // ctor for manual record creation.
        public Undergrad(string fName, string lName, string email, float gpa, YearRank rank)
            : base(fName, lName, email)
        {
            GradePointAverage = gpa;
            Rank = rank;
        }
        // I didnt use base.ToString() here because I like the formatting better the way I did it here.
        // Otherwise the data I wanted at the bottom would be on the top and was no in the right order 
        // or format.
        public override string ToString()
        {
            string str = string.Empty;
            str += $"{StudentID}";
            str += "\n" + FirstName;
            str += "\n" + LastName;
            str += "\n" + EmailAddress;
            str += "\n" + EnrollmentDate;
            str += $"\n{GradePointAverage}";
            str += $"\n{Rank}";

            return str;
        }
        // I know I didnt use the base.ToStringForConsole() but I did it because it would print out of format.
        public override string ToStringForConsole()
        {
            string str = string.Empty;
            str += $"Student ID: {StudentID}";
            str += "\nFirst name: " + FirstName;
            str += "\n Last name: " + LastName;
            str += "\nEmail addr: " + EmailAddress;
            str += "\n  Enrolled: " + EnrollmentDate;
            str += $"\n       GPA: {GradePointAverage}";
            str += $"\n Year rank: {Rank}\n";

            return str;
        }
    }
}
