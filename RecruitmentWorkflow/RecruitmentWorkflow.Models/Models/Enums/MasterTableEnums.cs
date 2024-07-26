using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWorkflow.Models.Models.Enums
{
    public enum EmployeeRoleEnum
    {
        Recruiter = 1,
        HiringManager = 2,
        Interviewer = 3,
        HR = 4
    }

    public enum RecritmentStageEnum
    {
        Screening = 1,
        TechnicalInterviewL1,
        TechnicalInterviewL2,
        ManagerInterview,
        HRInterview,
        OfferReleased,
        NotStarted,
    }

    public enum CandidateStatusEnum
    {
        New = 1,
        InProgress = 2,
        Hold = 3,
        Approved = 4,
        Rejected = 5
    }
    public enum InterviewStatusEnum
    {
        InProgress = 1,
        Approved,
        Rejected,
        Hold,
        Skip,
        NotStarted,
    }

}
