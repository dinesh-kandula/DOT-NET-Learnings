using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Hosting;
using System;
using System.Xml.Linq;
using WorkflowCore.Interface;

namespace WorkflowCoreWebApi
{
    public class MyWorkflow : IWorkflow<MyData>
    {
        public string Id => "MyWorkflow";
        public int Version => 1;

        public void Build(IWorkflowBuilder<MyData> builder)
        {
            builder
                .StartWith(context =>
                {
                    MyData data = context.Workflow.Data as MyData;
                    Console.WriteLine($"Recruitment Process Started for {data.CandidateName}");
                })
                .Then<Screening>()
                .WaitFor("screening", data => data.CandidateID.ToString(), data => DateTime.Now)
                    .Output((step, data) =>
                    {
                        var eData = step.EventData as MyData;
                        ProcessMyEventData(eData, data);
                    })
                 .If(data => data.currentInterviewStatus.ToLower() != "rejected")
                    .Do(then => then
                    .Then<SendMail>()
                    .Then<TechnicalInterview1>()
                    .WaitFor("technicalL1", data => data.CandidateID.ToString(), data => DateTime.Now)
                        .Output((step, data) =>
                        {
                            var eData = step.EventData as MyData;
                            ProcessMyEventData(eData, data);
                        })
                    .If(data => data.currentInterviewStatus.ToLower() != "rejected")
                        .Do(then => then
                        .Then<SendMail>()
                        .Then<TechnicalInterview2>()
                        .WaitFor("technicalL2", data => data.CandidateID.ToString(), data => DateTime.Now)
                            .Output((step, data) =>
                            {
                                var eData = step.EventData as MyData;
                                ProcessMyEventData(eData, data);
                            })
                         .If(data => data.currentInterviewStatus.ToLower() != "rejected")
                            .Do(then => then
                            .Then<SendMail>()
                            .Then<ManagerInterview>()
                            .WaitFor("manager", data => data.CandidateID.ToString(), data => DateTime.Now)
                            .Output((step, data) =>
                            {
                                var eData = step.EventData as MyData;
                                ProcessMyEventData(eData, data);
                            })
                            .If(data => data.currentInterviewStatus.ToLower() != "rejected")
                                .Do(then => then
                                .Then<SendMail>()
                                .Then<OfferLetterRelease>()
                                .Then<SendMail>()
                            )
                        )
                    )
                 ).If(data => data.candidateStatus.ToLower() == "rejected").Do(reject => reject
                    .Then<SendMail>()
                    .EndWorkflow()
                 )
                 .Then(c =>
                 {
                     var data = c.Workflow.Data as MyData;
                     Console.WriteLine($"Recruitment Process Completed for {data.CandidateName}");
                 })
                 .EndWorkflow();
        }

        private static async void ProcessMyEventData(MyData eData, MyData data)
        {
            if (eData != null)
            {
                data.currentInterviewStatus = eData.currentInterviewStatus ?? data.currentInterviewStatus;
                data.candidateStatus = eData.candidateStatus ?? data.candidateStatus;
                await Task.Delay(1000);
            }
            else
            {
                Console.WriteLine("Event Published Wrong Data");
            }
        }
    }

    public class MyTaskWorkflow : IWorkflow<TaskData>
    {
        public string Id => "MyTaskWorkflow";
        public int Version => 1;
        public void Build(IWorkflowBuilder<TaskData> builder)
        {
            builder
                .StartWith(context => Console.WriteLine("Hello"))
                    .If(data => data.Counter >= 1).Do(then => then
                        .Then<Task1>()
                        .Then<AddNumbers>()
                            .Input(step => step.Input1, data => 1)
                            .Input(step => step.Input2, data => 2)
                            .Output(data => data.Counter, step => step.Output)
                        .Then<Task2>()
                        .Then(c => Console.WriteLine("Waiting for My Event to Publish"))
                        .WaitFor("MyEvent", data => "0", data => DateTime.Now.AddSeconds(5))
                            .Output((step, data) =>
                            {
                                var eData = step.EventData as TaskData;
                                if (eData != null)
                                {
                                    data.Value1 = eData.Value1 ?? data.Value1;
                                    data.Value2 = eData.Value2 ?? data.Value2;
                                    data.Counter = eData.Counter == 0 ? data.Counter : eData.Counter;
                                }
                            })
                        .Then(context =>
                        {
                            var data = context.Workflow.Data as TaskData;
                            Console.WriteLine($"Event Consumed - {data.Value1}, {data.Value2}, {data.Counter}");
                        })
                        .Then<RecurringTask>()
                        .ForEach(data => new List<int>() { 1, 2, 3, 4 })
                            .Do(x => x
                                .StartWith(c =>
                                {
                                    Console.WriteLine($"Reading the Foreach List items {c.Item}");
                                })
                            .Then(c => Console.WriteLine("Doing something in ForEach"))
                        )
                        .Parallel()
                            .Do(then => then
                                .Then<ParallelTask0>()
                            )
                            .Do(then =>
                                then.StartWith(c => Console.WriteLine("Parallel Task1 dot1"))
                                    .Then(c => Console.WriteLine("Parallel Task1 dot2"))
                                    .Then(c => Console.WriteLine("Parallel Task1 dot3"))
                             )
                            .Do(then =>
                                then.StartWith(c => Console.WriteLine("Parallel Task2 dot1"))
                                    .Then(c => Console.WriteLine("Parallel Task2 dot2"))
                                    .Then(c => Console.WriteLine("Parallel Task2 dot3"))
                             )
                            .Do(then =>
                                then.StartWith(c => Console.WriteLine("Parallel Task3 dot1"))
                                    .Then(c => Console.WriteLine("Parallel Task3 dot2"))
                                    .Then(c => Console.WriteLine("Parallel Task3 dot3"))
                             )
                        .Join()
                        .Then<Task3>()
                        .Then(context => Console.WriteLine("End"))
                 )
                 .If(data => data.Counter <= 1).Do(reject => reject
                    .Then(context =>
                    {
                        var data = context.Workflow.Data as TaskData;
                        Console.WriteLine($"Counter is less than or equal to zero 0: ${data.Value1} & {data.Value2}");
                    })
                    .EndWorkflow()
                )
                .Then(c => Console.WriteLine("Good Bye"))
                .EndWorkflow();
        }
    }
}
