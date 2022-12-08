using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ActivityLog = CompanySetup.Application.ActivityLog.Commands;
using CompanySetup.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CompanySetup.Core.Entities;


namespace Asl.Infrastructure
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserContext _userContext;
        public ActivityLogService(IMediator mediator, ICurrentUserContext currentUserContext)
        {
            _mediator = mediator;
            _userContext = currentUserContext;
        }
        public async Task<bool> InsertActivity(string loginUserId, string currentUserCompanyId, string systemKeyword, string comment, BaseEntity<Guid> entity = null)
        {

            //return await InsertActivity(loginUserId, systemKeyword, comment, entity);
            var actvityCommand = new ActivityLog.Commands.V1.CreateActivityLog
            {
                Comment = comment,
                UserId = string.IsNullOrEmpty(loginUserId) ? Guid.Empty : new Guid(loginUserId),
                SystemKeyword = systemKeyword,
                EntityId = entity?.Id,
                CompanyId = string.IsNullOrEmpty(currentUserCompanyId) ? Guid.Empty : new Guid(currentUserCompanyId)
            };
            await _mediator.Send(actvityCommand).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> InsertActivity(string systemKeyword, string comment, BaseEntity<Guid> entity = null)
        {
            var actvityCommand = new ActivityLog.Commands.V1.CreateActivityLog
            {
                Comment = comment,
                UserId = !string.IsNullOrEmpty(_userContext.CurrentUserId) ? new Guid(_userContext.CurrentUserId) : Guid.Empty,
                SystemKeyword = systemKeyword,
                EntityId = entity?.Id,
                CompanyId = !string.IsNullOrEmpty(_userContext.CurrentUserCompanyId) ? new Guid(_userContext.CurrentUserCompanyId) : Guid.Empty
            };
            await _mediator.Send(actvityCommand).ConfigureAwait(false);
            return true;
        }
    }
}
