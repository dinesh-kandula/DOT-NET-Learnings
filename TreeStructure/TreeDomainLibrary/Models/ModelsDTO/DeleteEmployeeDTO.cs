using System.ComponentModel.DataAnnotations;

namespace TreeDomainLibrary.Models.ModelsDTO
{
    public class DeleteEmployeeDTO
    {
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public int NewParentId { get; set; }
    }
}
