using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeePaidIncomeTax.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeePaidIncomeTax, EmployeePaidIncomeTaxCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeePaidIncomeTax, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeePaidIncomeTax, Guid>
        _repository;

        public async Task<EmployeePaidIncomeTaxCommandVM> Handle(Commands.V1.CreateEmployeePaidIncomeTax request, CancellationToken cancellationToken)
        {
            var response = new EmployeePaidIncomeTaxCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                //var entity = EmployeeEntities.EmployeePaidIncomeTax.Create(
                         
                //         request.EmployeeId,
                //         request.FinancialYear,
                //         request.MonthName,
                //         request.FinancialYearId,
                //         request.MonthCycleId,
                //         request.Basic,
                //         request.HouseRent,
                //         request.Medical,
                //         request.Conveyance,
                //         request.FestivalBonus,
                //         request.TaxAmount,
                //         request.ProcessingDate,
                //         request.Remarks,
                //         request.CompanyId
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
