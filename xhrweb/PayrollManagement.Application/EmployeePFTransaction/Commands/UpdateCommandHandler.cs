using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeePFTransaction.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeePFTransaction, EmployeePFTransactionCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.EmployeePFTransaction, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeePFTransaction, Guid> _repository;

        public async Task<EmployeePFTransactionCommandVM>
            Handle(Commands.V1.UpdateEmployeePFTransaction request, CancellationToken cancellationToken)
        {
            var response = new EmployeePFTransactionCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
    //            entity.UpdateEmployeePFTransaction(

    //                     request.EmployeeId,
    //                     request.EmlpoyeeDesignationId,
    //                     request.EmployeeDepartmentId,
    //                     request.PFYearId,
    //                     request.PFMonthId,
    //                     request.TransactionDate,
    //                     request.CompanyContribution,
    //                     request.EmployeeContribution,
    //                     request.EmployeeInterestRate,
    //                     request.CompanyInterestRate,
    //                     request.InterestOnEmployeeContribution,
    //                     request.InterestOnCompanyContribution,
    //                     request.TotalContribution,
    //                     request.TotalInterest,
    //                     request.EmployeeCumulativeBalance,
    //                     request.CompanyCumulativeBalance,
    //                     request.TotalCumulativeBalance,
    //                     request.Remarks

    //);

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

