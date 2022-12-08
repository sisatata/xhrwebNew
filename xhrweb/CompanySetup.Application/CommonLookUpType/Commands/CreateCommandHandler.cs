using CompanySetup.Application.CommonLookUpType.Commands.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;
using CompanySetup.Core.Interfaces;

namespace CompanySetup.Application.CommonLookUpType.Commands
{
    public class CreateCommandHandler : IRequestHandler<LookUpCommands.V1.CreateCommonLookUpType, CommonLookUpTypeCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<CompanyEntities.CommonLookUpType, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.CommonLookUpType, Guid>
        _repository;

        public async Task<CommonLookUpTypeCommandVM> Handle(LookUpCommands.V1.CreateCommonLookUpType request, CancellationToken cancellationToken)
        {
            var response = new CommonLookUpTypeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = CompanyEntities.CommonLookUpType.Create(
                         request.ShortCode,
                         request.LookUpTypeName,
                         request.LookUpTypeNameLC,
                         request.Remarks,
                         request.CompanyId == Guid.Empty ? null : request.CompanyId,
                         request.ParentId,
                         request.SortOrder



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
