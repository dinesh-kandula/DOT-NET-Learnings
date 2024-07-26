using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using RecruitmentWorkflow.Models.Models;
using RecruitmentWorkflow.Models.Models.WorkflowData;
using RecruitmentWorkflow.Workflow;
using System.Xml.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Services;

namespace RecruitmentWorkflow.API
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            // RabbitMQ
            services.AddSingleton<IConnection>(sp =>
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                return factory.CreateConnection();
            });
            // RabbitMQ
            services.AddSingleton<IModel>(sp =>
            {
                var connection = sp.GetService<IConnection>();
                return connection.CreateModel();
            });

            // Add services to the container.
            services.AddControllers();

            services.AddWorkflow();

            services.AddSingleton<IWorkflowHost, WorkflowHost>();
            services.AddSingleton<IHostedService, WorkflowHostedService>();

            services.AddTransient<ReviewCandidateStep>();
            services.AddTransient<TechnicalInterviewL1>();
            services.AddTransient<TechnicalInterviewL2>();
            services.AddTransient<ManagerRound>();
            services.AddTransient<HrInterview>();
            services.AddTransient<OfferLetterRelease>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Recruitment Workflow API",
                    Description = "An ASP.NET Core Web API for managing Recruitment Workflow APIs",
                });
            });

            services.AddDbContext<RecruitmentWorkflowContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DBConnection"))
            );

            return services;
        }
    }

    public class WorkflowHostedService : IHostedService
    {
        private readonly IWorkflowHost _workflowHost;

        public WorkflowHostedService(IWorkflowHost workflowHost)
        {
            _workflowHost = workflowHost;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _workflowHost.RegisterWorkflow<WorkflowMain, MyCandidateData>();
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
