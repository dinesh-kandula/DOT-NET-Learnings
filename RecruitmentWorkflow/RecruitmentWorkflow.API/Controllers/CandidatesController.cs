using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitmentWorkflow.Models.Models;
using RecruitmentWorkflow.Models.Models.DTO;
using RecruitmentWorkflow.Models.Models.Enums;
using RecruitmentWorkflow.Models.Models.WorkflowData;
using System.Xml.Linq;
using WorkflowCore.Interface;

namespace RecruitmentWorkflow.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly RecruitmentWorkflowContext _context;
        private readonly IWorkflowHost _workflowHost;

        public CandidatesController(RecruitmentWorkflowContext context, IWorkflowHost workflowHost)
        {
            _context = context;
            _workflowHost = workflowHost;
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate([FromBody] CandidateDTO candidateDto)
        {
            var candidate = new Candidate
            {
                Name = candidateDto.Name,
                Email = candidateDto.Email,
                CandidateStatus = 1
            };

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            var workflowData = new MyCandidateData
            {
                CandidateId = candidate.Id,
                CandidateName = candidate.Name,
                CandidateEmail = candidate.Email,
                CurrentStage = (int) RecritmentStageEnum.NotStarted,
                CurrentStageStatus = (int) InterviewStatusEnum.NotStarted,
                CandidateStatus = 1
            };

            _workflowHost.StartWorkflow("RecruitmentWorkflow", workflowData);
            return Ok("Candidate added and workflow started");
        }

        [HttpPut("updateStatus/{id}")]
        public async Task<IActionResult> UpdateCandidateStatus(int id, [FromBody] string status)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null) return NotFound();

            var previousStatus = candidate.Status;
            candidate.Status = status;

            _context.StatusChanges.Add(new StatusChange
            {
                CandidateId = candidate.Id,
                PreviousStatus = previousStatus,
                NewStatus = status,
                Timestamp = DateTime.Now
            });

            _context.Entry(candidate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Send email notifications
           
            // Update workflow
            var workflow = await _workflowHost.PersistenceStore.GetWorkflowInstance(candidate.Id.ToString());
            if (workflow != null)
            {
                var workflowData = new MyData
                {
                    CandidateId = candidate.Id,
                    CandidateName = candidate.Name,
                    CandidateEmail = candidate.Email,
                    CurrentStage = candidate.CurrentStage,
                    CurrentStageStatus = candidate.Status,
                    Status = candidate.Status
                };
                _workflowHost.PublishEvent("StatusChanged", candidate.Id.ToString(), workflowData);
            }

            return NoContent();
        }

        [HttpPut("updateStage/{id}")]
        public async Task<IActionResult> UpdateCandidateStage(int id, [FromBody] string stage)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null) return NotFound();

            var previousStage = candidate.CurrentStage;
            candidate.CurrentStage = stage;

            _context.Entry(candidate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Send email notifications
           
            // Update workflow
            var workflow = await _workflowHost.PersistenceStore.GetWorkflowInstance(candidate.Id.ToString());
            if (workflow != null)
            {
                var workflowData = new MyData
                {
                    CandidateId = candidate.Id,
                    CandidateName = candidate.Name,
                    CandidateEmail = candidate.Email,
                    CurrentStage = candidate.CurrentStage,
                    CurrentStageStatus = candidate.Status,
                    Status = candidate.Status
                };
                _workflowHost.PublishEvent("StageChanged", candidate.Id.ToString(), workflowData);
            }

            return NoContent();
        }
    }

}
