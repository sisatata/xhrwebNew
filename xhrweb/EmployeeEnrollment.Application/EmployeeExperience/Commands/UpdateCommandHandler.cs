using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeExperience.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeExperience, EmployeeExperienceCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeExperience, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeExperience, Guid> _repository;

        public async Task<EmployeeExperienceCommandVM>
            Handle(Commands.V1.UpdateEmployeeExperience request, CancellationToken cancellationToken)
        {
            var response = new EmployeeExperienceCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateEmployeeExperience(

                         //request. Id, 
                         request.EmployeeId,
                         request.CompanyName,
                         request.Designation,
                         request.FunctionalDesignation,
                         request.Department,
                         request.Responsibilities,
                         request.CompanyAddress,
                         request.CompanyContactNo,
                         request.CompanyMobile,
                         request.CompanyEmail,
                         request.CompanyWeb,
                         request.FromDate,
                         request.ToDate,
                         request.TilDate,
                         request.IsDeleted,
                         request.CompanyId

    );

                await _repository.UpdateAsync(entity);

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

