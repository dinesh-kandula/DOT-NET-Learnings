using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace RecruitmentWorkflow.Models.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 250)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name should only contain alphabets")]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email {  get; set; }
        [Required]
        public int RoleId { get; set; }
        public EmployeeRoleMaster? Role {  get; set; }

    }
}
