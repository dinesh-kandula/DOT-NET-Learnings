using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeDomainLibrary.Models.Enums;

namespace TreeDomainLibrary.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public string JobTitle { get; set; }
        public RoleEnum Role { get; set; }
        public int? ParentId { get; set; }
        public Employee Parent { get; set; }
        public ICollection<Employee> DirectReports { get; set; }
    }
}
