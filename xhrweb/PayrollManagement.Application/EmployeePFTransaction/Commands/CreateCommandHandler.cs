using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeePFTransaction.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeePFTransaction, EmployeePFTransactionCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.EmployeePFTransaction, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeePFTransaction, Guid>
        _repository;

        public async Task<EmployeePFTransactionCommandVM> Handle(Commands.V1.CreateEmployeePFTransaction request, CancellationToken cancellationToken)
        {
            var response = new EmployeePFTransactionCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                //var entity = PayrollEntities.EmployeePFTransaction.Create(

                //         request.EmployeeId,
                //         request.EmlpoyeeDesignationId,
                //         request.EmployeeDepartmentId,
                //         request.PFYearId,
                //         request.PFMonthId,
                //         request.TransactionDate,
                //         request.CompanyContribution,
                //         request.EmployeeContribution,
                //         request.EmployeeInterestRate,
                //         request.CompanyInterestRate,
                //         request.InterestOnEmployeeContribution,
                //         request.InterestOnCompanyContribution,
                //         request.TotalContribution,
                //         request.TotalInterest,
                //         request.EmployeeCumulativeBalance,
                //         request.CompanyCumulativeBalance,
                //         request.TotalCumulativeBalance,
                //         request.Remarks

                //);
                //var data = await _repository.AddAsync(entity);

                //response.Status = true;
                //response.Message = "entity created successfully.";
                //response.Id = entity.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
