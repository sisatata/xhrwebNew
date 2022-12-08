using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateBenefitDeductionEmployeeAssigned, BenefitDeductionEmployeeAssignedCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.BenefitDeductionEmployeeAssigned, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.BenefitDeductionEmployeeAssigned, Guid>
        _repository;

        public async Task<BenefitDeductionEmployeeAssignedCommandVM> Handle(Commands.V1.CreateBenefitDeductionEmployeeAssigned request, CancellationToken cancellationToken)
        {
            var response = new BenefitDeductionEmployeeAssignedCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = PayrollEntities.BenefitDeductionEmployeeAssigned.Create(

                         request.BenefitDeductionId,
                         request.EmployeeId,
                         request.Remarks,
                         request.IsDeleted,
                         request.StartDate,
                         request.EndDate,
                         request.Amount


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
