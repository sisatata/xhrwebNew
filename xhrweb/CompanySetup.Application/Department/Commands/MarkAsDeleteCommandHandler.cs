using CompanySetup.Application.Department.Commands.Models;
using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Department.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<DepartmentCommands.V1.MarkDepartmentAsDelete, DepartmentCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<Core.Entities.Department, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<Core.Entities.Department, Guid> _repository;

        public async Task<DepartmentCommandVM> Handle(DepartmentCommands.V1.MarkDepartmentAsDelete request, CancellationToken cancellationToken)
        {
            var response = new DepartmentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkDepartmentAsDeleted();
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
