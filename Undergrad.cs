//CHANGE HISTORY
//DATE          DEVELOPER          DESCRIPTION     
//02/26/2019    jmsnmrtn@uw.edu    CREATED UNDERGRAD CLASS
//                                 ADDED YEAR RANK
//                                 ADDED STUDENT INHERITANCE 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    public class Undergrad : Student
    {
        public virtual string YearRank { get; set; }

        public Undergrad (string fName, string lName, string email, decimal gpa, int credits, string rank) 
            : base(fName, lName, email, gpa, credits)
        {
            YearRank = rank;
        }
        public override string ToString()
        {
            string str = string.Empty;
            str += $"{StudentID}";
            str += "\n" + FirstName;
            str += "\n" + LastName;
            str += "\n" + EmailAddress;
            str += "\n" + EnrollmentDate;
            str += $"\n{GPA}";
            str += $"\n{CreditsEarned}";
            str += $"\n{YearRank}";

            return str;
        }
        public override string ToStringForConsole()
        {
            string str = string.Empty;
            str += $"     Student ID: {StudentID}";
            str += "\n     First name: " + FirstName;
            str += "\n      Last name: " + LastName;
            str += "\n     Email addr: " + EmailAddress;
            str += "\n       Enrolled: " + EnrollmentDate;
            str += $"\n            GPA: {GPA}";
            str += $"\n Credits Earned: {CreditsEarned}";
            str += $"\n      Year rank: {YearRank}\n";

            return str;
        }
    }
}
