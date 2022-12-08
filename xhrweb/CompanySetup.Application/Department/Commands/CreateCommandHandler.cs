using CompanySetup.Application.Department.Commands.Models;
using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Department.Commands
{
    public class CreateCommandHandler : IRequestHandler<DepartmentCommands.V1.CreateDepartment, DepartmentCommandVM>
    {

        public CreateCommandHandler(IAsyncRepository<Core.Entities.Department, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<Core.Entities.Department, Guid> _repository;

        public async Task<DepartmentCommandVM> Handle(DepartmentCommands.V1.CreateDepartment request, CancellationToken cancellationToken)
        {
            var response = new DepartmentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = Core.Entities.Department.Create(request.CompanyId, request.BranchId, request.DepartmentName, request.ShortName,
                                    request.DepartmentLocalizedName, request.SortOrder);
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
