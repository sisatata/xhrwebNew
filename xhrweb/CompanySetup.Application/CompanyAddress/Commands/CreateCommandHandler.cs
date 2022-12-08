using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.CompanyAddress.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateCompanyAddress, CompanyAddressCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<CompanyEntities.CompanyAddress, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.CompanyAddress, Guid>
        _repository;

        public async Task<CompanyAddressCommandVM> Handle(Commands.V1.CreateCompanyAddress request, CancellationToken cancellationToken)
        {
            var response = new CompanyAddressCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = CompanyEntities.CompanyAddress.Create(
                         request.CompanyId,
                         request.AddressLine1,
                         request.AddressLine2,
                         request.Village,
                         request.PostOffice,
                         request.ThanaId,
                         request.DistrictId,
                         request.CountryId,
                         request.Latitude,
                         request.Longitude,
                         request.AddressTypeId
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
