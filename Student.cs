//CHANGE HISTORY
//DATE          DEVELOPER          DESCRIPTION 
//TODO CHNAGE HISTORY
using System;

namespace StudentDB
{
    public class Student : object
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public decimal GPA { get; set; }
        public int CreditsEarned { get; set; }


        public Student(string fName, string lName, string email, decimal gpa, int credits)
        {
            //Random rand = new Random(); // MAYBE USE THIS FOR RANDOM STUDENT ID's
            FirstName = fName;
            LastName = lName;
            EmailAddress = email;
            EnrollmentDate = DateTime.Now;
            GPA = gpa;
            CreditsEarned = credits;
            
        }

        // replace the default ctor with our own default
        public Student()
        {

        }

        public override string ToString()
        {
            string str = string.Empty;
            str += $"      Student ID: {StudentID}";
            str += "\n     First name: " + FirstName;
            str += "\n      Last name: " + LastName;
            str += "\n     Email addr: " + EmailAddress;
            str += "\n       Enrolled: " + EnrollmentDate;
            str += $"\n           GPA: {GPA}";
            str += $"\nCredits Earned: {CreditsEarned}";

            return str;
        }
        public string ToStringFileFormat()
        {
            string str = string.Empty;
            str += $"{StudentID}";
            str += "\n" + FirstName;
            str += "\n" + LastName;
            str += "\n" + EmailAddress;
            str += "\n" + EnrollmentDate;
            str += $"\n{GPA}";
            str += $"\n{CreditsEarned}";

            return str;
        }
    }
}