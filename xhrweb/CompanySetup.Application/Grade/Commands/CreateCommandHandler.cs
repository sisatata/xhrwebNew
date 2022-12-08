using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using GradeEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.Grade.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateGrade, GradeCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<GradeEntities.Grade, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<GradeEntities.Grade, Guid>
        _repository;

        public async Task<GradeCommandVM> Handle(Commands.V1.CreateGrade request, CancellationToken cancellationToken)
        {
            var response = new GradeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = GradeEntities.Grade.Create(

                         request.GradeName,
                         request.GradeNameLocalized,
                         request.Rank,
                         request.IsDeleted,
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
