using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWorkflow.Models.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:250)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name should only contain alphabets")]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [DefaultValue(1)]
        public int CandidateStatus { get; set; }

        [RegularExpression(@"^[1-5](\.[0])?$", ErrorMessage = "Overall rating must be between 1.0 and 5.0")]
        public decimal OverAllRating { get; set; }
        public ICollection<Job>? Jobs { get; set; }
        public string GetFormattedOverAll() => OverAllRating.ToString("0.0");
    }
}
