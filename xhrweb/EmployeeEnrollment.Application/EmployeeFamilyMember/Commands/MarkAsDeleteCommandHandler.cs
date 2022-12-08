using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeFamilyMember.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteEmployeeFamilyMember, EmployeeFamilyMemberCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeFamilyMember, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeFamilyMember, Guid> _repository;

        public async Task<EmployeeFamilyMemberCommandVM>
            Handle(Commands.V1.MarkAsDeleteEmployeeFamilyMember request, CancellationToken cancellationToken)
        {
            var response = new EmployeeFamilyMemberCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteEmployeeFamilyMember();

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
