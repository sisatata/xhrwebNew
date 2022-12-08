using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.PreviousPFGratuityEarnLeave.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreatePreviousPFGratuityEarnLeave, PreviousPFGratuityEarnLeaveCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.PreviousPFGratuityEarnLeave, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.PreviousPFGratuityEarnLeave, Guid>
        _repository;

        public async Task<PreviousPFGratuityEarnLeaveCommandVM> Handle(Commands.V1.CreatePreviousPFGratuityEarnLeave request, CancellationToken cancellationToken)
        {
            var response = new PreviousPFGratuityEarnLeaveCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.PreviousPFGratuityEarnLeave.Create(
                         request.EmployeeId,
                         request.PFAmount,
                         request.GratuityAmount,
                         request.EarnLeaveAmount,
                         request.TillDate,
                         request.CompanyId,
                         request.IsDeleted,
                         request.Remarks
                );
                var data = await _repository.AddAsync(entity);

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
