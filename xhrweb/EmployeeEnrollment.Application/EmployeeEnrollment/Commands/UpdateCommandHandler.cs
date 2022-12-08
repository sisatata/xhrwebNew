using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;
using CustomConfig = EmployeeEnrollment.Application.EmployeeCustomConfiguration.Commands;
using EmployeeEnrollment.Core.Specifications;
using System.Linq;

namespace EmployeeEnrollment.Application.EmployeeEnrollment.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeEnrollment, EmployeeEnrollmentCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeEnrollment, Guid> repository,
             IMediator mediator,
             IAsyncRepository<EmployeeEntities.EmployeeCustomConfiguration, Guid> repositoryEmployeeCustomConfiguration)
        {
            _repository = repository;
            _mediator = mediator;
            _repositoryEmployeeCustomConfiguration = repositoryEmployeeCustomConfiguration;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeEnrollment, Guid> _repository;
        private readonly IAsyncRepository<EmployeeEntities.EmployeeCustomConfiguration, Guid> _repositoryEmployeeCustomConfiguration;
        private readonly IMediator _mediator;
        public async Task<EmployeeEnrollmentCommandVM>
            Handle(Commands.V1.UpdateEmployeeEnrollment request, CancellationToken cancellationToken)
        {
            var response = new EmployeeEnrollmentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateEmployeeEnrollment(

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
    , request.LeaveTypeGroup

    );

                await _repository.UpdateAsync(entity);
                var employeeCustomConfigurationSpecification = new EmployeeCustomConfigurationSpecification(request.EmployeeId, "LeaveGroup");
                var leaveCustomGroups = await _repositoryEmployeeCustomConfiguration.ListAsync(employeeCustomConfigurationSpecification).ConfigureAwait(false);
                if (leaveCustomGroups.Any())
                {
                    foreach (var item in leaveCustomGroups)
                    {
                        item.CloseEndDateEmployeeCustomConfiguration();
                        await _repositoryEmployeeCustomConfiguration.UpdateAsync(item).ConfigureAwait(false);
                    }
                }
                var employeeCustomConfig = await _mediator.Send(new CustomConfig.Commands.V1.CreateEmployeeCustomConfiguration
                {
                    Code = "LeaveGroup",
                    CustomValue = request.LeaveTypeGroup,
                    Description = $"Leave Group {request.LeaveTypeGroup}",
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.AddYears(10),
                    IsDeleted = false,
                    EmployeeId = request.EmployeeId,
                }).ConfigureAwait(false);

                response.Status = true;
                response.Message = "entity updated successfully.";
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

