using System;
using System.Threading.Tasks;
using TaskManagement.Application.TaskDetailLog.Commands;
using TaskManagement.Application.TaskDetailLog.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class TaskDetailLogController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetTaskDetailLogList());
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetTaskDetailLog { Id = id });
            return Ok(data);
        }
        [HttpGet("taskDetail/{taskDetailId}")]
        public async Task<IActionResult> GetByTaskDetail(Guid taskDetailId)
        {
            var data = await Mediator.Send(new Queries.GetTaskDetailLogListByTaskDetail { TaskDetailId= taskDetailId });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateTaskDetailLog command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateTaskDetailLog command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteTaskDetailLog command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
