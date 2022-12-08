using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LeaveManagement.Application.LeaveTypes.Commands.Models;
using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using LeaveManagement.Core.Interfaces;
using MediatR;

namespace LeaveManagement.Application.LeaveTypes.Commands
{
    public class MarkAsDeleteCommandHandler:IRequestHandler<LeaveTypeCommands.V1.MarkLeaveTypeAsDelete,LeaveTypeCommandVM>
    {

        public MarkAsDeleteCommandHandler(IAsyncRepository<LeaveType, Guid> repository) 
        {
            _deleteRepository = repository;
        }

        private readonly IAsyncRepository<LeaveType, Guid> _deleteRepository;

        public async Task<LeaveTypeCommandVM> Handle(LeaveTypeCommands.V1.MarkLeaveTypeAsDelete request, CancellationToken cancellationToken)
        {
            var response = new LeaveTypeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _deleteRepository.GetByIdAsync(request.Id);
                entity.MarkLeaveTypeAsDeleted();
                await _deleteRepository.UpdateAsync(entity);
                response.Status = true;
                response.Message = "deleted Successfully";
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
