using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;
using EmployeeCommand = EmployeeEnrollment.Application.EmployeeEnrollment.Commands;

namespace EmployeeEnrollment.Application.EmployeeStatusHistory.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeStatusHistory, EmployeeStatusHistoryCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeStatusHistory, Guid> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeStatusHistory, Guid> _repository;
        private readonly IMediator _mediator;

        public async Task<EmployeeStatusHistoryCommandVM> Handle(Commands.V1.CreateEmployeeStatusHistory request, CancellationToken cancellationToken)
        {
            var response = new EmployeeStatusHistoryCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.EmployeeStatusHistory.Create(
                         request.EmployeeId,
                         request.StatusId,
                         DateTime.Now,
                         request.Remarks
                );
                var data = await _repository.AddAsync(entity).ConfigureAwait(false);

                var employeeStatus = await _mediator.Send(new EmployeeCommand.Commands.V1.UpdateStatusEmployee
                {
                    Id = request.EmployeeId,
                    StatusId = request.StatusId
                });

                response.Status = true;
                response.Message = "entity created successfully.";
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
