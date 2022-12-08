using CompanySetup.Application.Branch.Commands.Models;
using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Branch.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<BranchCommands.V1.MarkBranchAsDelete, BranchCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<Core.Entities.Branch, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<Core.Entities.Branch, Guid> _repository;

        public async Task<BranchCommandVM> Handle(BranchCommands.V1.MarkBranchAsDelete request, CancellationToken cancellationToken)
        {
            var response = new BranchCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkBranchAsDeleted();
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
