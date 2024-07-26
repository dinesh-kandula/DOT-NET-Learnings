using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndDS
{
    #region Delegate Declarations
    public delegate bool EligibleToDonate(Student StudentToDonateBlood);

    public delegate T Add<T>(T item1, T item2 );

    #endregion Delegate Declarations

    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }

        public static List<Student> PromoteEmployee(List<Student> students, EligibleToDonate eligibleToDonate)
        {
            List<Student> eligibleStudents = [];

            foreach (Student student in students)
            {
                if (eligibleToDonate(student))
                {
                    eligibleStudents.Add(student);
                    Console.WriteLine($"Student {student.StudentName} can donate blood");
                }
            }
            return eligibleStudents;
        }
    }

    public class Standard
    {
        public int StandardID { get; set; }
        public string StandardName { get; set; }
    }
    
    internal class DelegateLearning
    {
        public void DelegateLearningMethod()
        {
            #region Linq Practice
            IList<Student> studentList = new List<Student>()
            {
                new Student() { StudentID = 1, StudentName = "John", Age = 1 } ,
                new Student() { StudentID = 2, StudentName = "Moin", Age = 1 } ,
                new Student() { StudentID = 3, StudentName = "Bill", Age = 2 } ,
                new Student() { StudentID = 4, StudentName = "Ram", Age = 3 } ,
                new Student() { StudentID = 5, StudentName = "Ron", Age = 1 }
            };

            int SumOfStudentsAge = studentList.Aggregate<Student, int>(0, (s1, s2) => s1 += s2.Age);

            string NameOfStudent = studentList.Aggregate<Student, string>("Student Names: ",
                                                        (str, s) => str += s.StudentName + ",");
            string NameOfStudentNoCommaa = studentList.Aggregate<Student, string, string>("NameOfStudentNoCommaa: ",
                                                        (str, s) => str += s.StudentName + ", ",
                                                        str => str.Substring(0, str.Length - 2).ToUpper()
                                                        );

            Console.WriteLine(SumOfStudentsAge);
            Console.WriteLine(NameOfStudent);
            Console.WriteLine(NameOfStudentNoCommaa);

            #endregion Linq Practice



            Console.WriteLine("-------delaget------");
            //delaget
            Add<int> add = new Add<int>(Sum);
            Console.WriteLine(add(1, 2));
            Add<string> con = new Add<string>(Concate);
            Console.WriteLine(con("dinnu ", "bunny"));
        }

        public static int Sum(int val1, int val2)
        {
            return val1 + val2;
        }

        public static string Concate(string str1, string str2)
        {
            return string.Concat(str1, str2);
        }
    }


}
