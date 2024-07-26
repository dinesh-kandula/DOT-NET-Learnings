using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Primitives;

namespace WorkflowCoreWebApi
{
    /// <summary>
    /// Tasks for MyWorkflow -- Recruitment Process
    /// </summary>
    public class Screening : StepBodyAsync
    {
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            MyData myData = context.Workflow.Data as MyData;
            if(myData != null )
            {
                myData.currentInterviewerName = "Recruiter";
                myData.currentInterviewStage = "Screening";
                myData.currentInterviewStatus = "InProgress";
                myData.candidateStatus = "InProgress";

                await Console.Out.WriteLineAsync($"Hey {myData.CandidateName}, {myData.currentInterviewStage} started, waiting for the interview result.");
            }
            return ExecutionResult.Next();
        }
    }

    public class TechnicalInterview1 : StepBodyAsync
    {
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            MyData myData = context.Workflow.Data as MyData;
            myData.currentInterviewerName = "Interviewer Technical";
            myData.currentInterviewStage = "Technical Interview 1";
            myData.currentInterviewStatus = "InProgress";

            await Console.Out.WriteLineAsync($"Hey {myData.CandidateName}, {myData.currentInterviewStage} started, waiting for the interview result.");
            return ExecutionResult.Next();
        }
    }

    public class TechnicalInterview2 : StepBodyAsync
    {
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            MyData myData = context.Workflow.Data as MyData;
            myData.currentInterviewerName = "Interviewer Technical";
            myData.currentInterviewStage = "Technical Interview 2";
            myData.currentInterviewStatus = "InProgress";

            await Console.Out.WriteLineAsync($"Hey {myData.CandidateName}, {myData.currentInterviewStage} started, waiting for the interview result.");
            return ExecutionResult.Next();
        }
    }

    public class ManagerInterview : StepBodyAsync
    {
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            MyData myData = context.Workflow.Data as MyData;
            myData.currentInterviewerName = "Hiring Manager";
            myData.currentInterviewStage = "Manager Interview";
            myData.currentInterviewStatus = "InProgress";

            await Console.Out.WriteLineAsync($"Hey {myData.CandidateName}, {myData.currentInterviewStage} started, waiting for the interview result.");
            return ExecutionResult.Next();
        }
    }

    public class HRInterview : StepBodyAsync
    {
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            MyData myData = context.Workflow.Data as MyData;
            myData.currentInterviewerName = "HR";
            myData.currentInterviewStage = "HR Interview";
            myData.currentInterviewStatus = "InProgress";

            await Console.Out.WriteLineAsync($"Hey {myData.CandidateName}, {myData.currentInterviewStage} started, waiting for the interview result.");
            return ExecutionResult.Next();
        }
    }

    public class OfferLetterRelease : StepBodyAsync
    {
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            MyData myData = context.Workflow.Data as MyData;
            myData.currentInterviewerName = "HR";
            myData.currentInterviewStage = "Offer Release";
            myData.currentInterviewStatus = "Approved";
            myData.candidateStatus = "Approved";

            await Console.Out.WriteLineAsync($"Hey {myData.CandidateName}, {myData.currentInterviewStage} you have cleared all Interviews, Offer Letter Will Release soon! ");
            return ExecutionResult.Next();
        }
    }

    public class SendMail : StepBodyAsync
    {
        public override Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            MyData myData = context.Workflow.Data as MyData;

            if (myData.candidateStatus.ToLower() == "rejected")
            {
                Console.WriteLine($"**Application Update**: Sorry {myData.CandidateName}, your application has been cancelled. Better luck next time!");
            }
            else if (myData.candidateStatus.ToLower() == "hold")
            {
                Console.WriteLine($"**Application Update**: Hi {myData.CandidateName}, your application has been placed on hold. Please be patient, and thank you for waiting.");
            }
            else if (myData.currentInterviewStatus.ToLower() == "approved" && myData.candidateStatus.ToLower() == "approved")
            {
                Console.WriteLine($"**Job Offer**: Hearty congratulations to you, {myData.CandidateName}! You are now a part of our team. Your offer letter has been released.");
            }
            else if (myData.currentInterviewStatus.ToLower() == "approved" && myData.candidateStatus.ToLower() == "inprogress")
            {
                Console.WriteLine($"**Interview Update**: Congratulations, {myData.CandidateName}! You have successfully cleared the {myData.currentInterviewStage} stage.");
            }

            return Task.FromResult(ExecutionResult.Next());
        }
    }





    /// <summary>
    /// Tasks For MyTaskWorkFlow
    /// </summary>

    public class Task1 : StepBodyAsync
    {
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Enter Value 1: ");
            string input1 = Console.ReadLine();
            await Console.Out.WriteLineAsync("Enter Value 2: ");
            string input2 = Console.ReadLine();

            var data = context.Workflow.Data as TaskData;
            data.Value1 = input1;
            data.Value2 = input2;

            Console.WriteLine("Task 1 executed");
            return ExecutionResult.Next();
        }
    }

    public class AddNumbers : StepBody
    {
        public int Input1 { get; set; }

        public int Input2 { get; set; }

        public int Output { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Output = (Input1 + Input2);
            Console.WriteLine($"Output/Counter == {Output}");
            return ExecutionResult.Next();
        }
    }

    public class Task2 : StepBodyAsync
    {
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var data = context.Workflow.Data as TaskData;

            await Console.Out.WriteLineAsync($"Data from Task2 Value 1: {data.Value1} ; Value 2 {data.Value2} ; Counter : {data.Counter}");

            if (!string.IsNullOrEmpty(data.Value1) && !string.IsNullOrEmpty(data.Value2))
            {

                if (data.Value1 == "Dinnu" && data.Value2 == "Bunny")
                {
                    await Console.Out.WriteLineAsync($"input validated: {data.Value1} & {data.Value2}");
                }
                else
                {
                    await Console.Out.WriteLineAsync($"input not validated: {data.Value1} & {data.Value2}");
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("No Proper Data entered..");
            }
            return ExecutionResult.Next();
        }
    }

    public class RecurringTask : StepBodyAsync
    {
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var data = context.Workflow.Data as TaskData;

            if (!string.IsNullOrEmpty(data.Value1) && !string.IsNullOrEmpty(data.Value2))
            {
                await Console.Out.WriteLineAsync($"Data is also accessed from Recurring Task: {data.Value1} & {data.Value2}");

            }
            while (data.Counter < 5)
            {
                data.Counter++;
                Console.WriteLine($"Doing recurring task - {data.Counter}");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            Console.WriteLine($"Completed Recurring task at - {data.Counter}");
            return ExecutionResult.Next();
        }
    }

    public class Task3 : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"Task 3 executed");
            return ExecutionResult.Next();
        }
    }

    public class ParallelTask0 : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Paralled Task 0 - It need 10 seconds to run");
            Console.WriteLine("Parallel Running");
            int d = 0;
            while(d <= 100000)
            {
                d++;
            }
            Console.WriteLine("Paralled Task 0 - Completed");
            return ExecutionResult.Next();
        }
    }
}

