using Microsoft.EntityFrameworkCore;
using RecruitmentWorkflow.Models.Models;
using RecruitmentWorkflow.Models.Models.Enums;
using RecruitmentWorkflow.Models.Models.WorkflowData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace RecruitmentWorkflow.Workflow
{
    public class ReviewCandidateStep : StepBodyAsync
    {
        private readonly RecruitmentWorkflowContext _dbContext;
        //private readonly EmailService _emailService;

        public ReviewCandidateStep(RecruitmentWorkflowContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var data = context.Workflow.Data as MyCandidateData;
            int currentStage = (int) RecritmentStageEnum.Screening;
            int crrentStateStatus = (int) InterviewStatusEnum.InProgress;
            if (data != null)
            {
                var candidate = _dbContext.Candidates.Find(data.CandidateId);

                if (candidate != null)
                {

                    var interviewerId = await _dbContext.Employees.Where(e => e.RoleId == (int)EmployeeRoleEnum.Recruiter).Select(e => e.Id).FirstOrDefaultAsync();
                    var interview = new Interview
                    {
                        CandidateId = data.CandidateId,
                        InterviewerId = interviewerId,
                        RecruitmentStage = currentStage,
                        InterviewStatus = crrentStateStatus
                    };
                    _dbContext.SaveChanges();
                    int candidateStatus = (int)CandidateStatusEnum.InProgress;

                    candidate.CandidateStatus = candidateStatus;
                    data.CurrentStage = currentStage;
                    _dbContext.SaveChanges();
                }
            }
            return ExecutionResult.WaitForEvent("CheckStatus", data.CandidateId.ToString(), effectiveDate: DateTime.Parse("2024-18-7"));
        }
    }
}
