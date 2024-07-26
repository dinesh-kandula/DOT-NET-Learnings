using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace TreeStructureWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConnection _connection;
        private readonly IModel _model;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConnection connection, IModel model)
        {
            _logger = logger;
            _connection = connection;
            _model = model;
        }

        [HttpGet("HitWorkerService/{message}")]
        public IActionResult Get(string message)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            // Send the message to the queue
            var properties = _model.CreateBasicProperties();
            properties.Persistent = true;
            properties.ContentType = "application/json";
            _model.BasicPublish(exchange: "", routingKey: "feedback_queue", basicProperties: properties, body: body);

            return Accepted();
        }
    }
}
