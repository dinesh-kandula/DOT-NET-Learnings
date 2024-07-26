namespace HRM_OPPS_SOLID.DomainModel
{
    /// <summary>
    /// Inheriting Employee Class
    /// </summary>
    public class Manager : Employee
    {
        public int EmployeeCount { get; set; }
        public decimal Bonus { get; set; } 
        
        /// <summary>
        /// Method Overriding, Here we are overrinding GetSalary
        /// </summary>
        /// <returns></returns>
        public override decimal GetSalary()
        {
            return base.GetSalary() + Bonus;
        }
        
        public void HireEmployee(Employee employee)
        {
        }

        public void FireEmployee(Employee employee)
        {
        }

    }
}
