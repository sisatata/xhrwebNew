using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using DistrictEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.District.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteDistrict, DistrictCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<DistrictEntities.District, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<DistrictEntities.District, Guid> _repository;

        public async Task<DistrictCommandVM>
            Handle(Commands.V1.MarkAsDeleteDistrict request, CancellationToken cancellationToken)
        {
            var response = new DistrictCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteDistrict();

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
