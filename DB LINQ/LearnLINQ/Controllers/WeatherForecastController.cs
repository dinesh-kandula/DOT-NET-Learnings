using LearnLINQ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace LearnLINQ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly LINQDBContext context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, LINQDBContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult> Get()
        {
            ConfigurationOptions options = new ConfigurationOptions
            {
                //list of available nodes of the cluster along with the endpoint port.
                EndPoints = {
        { "localhost", 16379 },
        { "localhost", 16380 },
        // ...
    },
            };



            ConnectionMultiplexer cluster = ConnectionMultiplexer.Connect(options);
            IDatabase db = cluster.GetDatabase();
            db.StringSet("foo", "bar");
            Console.WriteLine(db.StringGet("foo"));

            var x = this.context.Customers.Include(c => c.Orders.Where(o => o.Total > 500)).ThenInclude(o => o.OrderDetails.Where(x => x.Quantity >= 8)).Where(c => c.CustomerId == 1);

            //this.context.Orders.GroupBy(x => x.CustomerId).OrderBy(x => x.Count()).Take(5).ToList();

    //        var x2 = this.context.Customers.Include(x => x.Orders)
    //            .Where(x => x.Orders.Any(x => x.OrderDetails.Any(x => x.Product.ProductName == "Chai")))
    //            .Select(x => new
    //            {
    //                CustomerId = x.CustomerId,
    //                CustomerName = x.CompanyName,
    //                ProductId = x.Orders
    //                    .SelectMany(o => o.OrderDetails)
    //                    .FirstOrDefault(od => od.Product.ProductName == "Chai")!.ProductId,
    //                ProductName = "Chai",
    //                Category = x.Orders
    //                    .SelectMany(o => o.OrderDetails)
    //                    .FirstOrDefault(od => od.Product.ProductName == "Chai")!.Product.Category,
    //                UnitPrice = x.Orders
    //                    .SelectMany(o => o.OrderDetails)
    //                    .FirstOrDefault(od => od.Product.ProductName == "Chai")!.UnitPrice,
    //            })
    //            .ToList();

    //        var x1 = this.context.OrderDetails.GroupBy(od => od.OrderId)
    //            .Select(g => new
    //            {
    //                OrderId = g.Key,
    //                TotalRevenue = g.Sum(od => od.Quantity * od.UnitPrice)
    //            })
    //            .Where(t => t.TotalRevenue > 1000)
    //.OrderByDescending(a => a.TotalRevenue)
    //.Take(3)
    //            .ToList();



            return Ok(x);
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}
