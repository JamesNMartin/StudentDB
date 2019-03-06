//CHANGE HISTORY
//DATE          DEVELOPER          DESCRIPTION 
//TODO CHANGE HISTORY
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    public class Gradstudent : Student
    {
        public virtual string AdvisorLastName { get; set; }
        public virtual string AdvisorFirstName { get; set; }
        public virtual int TuitionCredit { get; set; }

        public Gradstudent (string fName, string lName, string email, decimal gpa, int credits, string advLast, string advFirst, int tuitionCredit) : base(fName, lName, email, gpa, credits)
        {
            AdvisorLastName = advLast;
            AdvisorFirstName = advFirst;
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
            str += $"\n{GPA}";
            str += $"\n{CreditsEarned}";
            str += $"\n{AdvisorFirstName} {AdvisorLastName}";
            str += $"\n{TuitionCredit}";

            return str;
        }
        public override string ToStringForConsole()
        {
            string str = string.Empty;
            str += $"         Student ID: {StudentID}";
            str += "\n         First name: " + FirstName;
            str += "\n          Last name: " + LastName;
            str += "\n         Email addr: " + EmailAddress;
            str += "\n           Enrolled: " + EnrollmentDate;
            str += $"\n               GPA: {GPA}";
            str += $"\n    Credits Earned: {CreditsEarned}";
            str += $"\nAdvisor First Name: {AdvisorFirstName}";
            str += $"\n Advisor Last Name: {AdvisorLastName}";
            str += $"\n    Tuition Credit: {TuitionCredit}\n";

            return str;
        }
    }

    /* GradStudent(s) have a graduate faculty Advisor and they have a financial tuition credit for the teaching they do while in their grad programs. */
}
