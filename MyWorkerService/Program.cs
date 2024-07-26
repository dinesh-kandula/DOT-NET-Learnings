using MyWorkerService;
using RabbitMQ.Client;


var builder = Host.CreateApplicationBuilder(args);

// RabbitMQ IConnection
builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    return factory.CreateConnection();
});

// RabbitMQ IModel
builder.Services.AddSingleton<IModel>(sp =>
{
    var connection = sp.GetService<IConnection>();
    return connection.CreateModel();
});

builder.Services.AddHostedService<Worker>();

// Creating a CancellationTokenSource
var cancellationTokenSource = new CancellationTokenSource();
cancellationTokenSource.CancelAfter(TimeSpan.FromMinutes(2));
/*
 * Here, we're setting a timeout of 2 minutes using the CancelAfter method.
 * This means that after 2 minutes, the CancellationTokenSource will automatically cancel the token,
 * signaling that the operation should stop.
 */



var host = builder.Build();
await host.RunAsync(cancellationTokenSource.Token);