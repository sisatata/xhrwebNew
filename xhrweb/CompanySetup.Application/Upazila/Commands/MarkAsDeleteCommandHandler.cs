using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UpazilaEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.Upazila.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteUpazila, UpazilaCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<UpazilaEntities.Upazila, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<UpazilaEntities.Upazila, Guid> _repository;

        public async Task<UpazilaCommandVM>
            Handle(Commands.V1.MarkAsDeleteUpazila request, CancellationToken cancellationToken)
        {
            var response = new UpazilaCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteUpazila();

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
