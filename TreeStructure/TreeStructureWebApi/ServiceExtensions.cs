using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System.Reflection;
using System.Text.Json.Serialization;
using TreeDomainLibrary.Models;
using TreeDomainLibrary.Repositories;
using TreeDomainLibrary.Services;

namespace TreeStructureWebApi
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

            
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Employee Tree Structure Web API",
                    Description = "An ASP.NET Core Web API for managing Employee Tree Structure APIs",
                });
                //Set the comments path for the Swagger JSON and UI.
                // Go to the project properties > Build > Supress specific warnings > (Enter) 1701;1702;1591 
                // > Output > Documentation File (Check)
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            // DB
            services.AddDbContext<OfficeContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DBConnection"))
            );

            //Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
