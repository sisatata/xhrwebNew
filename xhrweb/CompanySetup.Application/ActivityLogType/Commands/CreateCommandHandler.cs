using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.ActivityLogType.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateActivityLogType, ActivityLogTypeCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<CompanyEntities.ActivityLogType, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.ActivityLogType, Guid>
        _repository;

        public async Task<ActivityLogTypeCommandVM> Handle(Commands.V1.CreateActivityLogType request, CancellationToken cancellationToken)
        {
            var response = new ActivityLogTypeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = CompanyEntities.ActivityLogType.Create(
                         request.SystemKeyword,
                         request.Name,
                         request.Enabled
            
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
