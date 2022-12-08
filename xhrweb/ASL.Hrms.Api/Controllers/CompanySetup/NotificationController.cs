using System;
using System.Threading.Tasks;
using CompanySetup.Application.Notification.Commands;
using CompanySetup.Application.Notification.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class NotificationController : BaseController
    {
        #region Queries

        [HttpGet("ownerId/{id}")]
        public async Task<IActionResult> GetByOwner(Guid ownerId)
        {
            var data = await Mediator.Send(new Queries.GetNotificationListByOwner { NotificationOwnerId = ownerId });
            return Ok(data);
        }

        [HttpGet("get-summary-owner/{ownerId}")]
        public async Task<IActionResult> GetNotificationSummaryByOwner(Guid ownerId)
        {
            var data = await Mediator.Send(new Queries.GetNotificationSummaryByOwner { NotificationOwnerId = ownerId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetNotification { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.ProcessHolidayNotificationBulk command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateNotification command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        [HttpPut]
        public async Task<IActionResult> MarkRead([FromBody] Commands.V1.MarkAsViewedNotification command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
