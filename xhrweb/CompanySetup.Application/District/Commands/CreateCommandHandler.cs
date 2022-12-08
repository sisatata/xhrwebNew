using CompanySetup.Application.District.Commands;
using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using DistrictEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.District.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateDistrict, DistrictCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<DistrictEntities.District, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<DistrictEntities.District, Guid>
        _repository;

        public async Task<DistrictCommandVM> Handle(Commands.V1.CreateDistrict request, CancellationToken cancellationToken)
        {
            var response = new DistrictCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = DistrictEntities.District.Create(
                         request.DivisionId,
                         request.Name,
                         request.NameLocalized,
                         request.Latitude,
                         request.Longitude,
                         request.Website
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
