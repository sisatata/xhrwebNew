using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.PreviousPFGratuityEarnLeave.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdatePreviousPFGratuityEarnLeave, PreviousPFGratuityEarnLeaveCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.PreviousPFGratuityEarnLeave, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.PreviousPFGratuityEarnLeave, Guid> _repository;

        public async Task<PreviousPFGratuityEarnLeaveCommandVM>
            Handle(Commands.V1.UpdatePreviousPFGratuityEarnLeave request, CancellationToken cancellationToken)
        {
            var response = new PreviousPFGratuityEarnLeaveCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdatePreviousPFGratuityEarnLeave(

                         request.EmployeeId,
                         request.PFAmount,
                         request.GratuityAmount,
                         request.EarnLeaveAmount,
                         request.TillDate,
                         request.CompanyId,
                         request.IsDeleted,
                         request.Remarks

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

