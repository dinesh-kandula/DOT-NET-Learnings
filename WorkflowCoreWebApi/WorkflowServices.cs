using WorkflowCore.Interface;
using WorkflowCore.Services;

namespace WorkflowCoreWebApi
{
    public static class WorkflowServices
    {
        public static IServiceCollection AddWorkflowServices(this IServiceCollection services)
        {
            services.AddWorkflow();
            services.AddTransient<Screening>();
            services.AddTransient<TechnicalInterview1>();
            services.AddTransient<TechnicalInterview2>();
            services.AddTransient<ManagerInterview>();
            services.AddTransient<HRInterview>();
            services.AddTransient<OfferLetterRelease>();
            services.AddTransient<SendMail>();

            services.AddTransient<Task1>();
            services.AddTransient<Task2>();
            services.AddTransient<Task3>();

            services.AddSingleton<IWorkflowHost, WorkflowHost>();
            services.AddHostedService<WorkflowService>();
            return services;
        }
    }

    public class WorkflowService : IHostedService
    {
        private readonly IWorkflowHost _workflowHost;

        public WorkflowService(IWorkflowHost workflowHost)
        {
            _workflowHost = workflowHost;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _workflowHost.RegisterWorkflow<MyWorkflow, MyData>();
            _workflowHost.RegisterWorkflow<MyTaskWorkflow, TaskData>();
            _workflowHost.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _workflowHost.Stop();
            return Task.CompletedTask;
        }
    }
}
