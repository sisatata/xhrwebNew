using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.IncomeTaxSlab.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateIncomeTaxSlab, IncomeTaxSlabCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.IncomeTaxSlab, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.IncomeTaxSlab, Guid> _repository;

        public async Task<IncomeTaxSlabCommandVM>
            Handle(Commands.V1.UpdateIncomeTaxSlab request, CancellationToken cancellationToken)
        {
            var response = new IncomeTaxSlabCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateIncomeTaxSlab(

                         
                         request.SlabName,
                         request.Description,
                         request.StartDate,
                         request.EndDate,
                         request.IsRunningFlag,
                         
                         request.CompanyId,
                         request.SlabAmount,
                         request.TaxablePercent,
                         request.TaxPayerTypeCode,
                         request.SlabOrder

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

