using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    class DatabaseApp
    {
        static void Main(string[] args)
        {
            //MAKE STUDENTS
            Student stu01 = new Student();
            Student stu02 = new Student();
            Student stu03 = new Student();

            //MANIPULATE

            //OUTPUT
            Console.WriteLine(stu01);
            Console.WriteLine(stu02);
            Console.WriteLine(stu03);
        }
    }
}
