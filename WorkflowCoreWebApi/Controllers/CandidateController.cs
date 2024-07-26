using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WorkflowCore.Interface;

namespace WorkflowCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly IWorkflowHost _workflowHost;
        public CandidateController(IWorkflowHost workflowHost)
        {
            _workflowHost = workflowHost;
        }

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
        public IActionResult StartWorkflow([FromForm] MyDataDTO myDataDTO)
        {
            MyData myData = new MyData
            {
                CandidateID = myDataDTO.CandidateID,
                CandidateName = myDataDTO.CandidateName,
                CandidateEmail = myDataDTO.CandidateEmail,
                candidateStatus = "New"
            };

            var workflow = _workflowHost.StartWorkflow("MyWorkflow", myData);
            return Ok(new { Workflow = workflow });
        }

        /// <summary>
        /// For Screening Approval
        /// </summary>
        /// <param name="candidateId"></param>
        /// <param name="candidateStatus"></param>
        /// <param name="currentInterviewStatus"></param>
        /// <returns></returns>
        [HttpPut("ScreeningApproval/{candidateId}")]
        public IActionResult ScreeningApproval(int candidateId, [Required] string candidateStatus, [Required] string currentInterviewStatus)
        {
            MyData myData = new MyData { candidateStatus = candidateStatus, currentInterviewStatus = currentInterviewStatus };
            _workflowHost.PublishEvent("screening", candidateId.ToString(), myData);
            return Ok("Submitted Results");
        }

        [HttpPut("TechnicalInterview_L1_Approval/{candidateId}")]
        public IActionResult TechnicalInterviewL1(int candidateId, [Required] string candidateStatus, [Required] string currentInterviewStatus)
        {
            MyData myData = new MyData { candidateStatus = candidateStatus, currentInterviewStatus = currentInterviewStatus };
            _workflowHost.PublishEvent("technicalL1", candidateId.ToString(), myData);
            return Ok("Submitted Results");
        }

        [HttpPut("TechnicalInterview_L2_Approval/{candidateId}")]
        public IActionResult TechnicalInterviewL2(int candidateId, [Required] string candidateStatus, [Required] string currentInterviewStatus)
        {
            MyData myData = new MyData { candidateStatus = candidateStatus, currentInterviewStatus = currentInterviewStatus };
            _workflowHost.PublishEvent("technicalL2", candidateId.ToString(), myData);
            return Ok("Submitted Results");
        }

        [HttpPut("Manager_Approval/{candidateId}")]
        public IActionResult ManagerInterview(int candidateId, [Required] string candidateStatus, [Required] string currentInterviewStatus)
        {
            MyData myData = new MyData { candidateStatus = candidateStatus, currentInterviewStatus = currentInterviewStatus };
            _workflowHost.PublishEvent("manager", candidateId.ToString(), myData);
            return Ok("Submitted Results");
        }
    }
}
