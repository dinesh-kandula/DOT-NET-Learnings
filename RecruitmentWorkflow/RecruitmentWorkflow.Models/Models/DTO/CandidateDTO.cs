using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWorkflow.Models.Models.DTO
{
    public class CandidateDTO
    {
        [Required]
        [StringLength(maximumLength: 250)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name should only contain alphabets")]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
