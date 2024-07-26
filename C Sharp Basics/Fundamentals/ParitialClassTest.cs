using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace Fundamentals
{
    public class ParitialClassTest
    {
        public void GetEmployeeDetails()
        {
            /*
             * In Employee is created as partial and also creted another class with same 
             * partial. When I created an instance for the class I could able to access
             * Both properties from 2 parital classes.
             */
            Employee employee = new(20)
            {
                EmpId = 1,
                EmpName = "Test",
                Role = "Admin",
                RollNo = 19
            };

            employee.printId();
        }

    }

    /// <summary>
    /// Employee Partial Class 1
    /// </summary>
    public partial class Employee
    {
        public void printId()
        {
            Console.WriteLine(_id);
        }

        public int EmpId { get; set; }

        public required string EmpName { get; set; }
    }

    /// <summary>
    /// Employee Partial Class 2
    /// </summary>
    public partial class Employee
    {
        private readonly int _id;

        public Employee(int id)
        {
            _id = id;
        }
        
     
        public int RollNo { get; set; }

        public required string Role { get; set; }
    }
}
