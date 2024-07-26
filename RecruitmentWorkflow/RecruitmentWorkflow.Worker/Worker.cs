using RecruitmentWorkflow.Models.Models;
using RecruitmentWorkflow.Models.Models.Enums;
using RecruitmentWorkflow.Models.Models.WorkflowData;
using System.Xml.Linq;
using WorkflowCore.Interface;

namespace RecruitmentWorkflow.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IServiceProvider _serviceProvider;
        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<RecruitmentWorkflowContext>();
                    var workflowHost = scope.ServiceProvider.GetRequiredService<IWorkflowHost>();

                    var candidates = dbContext.Candidates
                        .Where(c => c.CandidateStatus == (int)CandidateStatusEnum.InProgress || c.CandidateStatus == (int) CandidateStatusEnum.Hold)
                        .ToList();

                    foreach (var candidate in candidates)
                    {
                        var workflowInstance = await workflowHost.PersistenceStore.GetWorkflowInstance(candidate.Id.ToString());
                        if (workflowInstance != null)
                        {
                            var workflowData = new MyCandidateData
                            {
                                CandidateId = candidate.Id,
                                CandidateName = candidate.Name,
                                CandidateEmail = candidate.Email,
                                CurrentStage = candidate.CurrentStage,
                                CurrentStageStatus = candidate.Status,
                                CandidateStatus = candidate.CandidateStatus
                            };
                            workflowHost.PublishEvent("CheckStatus", candidate.Id.ToString(), workflowData);
                        }
                    }
                }

                await Task.Delay(10000, stoppingToken); // Poll every 10 seconds
            }
        }
    }
}
