using CompanySetup.Application.Branch.Commands.Models;
using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Branch.Commands
{
    public class UpdateCommandHandler : IRequestHandler<BranchCommands.V1.UpdateBranch, BranchCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<Core.Entities.Branch, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<Core.Entities.Branch, Guid> _repository;

        public async Task<BranchCommandVM> Handle(BranchCommands.V1.UpdateBranch request, CancellationToken cancellationToken)
        {
            var response = new BranchCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateBranch
                    (
                        (request.CompanyId == Guid.Empty || request.CompanyId == null) ? entity.CompanyId  : request.CompanyId,
                        request.BranchName ?? entity.BranchName,
                        request.ShortName ?? entity.ShortName,
                        request.BranchLocalizedName ?? entity.BranchLocalizedName,
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
