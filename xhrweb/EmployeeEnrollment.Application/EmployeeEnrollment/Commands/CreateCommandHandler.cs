using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;
using CustomConfig = EmployeeEnrollment.Application.EmployeeCustomConfiguration.Commands;

namespace EmployeeEnrollment.Application.EmployeeEnrollment.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeEnrollment, EmployeeEnrollmentCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeEnrollment, Guid>
        repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeEnrollment, Guid>
        _repository;
        private readonly IMediator _mediator;
        public async Task<EmployeeEnrollmentCommandVM> Handle(Commands.V1.CreateEmployeeEnrollment request, CancellationToken cancellationToken)
        {
            var response = new EmployeeEnrollmentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.EmployeeEnrollment.Create(
                         request.EmployeeId,
                         request.JoiningDate,
                         request.QuitDate,
                         request.StatusId,
                         request.PermanentDate,
                         request.JobTypeId,
                         request.GradeId,
                         request.SubGradeId,
                         request.EmployeeTypeId,
                         request.EmployeeCategoryId,
                         request.ConfirmationId,
                         request.GenderId
                ,        request.LeaveTypeGroup
                );
                var data = await _repository.AddAsync(entity);

                var employeeCustomConfig = await _mediator.Send(new CustomConfig.Commands.V1.CreateEmployeeCustomConfiguration
                {
                    Code = "LeaveGroup",
                    CustomValue = request.LeaveTypeGroup,
                    Description = $"Leave Group {request.LeaveTypeGroup}",
                    StartDate = request.JoiningDate,
                    EndDate = DateTime.Now.AddYears(10),
                    IsDeleted = false,
                    EmployeeId = request.EmployeeId,
                }).ConfigureAwait(false);

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
