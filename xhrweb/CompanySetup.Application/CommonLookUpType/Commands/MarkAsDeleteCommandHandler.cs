using CompanySetup.Application.CommonLookUpType.Commands.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;
using CompanySetup.Core.Interfaces;

namespace CompanySetup.Application.CommonLookUpType.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<LookUpCommands.V1.MarkAsDeleteCommonLookUpType, CommonLookUpTypeCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<CompanyEntities.CommonLookUpType, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.CommonLookUpType, Guid> _repository;

        public async Task<CommonLookUpTypeCommandVM>
            Handle(LookUpCommands.V1.MarkAsDeleteCommonLookUpType request, CancellationToken cancellationToken)
        {
            var response = new CommonLookUpTypeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteCommonLookUpType();

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
