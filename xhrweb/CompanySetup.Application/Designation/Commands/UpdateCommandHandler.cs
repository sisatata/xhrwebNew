using CompanySetup.Application.Designation.Commands.Models;
using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Designation.Commands
{
    public class UpdateCommandHandler : IRequestHandler<DesignationCommands.V1.UpdateDesignation, DesignationCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<Core.Entities.Designation, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<Core.Entities.Designation, Guid> _repository;
        public async Task<DesignationCommandVM> Handle(DesignationCommands.V1.UpdateDesignation request, CancellationToken cancellationToken)
        {
            var response = new DesignationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateDesignation
                    (
                        (request.LinkedEntityId == Guid.Empty || request.LinkedEntityId == null) ? entity.LinkedEntityId : request.LinkedEntityId,
                        request.LinkedEntityType ?? entity.LinkedEntityType,
                        request.DesignationName ?? entity.DesignationName,
                        request.DesignationLocalizedName ?? entity.DesignationLocalizedName,
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
