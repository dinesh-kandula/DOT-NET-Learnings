using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWorkflow.Models.Models.WorkflowData
{
    public class MyCandidateData
    {
        [Key]
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string? CandidateName { get; set; }
        public string?  CandidateEmail { get; set; }
        public int CurrentInterviewerId { get; set; }
        public int CurrentStage { get; set; }
        public int CurrentStageStatus { get; set; }
        public int CandidateStatus { get; set; }
    }
}
