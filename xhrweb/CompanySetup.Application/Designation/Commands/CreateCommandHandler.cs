using CompanySetup.Application.Designation.Commands.Models;
using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Designation.Commands
{
    public class CreateCommandHandler : IRequestHandler<DesignationCommands.V1.CreateDesignation, DesignationCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<Core.Entities.Designation, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<Core.Entities.Designation, Guid> _repository;

        public async Task<DesignationCommandVM> Handle(DesignationCommands.V1.CreateDesignation request, CancellationToken cancellationToken)
        {
            var response = new DesignationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = Core.Entities.Designation.Create(linkedEntityId: request.LinkedEntityId, linkedEntityType: request.LinkedEntityType, designationName: request.DesignationName,
                    designationLocalizedName: request.DesignationLocalizedName, shortName: request.ShortName, sortOrder: request.SortOrder);

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
