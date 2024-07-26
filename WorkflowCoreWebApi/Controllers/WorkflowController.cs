using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using WorkflowCore.Interface;

namespace WorkflowCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowHost _workflowHost;

        public WorkflowController(IWorkflowHost workflowHost)
        {
            _workflowHost = workflowHost;
        }

        [HttpPost("StartTaskWorkflow")]
        public IActionResult StartTaskWorkflow([FromForm] TaskData taskData)
        {
            var workflow = _workflowHost.StartWorkflow("MyTaskWorkflow", taskData);
            return Ok(new { Workflow = workflow });
        }

        [HttpDelete("stop")]
        public IActionResult StopWorkflow()
        {
            _workflowHost.Stop();
            return Ok("Workflow host stopped");
        }

        [HttpPatch("restart")]
        public IActionResult ResumeWorkflow()
        {
            _workflowHost.Start();
            return Ok("Workflow host restarted/resumed");
        }

        [HttpDelete("Suspend/{workflowId}")]
        public async Task<IActionResult> SuspendWorkFlow(string workflowId)
        {
            bool susupened = await _workflowHost.SuspendWorkflow(workflowId);
            if (susupened)
                return Ok($"Workflow Stoped: {workflowId}");
            else
                return Ok($"Unable to stop the workflow : {workflowId}");
        }

        [HttpDelete("terminate/{workflowId}")]
        public async Task<IActionResult> TerminateWorkflow(string workflowId)
        {
            await _workflowHost.TerminateWorkflow(workflowId);
            return Ok("Workflow terminated");
        }

        [HttpPut("resume/{workflowId}")]
        public async Task<IActionResult> ResumeWorkflow(string workflowId)
        {
            await _workflowHost.ResumeWorkflow(workflowId);
            return Ok("Workflow resumed");
        }

        [HttpGet("publish")]
        public void PublishEvent()
        {
            TaskData data = new TaskData
            {
                Value1 = "Value 1",
                Value2 = "Value 2"
            };
            _workflowHost.PublishEvent("MyEvent", "0", data);
        }
    }
}
