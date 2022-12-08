using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.IncomeTaxPayerType.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateIncomeTaxPayerType, IncomeTaxPayerTypeCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.IncomeTaxPayerType, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.IncomeTaxPayerType, Guid> _repository;

        public async Task<IncomeTaxPayerTypeCommandVM>
            Handle(Commands.V1.UpdateIncomeTaxPayerType request, CancellationToken cancellationToken)
        {
            var response = new IncomeTaxPayerTypeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateIncomeTaxPayerType(


                         request.PayerTypeCode,
                         request.PayerTypeName,
                         request.Remarks,
                         request.IsActive,

                         request.CompanyId

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

