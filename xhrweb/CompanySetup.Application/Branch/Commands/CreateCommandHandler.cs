using CompanySetup.Application.Branch.Commands.Models;
using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Branch.Commands
{
    public class CreateCommandHandler : IRequestHandler<BranchCommands.V1.CreateBranch, BranchCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<Core.Entities.Branch, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<Core.Entities.Branch, Guid> _repository;

        public async Task<BranchCommandVM> Handle(BranchCommands.V1.CreateBranch request, CancellationToken cancellationToken)
        {
            var response = new BranchCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = Core.Entities.Branch.Create(request.CompanyId, request.BranchName, request.ShortName,
                                    request.BranchLocalizedName, request.SortOrder);
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
