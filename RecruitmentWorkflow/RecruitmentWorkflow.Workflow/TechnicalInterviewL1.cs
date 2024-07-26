using Microsoft.EntityFrameworkCore;
using RecruitmentWorkflow.Models.Models;
using RecruitmentWorkflow.Models.Models.Enums;
using RecruitmentWorkflow.Models.Models.WorkflowData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace RecruitmentWorkflow.Workflow
{
    public class TechnicalInterviewL1 : StepBodyAsync
    {
        private readonly RecruitmentWorkflowContext _dbContext;

        public TechnicalInterviewL1(RecruitmentWorkflowContext dbContext)
        {
            _dbContext = dbContext;
        }
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var data = context.Workflow.Data as MyCandidateData;
            int currentStage = (int) RecritmentStageEnum.TechnicalInterviewL1;
            int currentInterviewStatus = (int) InterviewStatusEnum.InProgress;
            if (data != null)
            {
                var candidate = _dbContext.Candidates.Find(data.CandidateId);
                var interview = _dbContext.Interviews.Where(i => i.CandidateId == data.CandidateId).Where(i => i.RecruitmentStage == currentStage).FirstOrDefault();

                int candidateStatus = (int) CandidateStatusEnum.InProgress;

                if (candidate != null)
                { 
                    candidate.CandidateStatus = candidateStatus;
                    _dbContext.SaveChanges();

                    data.CurrentStage = currentStage;
                    data.CurrentStageStatus = interview.InterviewStatus;
                    data.CandidateStatus = candidate.CandidateStatus;
                }
            }
            return ExecutionResult.Next();
        }
    }
}
