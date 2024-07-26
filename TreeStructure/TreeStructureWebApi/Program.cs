using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Reflection;
using TreeStructureWebApi;
using TreeStructureWebApi.Middleware;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main method");
logger.Info("init main method");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCustomServices(builder.Configuration);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI();
    //}

    // app.UseCustMiddleware();
    app.UseMiddleware<ExceptionMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseCors(policy =>
        policy.WithOrigins("https://localhost:7060", "https://localhost:7116")
        .AllowAnyMethod()
        .WithHeaders(HeaderNames.ContentType)
    );

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}