using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using GradeEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.Grade.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteGrade, GradeCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<GradeEntities.Grade, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<GradeEntities.Grade, Guid> _repository;

        public async Task<GradeCommandVM>
            Handle(Commands.V1.MarkAsDeleteGrade request, CancellationToken cancellationToken)
        {
            var response = new GradeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteGrade();

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
