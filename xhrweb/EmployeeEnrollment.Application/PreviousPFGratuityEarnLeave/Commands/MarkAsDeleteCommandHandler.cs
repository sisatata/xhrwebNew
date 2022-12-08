using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.PreviousPFGratuityEarnLeave.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeletePreviousPFGratuityEarnLeave, PreviousPFGratuityEarnLeaveCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<EmployeeEntities.PreviousPFGratuityEarnLeave, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.PreviousPFGratuityEarnLeave, Guid> _repository;

        public async Task<PreviousPFGratuityEarnLeaveCommandVM>
            Handle(Commands.V1.MarkAsDeletePreviousPFGratuityEarnLeave request, CancellationToken cancellationToken)
        {
            var response = new PreviousPFGratuityEarnLeaveCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeletePreviousPFGratuityEarnLeave();

                await _repository.UpdateAsync(entity);

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
