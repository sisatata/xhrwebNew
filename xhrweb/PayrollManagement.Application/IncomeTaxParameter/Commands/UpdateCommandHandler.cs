using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.IncomeTaxParameter.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateIncomeTaxParameter, IncomeTaxParameterCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.IncomeTaxParameter, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.IncomeTaxParameter, Guid> _repository;

        public async Task<IncomeTaxParameterCommandVM>
            Handle(Commands.V1.UpdateIncomeTaxParameter request, CancellationToken cancellationToken)
        {
            var response = new IncomeTaxParameterCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateIncomeTaxParameter(

                         request.ParameterName,
                         request.LimitAmount,
                         request.LimitPercentageOfBasic,
                         request.StartDate,
                         request.EndDate,
                         request.Remarks,
                         request.IsActive,
                         request.IsDeleted,
                         request.CompanyId,
                         request.PayerTypeCode

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

