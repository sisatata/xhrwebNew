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
   public class CreateCommandHandler: IRequestHandler<MonthCycleCommands.CreateMonthCycle, MonthCycleCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<Core.Entities.MonthCycle, Guid> repository)
        {
            _repository = repository;
        }
        private readonly IAsyncRepository<Core.Entities.MonthCycle, Guid> _repository;

        public async Task<MonthCycleCommandVM> Handle(MonthCycleCommands.CreateMonthCycle request, CancellationToken cancellationToken)
        {
            var response = new MonthCycleCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = Core.Entities.MonthCycle.CreateMonthCycle(request.CompanyId, request.FinancialYearId,request.MonthCycleName,request.MonthCycleLocalizedName,
                             request.MonthStartDate,request.MonthEndDate,request.TotalWorkingDays,request.RunningFlag,request.IsSelected, request.SortOrder,request.IsDeleted);

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
