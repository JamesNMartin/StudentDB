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
        public string YearRank { get; set; }

        public Undergrad (string fName, string lName, string email, decimal gpa, int credits, string rank) 
            : base(fName, lName, email, gpa, credits)
        {
            YearRank = rank;
        }
    }
}
