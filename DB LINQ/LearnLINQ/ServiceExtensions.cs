using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using LearnLINQ.Models;


namespace LearnLINQ
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<LINQDBContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DBConnection"))
            );

            return services;
        }
    }
}
