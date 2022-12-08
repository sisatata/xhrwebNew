using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.SalaryStructureComponent.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateSalaryStructureComponent, SalaryStructureComponentCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.SalaryStructureComponent, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.SalaryStructureComponent, Guid>
        _repository;

        public async Task<SalaryStructureComponentCommandVM> Handle(Commands.V1.CreateSalaryStructureComponent request, CancellationToken cancellationToken)
        {
            var response = new SalaryStructureComponentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = PayrollEntities.SalaryStructureComponent.Create(
                         request.SalaryStrutureId,
                         request.SalaryComponentName,
                         request.Value,
                         request.ValueType,
                         request.PercentOn,
                         request.IsDeleted,
                         request.CompanyId,
                         request.SortOrder


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
