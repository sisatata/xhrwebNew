using CompanySetup.Application.Department.Commands.Models;
using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Department.Commands
{
    public class UpdateCommandHandler : IRequestHandler<DepartmentCommands.V1.UpdateDepartment, DepartmentCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<Core.Entities.Department, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<Core.Entities.Department, Guid> _repository;

        public async Task<DepartmentCommandVM> Handle(DepartmentCommands.V1.UpdateDepartment request, CancellationToken cancellationToken)
        {
            var response = new DepartmentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateDepartment
                    (
                        (request.CompanyId == Guid.Empty || request.CompanyId == null) ? entity.CompanyId : request.CompanyId,
                        (request.BranchId == Guid.Empty || request.BranchId == null) ? entity.BranchId : request.BranchId,
                        request.DepartmentName ?? entity.DepartmentName,
                        request.DepartmentLocalizedName ?? entity.DepartmentLocalizedName,
                        request.ShortName ?? entity.ShortName,
                        request.SortOrder == 0 ? entity.SortOrder : request.SortOrder
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
