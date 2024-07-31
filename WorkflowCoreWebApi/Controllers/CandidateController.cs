using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WorkflowCore.Interface;
using WorkflowCoreWebApi.Services;

namespace WorkflowCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly IWorkflowHost _workflowHost;
        private readonly IRedisService _redisService;
        private readonly IConnectionMultiplexer _redis;
        private readonly ILogger<CandidateController> _logger;
        private const string _CandidateListKey = "candidateList";
        public CandidateController(IWorkflowHost workflowHost, IRedisService redisService, IConnectionMultiplexer redis, ILogger<CandidateController> logger)
        {
            _workflowHost = workflowHost;
            _redisService = redisService;
            _redis = redis;
            _logger = logger;
        }

        #region Redis Message Queue
        /*
         Use the Message with Worker Service
        */

        //[HttpPost("publish")]
        //public async Task PublishMessageAsync([FromQuery][Required]string channel, [FromQuery, Required]string message)
        //{
        //    var pub = _redis.GetSubscriber();
        //    await pub.PublishAsync(channel, message);
        //}

        //[HttpPost("consume")]
        //public string ConsumeMessage([FromQuery, Required]string channel)
        //{
        //    string msg ="NO MESSAGE FOUND";
        //    var sub = _redis.GetSubscriber();
        //    sub.Subscribe(channel, (ch, message) =>
        //    {
        //        msg = message.ToString();
        //    });
        //    return msg;
        //}
        #endregion

        /// <summary>
        /// Add the Candidate and start the workflow
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST Candidate/AddCandidateAndStartWorkflow
        ///     {        
        ///       "CandidateID": 1,
        ///       "CandidateName": "Mike",
        ///       "CandidateEmail": "Mike.Andrew@gmail.com"        
        ///     }
        /// </remarks>
        /// <param name="myDataDTO"></param>
        /// <returns>Workflow Details</returns>
        [HttpPost("AddCandidateAndStartWorkflow")]
        public async Task<IActionResult> StartWorkflowAsync([FromForm] MyDataDTO myDataDTO)
        {
            MyData myData = new MyData
            {
                CandidateID = myDataDTO.CandidateID,
                CandidateName = myDataDTO.CandidateName,
                CandidateEmail = myDataDTO.CandidateEmail,
                candidateStatus = "New"
            };

            _logger.LogInformation("Work Flow Started for Candidate {candidateName}", myData.CandidateName);
            await _redisService.AddToListAsync(_CandidateListKey, myData.CandidateID.ToString());

            var workflow = _workflowHost.StartWorkflow("MyWorkflow", myData);
            return Ok(new { Workflow = workflow });
        }

        [HttpGet("ClearRedisCache")]
        public async Task ClearRedisCache()
        {
            var server = _redis.GetServer(_redis.GetEndPoints()[0]);
            await server.FlushDatabaseAsync();
        }

        /// <summary>
        /// Returns Active Candidates
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWaitingCandidates")]
        public async Task<List<string>> GetWaitingCandidatesAsync()
        {
            _logger.LogInformation("Getting the list of all the active Candidates");
            return await _redisService.GetFromListAsync(_CandidateListKey);
        }

        /// <summary>
        /// For Screening Approval
        /// </summary>
        /// <param name="candidateId"></param>
        /// <param name="candidateStatus"></param>
        /// <param name="currentInterviewStatus"></param>
        /// <returns></returns>
        [HttpPut("ScreeningApproval/{candidateId}")]
        public async Task<IActionResult> ScreeningApprovalAsync(int candidateId, [Required] string candidateStatus, [Required] string currentInterviewStatus)
        {
            MyData myData = new MyData { candidateStatus = candidateStatus, currentInterviewStatus = currentInterviewStatus };
            await _workflowHost.PublishEvent("screening", candidateId.ToString(), myData);
            _logger.LogInformation("Screening response for Candidate {CandidateID} is {currentInterviewStatus}", myData.CandidateID, myData.currentInterviewStatus);
            return Ok("Submitted Results");
        }

        [HttpPut("TechnicalInterview_L1_Approval/{candidateId}")]
        public IActionResult TechnicalInterviewL1(int candidateId, [Required] string candidateStatus, [Required] string currentInterviewStatus)
        {
            MyData myData = new MyData { candidateStatus = candidateStatus, currentInterviewStatus = currentInterviewStatus };
            _workflowHost.PublishEvent("technicalL1", candidateId.ToString(), myData);
            _logger.LogInformation("Technical Interview L1 response for Candidate {CandidateID} is {currentInterviewStatus}", myData.CandidateID, myData.currentInterviewStatus);
            return Ok("Submitted Results");
        }

        [HttpPut("TechnicalInterview_L2_Approval/{candidateId}")]
        public IActionResult TechnicalInterviewL2(int candidateId, [Required] string candidateStatus, [Required] string currentInterviewStatus)
        {
            MyData myData = new MyData { candidateStatus = candidateStatus, currentInterviewStatus = currentInterviewStatus };
            _workflowHost.PublishEvent("technicalL2", candidateId.ToString(), myData);
            _logger.LogInformation("Technical Interview L2 response for Candidate {CandidateID} is {currentInterviewStatus}", myData.CandidateID, myData.currentInterviewStatus);
            return Ok("Submitted Results");
        }

        [HttpPut("Manager_Approval/{candidateId}")]
        public IActionResult ManagerInterview(int candidateId, [Required] string candidateStatus, [Required] string currentInterviewStatus)
        {
            MyData myData = new MyData { candidateStatus = candidateStatus, currentInterviewStatus = currentInterviewStatus };
            _workflowHost.PublishEvent("manager", candidateId.ToString(), myData);
            _logger.LogInformation("Manager Interview response for Candidate {CandidateID} is {currentInterviewStatus}", myData.CandidateID, myData.currentInterviewStatus);
            return Ok("Submitted Results");
        }
    }
}
