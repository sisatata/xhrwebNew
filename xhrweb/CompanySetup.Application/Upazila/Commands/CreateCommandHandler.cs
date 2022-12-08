using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UpazilaEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.Upazila.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateUpazila, UpazilaCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<UpazilaEntities.Upazila, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<UpazilaEntities.Upazila, Guid>
        _repository;

        public async Task<UpazilaCommandVM> Handle(Commands.V1.CreateUpazila request, CancellationToken cancellationToken)
        {
            var response = new UpazilaCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = UpazilaEntities.Upazila.Create(
                         request.DistrictId,
                         request.UpazilaName,
                         request.UpazilaNameLocalized

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
