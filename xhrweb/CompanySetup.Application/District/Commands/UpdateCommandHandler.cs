using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using DistrictEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.District.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateDistrict, DistrictCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<DistrictEntities.District, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<DistrictEntities.District, Guid> _repository;

        public async Task<DistrictCommandVM>
            Handle(Commands.V1.UpdateDistrict request, CancellationToken cancellationToken)
        {
            var response = new DistrictCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateDistrict(
                         request.DivisionId,
                         request.Name,
                         request.NameLocalized,
                         request.Latitude,
                         request.Longitude,
                         request.Website

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

