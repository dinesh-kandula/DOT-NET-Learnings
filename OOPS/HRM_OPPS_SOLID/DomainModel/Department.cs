namespace HRM_OPPS_SOLID.DomainModel
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// One to Many
        /// Each Department have multiples Employees
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
