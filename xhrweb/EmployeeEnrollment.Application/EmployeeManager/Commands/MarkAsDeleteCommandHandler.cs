using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;
using Query = EmployeeEnrollment.Application.EmployeeManager.Queries;

namespace EmployeeEnrollment.Application.EmployeeManager.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteEmployeeManager, EmployeeManagerCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeManager, Guid> repository,
            ICurrentUserContext userContext,
            IActivityLogService activityLogService,
            IMediator mediator)
        {
            _repository = repository;
            _userContext = userContext;
            _activityLogService = activityLogService;
            _mediator = mediator;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeManager, Guid> _repository;
        private readonly ICurrentUserContext _userContext;
        private readonly IActivityLogService _activityLogService;
        private readonly IMediator _mediator;

        public async Task<EmployeeManagerCommandVM>
            Handle(Commands.V1.MarkAsDeleteEmployeeManager request, CancellationToken cancellationToken)
        {
            var response = new EmployeeManagerCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {


                var entity = await _repository.GetByIdAsync(request.Id);
                var query = new Query.Queries.GetEmployeeManager { Id = entity.EmployeeId };
                var employeeManager = await _mediator.Send(query);

                entity.MarkAsDeleteEmployeeManager(new Guid(_userContext.CurrentUserId));

                await _repository.UpdateAsync(entity);

                await _activityLogService.InsertActivity("Employee", $"Manager {employeeManager.ManagerName }- {employeeManager.ManagerTypeName} for employee {employeeManager.EmployeeName} has been removed", entity);
                response.Status = true;
                response.Message = "entity deleted successfully.";
                response.Id = entity.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
