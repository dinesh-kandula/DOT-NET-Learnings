using System.ComponentModel.DataAnnotations;
using TreeDomainLibrary.Models.Enums;

namespace TreeDomainLibrary.Models.ModelsDTO
{
    public class EmployeeDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name should only contain alphabets")]
        public string Name { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        public string JobTitle { get; set; }
        public RoleEnum Role { get; set; }
        public int? ParentId { get; set; }
    }
}
