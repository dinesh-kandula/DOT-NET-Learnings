using Microsoft.AspNetCore.Mvc;
using RecruitmentWorkflow.Models.Models;

namespace RecruitmentWorkflow.API.Controllers
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
        private readonly RecruitmentWorkflowContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, RecruitmentWorkflowContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("/Master")]
        public IEnumerable<RecruitmentStageMaster> GetMaster()
        {
            return [.. _context.RecruitmentStageMasters];
        }

        [HttpPost("/AddCandidate")]
        public async void AddCandidate([FromForm] Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);

        }
    }
}
