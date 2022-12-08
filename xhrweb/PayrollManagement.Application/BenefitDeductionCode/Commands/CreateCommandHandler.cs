using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.BenefitDeductionCode.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateBenefitDeductionCode, BenefitDeductionCodeCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.BenefitDeductionCode, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.BenefitDeductionCode, Guid>
        _repository;

        public async Task<BenefitDeductionCodeCommandVM> Handle(Commands.V1.CreateBenefitDeductionCode request, CancellationToken cancellationToken)
        {
            var response = new BenefitDeductionCodeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = PayrollEntities.BenefitDeductionCode.Create(
                         request.CompanyId,
                         request.BenifitDeductionCode,
                         request.BenifitDeductionCodeName
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
