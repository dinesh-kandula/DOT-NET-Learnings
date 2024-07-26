using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWorkflow.Models.Models
{
    public  class JobCandidates
    {
        [Required]
        public int JobId { get; set; }
        public Job? Job {  get; set; }

        [Required]
        public int CandidateId { get; set; }
        public Candidate? Candidate { get; set; }
    }
}
