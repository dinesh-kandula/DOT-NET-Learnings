using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Unicode;
using System.Threading.Channels;
using System.Xml.Linq;

namespace MyWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConnection _connection;
        private readonly IModel _model;

        public Worker(ILogger<Worker> logger, IConnection connection, IModel model)
        {
            _logger = logger;
            _connection = connection;
            _model = model;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             /*
              * Declare Multiple Queues:
              * Declare all necessary queues within the ExecuteAsync method or in the service initialization.
              */
            _model.QueueDeclare("feedback_queue", true, false, false, null);
            _model.QueueDeclare("order_queue", true, false, false, null);
            /*
             * This line ensures that the queue named "feedback_queue" exists.If it doesn't exist, it will be created with the specified parameters:
             * Durable(true): The queue will survive a broker restart.
             * Exclusive(false): The queue can be accessed by other connections.
             * Auto - delete(false): The queue will not be deleted when there are no more connections.
             * Arguments(null): Additional arguments can be passed, but null indicates none.
             */

            // Create a consumer 
            var consumer = new EventingBasicConsumer(_model);
            // An instance of EventingBasicConsumer is created to listen for messages from the specified queue.


            /*
             * This event handler is set up to process messages when they are received.
             * The Received event is triggered whenever a message is delivered to the consumer.
             */
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray(); // Convert to byte array
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received: {message}");
                /*
                 * The message body is retrieved and converted from a byte array to a string using UTF-8 encoding.
                 * The message is then printed to the console.
                 */

                // Acknowledge the message
                _model.BasicAck(e.DeliveryTag, false);
                /*
                 * This line sends an acknowledgment to RabbitMQ indicating that the message has been successfully processed.
                 * This prevents the message from being redelivered.
                 */
            };

            /*Start Consuming Messages from the Queue*/
            _model.BasicConsume(queue: "feedback_queue", autoAck: false, consumer: consumer);
            /*
             * This starts the consumer to begin receiving messages from the specified queue.
             * The autoAck parameter is set to false to ensure that messages are manually acknowledged.
             */

            // Consumer for order queue
            var orderConsumer = new EventingBasicConsumer(_model);
            orderConsumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Order Received: {message}");
                _model.BasicAck(e.DeliveryTag, false);
            };
            _model.BasicConsume(queue: "order_queue", autoAck: false, consumer: orderConsumer);

            // A PeriodicTimer is initialized to tick every 10 days. This timer is used to trigger the periodic database operation.
            var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

            /*
             * Continuously Check for Messages until Cancellation is Requested
             * This loop ensures that the worker service continuously checks for messages and processes them.
             * The loop will run until the stoppingToken signals a cancellation, allowing for graceful shutdown of the service.
             */
            while (!stoppingToken.IsCancellationRequested)
            {
                /*
                 * Perform periodic DB operation if timer has elapsed.
                 * Inside the main loop, the WaitForNextTickAsync method waits for the timer to tick.
                 * When it does, the PerformDbOperation method is called.
                */              
                if (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    _logger.LogInformation("Performing scheduled database operation for every 5 minutes...");
                    Console.WriteLine("Performing scheduled database operation for every 5 minutes...");
                }

                // Continue processing messages
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
