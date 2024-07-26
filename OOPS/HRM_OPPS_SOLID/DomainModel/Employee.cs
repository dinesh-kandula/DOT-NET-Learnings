using HRM_OPPS_SOLID.DomainModel.GenericModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM_OPPS_SOLID.DomainModel
{
    /// <summary>
    /// Inheritance I am Inheriting the Person that is genric to employee
    /// </summary>
    public class Employee : Person
    {

        /// <summary>
        /// Encapsulation 
        /// I have created employeeId and monthOfJoining with private keyword,
        /// I am access EmployeeId and MonthOfJoining using get method
        /// If I want to define or update the value the value I am using set method and set method as certain conditions to update.
        /// </summary>
        private int employeeId;
        private int monthOfJoining;

        public int EmployeeId
        {
            get { return employeeId; }
            set
            {
                if (value > 0)
                {
                    employeeId = value + 1000;
                }
                else
                {
                    throw new ArgumentException("Employee ID must be a positive integer.");
                }
            }
        }

        public int MonthOfJoining
        {
            get { return monthOfJoining; }
            set
            {
                if (value >= 1 && value <= 12)
                {
                    monthOfJoining = value;
                }
                else
                {
                    throw new ArgumentException("Month of joining must be between 1 and 12.");
                }
            }
        }

        public string DeparmentId { get; set; }
        public string JobTitle { get; set; }

        public decimal Salary { get; set; }
        public virtual decimal GetSalary()
        {
            return Salary;
        }

        public virtual Department Department { get; set; }
    }
}
