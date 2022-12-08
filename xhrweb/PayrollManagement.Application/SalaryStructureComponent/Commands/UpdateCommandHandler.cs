using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.SalaryStructureComponent.Commands
{

    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateSalaryStructureComponent, SalaryStructureComponentCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.SalaryStructureComponent, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.SalaryStructureComponent, Guid> _repository;

        public async Task<SalaryStructureComponentCommandVM>
            Handle(Commands.V1.UpdateSalaryStructureComponent request, CancellationToken cancellationToken)
        {
            var response = new SalaryStructureComponentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateSalaryStructureComponent(
                         request.SalaryStrutureId,
                         request.SalaryComponentName,
                         request.Value,
                         request.ValueType,
                         request.PercentOn,
                         request.IsDeleted,
                         request.CompanyId,
                         request.SortOrder

    );

                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity updated successfully.";
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

