using LeaveManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using LeaveEntities = LeaveManagement.Core.Entities;

namespace LeaveManagement.Application.LeaveTypeGroup.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateLeaveTypeGroup, LeaveTypeGroupCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<LeaveEntities.LeaveTypeGroup, int> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<LeaveEntities.LeaveTypeGroup, int> _repository;

        public async Task<LeaveTypeGroupCommandVM>
            Handle(Commands.V1.UpdateLeaveTypeGroup request, CancellationToken cancellationToken)
        {
            var response = new LeaveTypeGroupCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateLeaveTypeGroup(

                         request.LeaveTypeGroupName,
                         request.LeaveTypeGroupNameLC,
                         request.CompanyId,
                         request.IsDeleted

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

