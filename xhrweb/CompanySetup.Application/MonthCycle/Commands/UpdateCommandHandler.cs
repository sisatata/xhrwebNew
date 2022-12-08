using CompanySetup.Application.MonthCycle.Commands.Models;
using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.MonthCycle.Commands
{
  public  class UpdateCommandHandler: IRequestHandler<MonthCycleCommands.UpdateMonthCycle, MonthCycleCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<Core.Entities.MonthCycle, Guid> repository)
        {
            _repository = repository;
        }
        private readonly IAsyncRepository<Core.Entities.MonthCycle, Guid> _repository;

        public async Task<MonthCycleCommandVM> Handle(MonthCycleCommands.UpdateMonthCycle request, CancellationToken cancellationToken)
        {
            var response = new MonthCycleCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateMonthCycle
                    (
                        (request.CompanyId == Guid.Empty || request.CompanyId == null) ? entity.CompanyId : request.CompanyId,
                        (request.FinancialYearId== Guid.Empty || request.FinancialYearId == null) ? entity.FinancialYearId : request.FinancialYearId,
                        request.MonthCycleName ?? entity.MonthCycleName,
                        request.MonthCycleLocalizedName ?? entity.MonthCycleLocalizedName,
                        request.MonthStartDate,
                        request.MonthEndDate,
                        request.TotalWorkingDays,
                        request.RunningFlag,
                        request.IsSelected,
                        request.SortOrder == 0 ? entity.SortOrder : request.SortOrder,
                        request.IsDeleted

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
