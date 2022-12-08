using LeaveManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using LeaveEntities = LeaveManagement.Core.Entities;

namespace LeaveManagement.Application.LeaveTypeGroup.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateLeaveTypeGroup, LeaveTypeGroupCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<LeaveEntities.LeaveTypeGroup, int>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<LeaveEntities.LeaveTypeGroup, int>
        _repository;

        public async Task<LeaveTypeGroupCommandVM> Handle(Commands.V1.CreateLeaveTypeGroup request, CancellationToken cancellationToken)
        {
            var response = new LeaveTypeGroupCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = LeaveEntities.LeaveTypeGroup.Create(
                         request.LeaveTypeGroupName,
                         request.LeaveTypeGroupNameLC,
                         request.CompanyId,
                         request.IsDeleted



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
