using DatabaseFirstApproach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirstApproach.Controllers
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
        private readonly ZeptoContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ZeptoContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var result = await _context.Products.Include(p => p.Carts).ToArrayAsync();
            return Ok(result);
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _context.ZeptoUsers.Select(
                x => new
                {
                    Name = x.FullName,
                    x.Gender,
                    x.UserName,
                    x.Phone,
                    Cart = new
                    {
                        x.Cart!.Id,
                        TotalValue = Math.Floor(x.Cart.Products.Sum(p => p.BasePrice + (p.BasePrice * (p.Offer / 100)))),
                        Products = x.Cart.Products.Select(p => new
                        {
                            p.ProductName,
                            price = p.BasePrice + (p.BasePrice * (p.Offer/100)),
                            p.Quantity,
                            p.Category
                        })
                    }
                }).Where(u => u.Name.ToLower().Equals("loki king")).ToArrayAsync();
            return Ok(result);
        }
    }
}
