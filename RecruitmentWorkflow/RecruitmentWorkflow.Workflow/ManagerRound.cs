using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace RecruitmentWorkflow.Workflow
{
    public class ManagerRound : StepBodyAsync
    {
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Console.WriteLine("Conducting Manager Interview");
            await Task.Delay(10);
            return ExecutionResult.Next();

        }
    }
}
