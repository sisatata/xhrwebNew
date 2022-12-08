using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.IncomeTaxPayerType.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateIncomeTaxPayerType, IncomeTaxPayerTypeCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.IncomeTaxPayerType, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.IncomeTaxPayerType, Guid>
        _repository;

        public async Task<IncomeTaxPayerTypeCommandVM> Handle(Commands.V1.CreateIncomeTaxPayerType request, CancellationToken cancellationToken)
        {
            var response = new IncomeTaxPayerTypeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.IncomeTaxPayerType.Create(
                         
                         request.PayerTypeCode,
                         request.PayerTypeName,
                         request.Remarks,
                         request.IsActive,
                        
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
