using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WorkflowCore.Primitives;
using RecruitmentWorkflow.Models.Models.WorkflowData;
using WorkflowCore.Models;

namespace RecruitmentWorkflow.Workflow
{
    public class WorkflowMain : IWorkflow<MyCandidateData>
    {
        public string Id => "RecruitmentWorkflow";
        public int Version => 1;

        public void Build(IWorkflowBuilder<MyCandidateData> builder)
        {
            builder
               .StartWith<ReviewCandidateStep>()
               .If(data => data.CandidateStatus != 5 && data.CurrentStageStatus != 3).Do(then => then
                .Then<TechnicalInterviewL1>()
                .If(data => data.CandidateStatus != 3 && data.CurrentStageStatus != 3).Do(then2 => then2
                    .Then<TechnicalInterviewL2>()
                    .If(data => data.CandidateStatus != 3 && data.CurrentStageStatus != 3).Do(then3 => then3
                        .Then<ManagerRound>()
                        .If(data => data.CandidateStatus != 3 && data.CurrentStageStatus != 3).Do(then4 => then4
                            .Then<HrInterview>()
                            .If(data => data.CandidateStatus != 3 && data.CurrentStageStatus != 3).Do(then5 => then5
                                .Then<OfferLetterRelease>()
                            )
                        )
                    )
                )
            )
               .Then(context =>
               {
                   Console.WriteLine("Goodbye world");
                   return ExecutionResult.Next();
               })
               .EndWorkflow();
        }
    }
}
