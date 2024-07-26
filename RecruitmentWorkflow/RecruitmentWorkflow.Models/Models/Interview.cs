using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWorkflow.Models.Models
{
    public class Interview
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CandidateId { get; set; }
        public Candidate? Candidate { get; set; }
        [Required]
        public int JobId { get; set; }
        public Job? Job { get; set; }
        [Required]
        public int InterviewerId { get; set; }
        public Employee? Interviewer { get; set; }
        [Required]
        public int RecruitmentStage { get; set; }
        public RecruitmentStageMaster? RecruitmentStageMaster { get; set; }
        [Required]
        public int InterviewStatus { get; set; }
        public InterviewStatusMaster? InterviewStatusMaster { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }
        public string ?Comments { get; set; }
    }
}
