using CompanySetup.Application.Designation.Commands.Models;
using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Designation.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<DesignationCommands.V1.MarkDesignationAsDelete, DesignationCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<Core.Entities.Designation, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<Core.Entities.Designation, Guid> _repository;
        public async Task<DesignationCommandVM> Handle(DesignationCommands.V1.MarkDesignationAsDelete request, CancellationToken cancellationToken)
        {
            var response = new DesignationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkDesignationAsDeleted();
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
