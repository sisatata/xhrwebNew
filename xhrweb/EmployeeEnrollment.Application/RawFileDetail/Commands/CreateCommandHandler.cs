using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.RawFileDetail.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateRawFileDetail, RawFileDetailCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.RawFileDetail, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.RawFileDetail, Guid>
        _repository;

        public async Task<RawFileDetailCommandVM> Handle(Commands.V1.CreateRawFileDetail request, CancellationToken cancellationToken)
        {
            var response = new RawFileDetailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.RawFileDetail.Create(
                         request.FileName,
                         request.FileType,
                         request.Comments,
                         request.CompanyId
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
