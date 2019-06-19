//CHANGELOG
//DATE             DEVELOPER               DESCRIPTION
//03/19/2019       jmsnmrtn@uw.edu         File created for PhD Student. 
//                                         Created all get and set methods for Thesis topic, Total journal publications, and publications as the author
//                                         Created ctor for PhD Student 
//                                         Created getter for Dissertation dealine
//                                         Created getter for days until deadline
//                                         Created two different ToString methods. One for saving the file and another for printing to the console.    

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    public class PhDStudent : Gradstudent
    {
        public virtual string ThesisTopic { get; set; }//a Thesis topic string that represents his/her research specialization
        public virtual int TotalJournalPublications { get; set; }//a total number of all journal publications 
        public virtual int PublicationsAsAuthor { get; set; }//the number of journal publications that list the PhD candidate as primary author

        //PhDStudent has a fully specified constructor (1 ctor only) that can instantiate and initialize PhDStudent objects correctly
        public PhDStudent (string fName, string lName, string email, string advisor, decimal tuitionCredit, string thesis, int totalPub, int pubAsAuth) 
            : base(fName, lName, email, advisor, tuitionCredit)    
        {
            ThesisTopic = thesis;
            TotalJournalPublications = totalPub;
            PublicationsAsAuthor = pubAsAuth;
        }
        // ctor for file in
        public PhDStudent(int id, string fName, string lName, string email, DateTime enroll, string advisor, decimal tuitionCredit, string thesis, int totalPub, int pubAsAuth)
            : base(id, fName, lName, email, enroll, advisor, tuitionCredit)
        {
            ThesisTopic = thesis;
            TotalJournalPublications = totalPub;
            PublicationsAsAuthor = pubAsAuth;
        }
        //a dissertation deadline which is not stored but calculated by a get only DateTime property that returns the date exactly 1 day prior to 6 years from the enrollment date
        public DateTime DissertationDeadline => EnrollmentDate.AddYears(6).AddDays(-1);
        //PhDStudent has a get only TimeSpan property which calculates the number of whole and partial days until the dissertation deadline
        public double DaysUntilDeadline => DissertationDeadline.Subtract(DateTime.Now).TotalDays;
        //PhDStudent overrides the ToString() expression-bodied method using string interpolation that will correctly trace through Student, GradStudent, and PhDStudent to print all the information about a PhDStudent, including the dissertation deadline and the time span remaining until the deadline as a number of whole and partial days
        public override string ToString()// I know this isnt the base ToString lambda expression but, doing it this way makes sure I have the order I want displayed as well as the format I need to do file in.
        {
            string str = string.Empty;
            str += "PHDSTUDENT";
            str += $"\n{StudentID}";
            str += "\n" + FirstName;
            str += "\n" + LastName;
            str += "\n" + EmailAddress;
            str += "\n" + EnrollmentDate;
            str += $"\n{AdvisorName}";
            str += $"\n{TuitionCredit}";
            str += $"\n{ThesisTopic}";
            str += $"\n{DissertationDeadline}";
            str += $"\n{DaysUntilDeadline}";
            str += $"\n{TotalJournalPublications}";
            str += $"\n{PublicationsAsAuthor}";

            return str;
        }
        //public override string ToString() => base.ToString + 
        //This is the lambda expression you would use otherwise. I dont like it due to the weird order it returns. 
        public override string ToStringForConsole()// I know this isnt the base ToString lambda expression but, doing it this way makes sure I have the order I want displayed as well as the format I need to do file in.
        {
            string str = string.Empty;
            str += $"            Student ID: {StudentID}";
            str += "\n            First name: " + FirstName;
            str += "\n             Last name: " + LastName;
            str += "\n            Email addr: " + EmailAddress;
            str += "\n              Enrolled: " + EnrollmentDate;
            str += $"\n          Advisor Name: {AdvisorName}";
            str += $"\n        Tuition Credit: {TuitionCredit}";
            str += $"\n          Thesis Topic: {ThesisTopic}";
            str += $"\n  Dissertation Dealine: {DissertationDeadline}";
            str += $"\n   Days Until Deadline: {DaysUntilDeadline}";
            str += $"\n    Total Publications: {TotalJournalPublications}";
            str += $"\nPublications as Author: {PublicationsAsAuthor}\n";

            return str;
        }

    }
}
