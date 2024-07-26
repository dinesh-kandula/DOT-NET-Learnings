using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeDomainLibrary.Models.Enums;

namespace TreeDomainLibrary.Models.ModelsDTO
{
    public class EmployeeFlatData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name should only contain alphabets")]
        public required string Name { get; set; }
        [Required]
        public required string EmployeeId { get; set; }
        public string JobTitle { get; set; }
        public RoleEnum Role { get; set; }
        public int? ParentId { get; set; }
        public bool HasChildren { get; set; }
        public int ChildCount {  get; set; }
    }
}
