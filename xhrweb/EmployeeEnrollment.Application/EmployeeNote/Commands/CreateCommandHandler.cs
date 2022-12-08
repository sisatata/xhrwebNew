using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeNote.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeNote, EmployeeNoteCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeNote, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeNote, Guid>
        _repository;

        public async Task<EmployeeNoteCommandVM> Handle(Commands.V1.CreateEmployeeNote request, CancellationToken cancellationToken)
        {
            var response = new EmployeeNoteCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.EmployeeNote.Create(
                         request.EmployeeId,
                         request.Note,
                         request.DownloadId,
                         request.DisplayToEmployee
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
