using CompanySetup.Application.MonthCycle.Commands.Models;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.MonthCycle.Commands
{
   public  class MarkAsDeleteCommandHandler
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<Core.Entities.MonthCycle, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<Core.Entities.MonthCycle, Guid> _repository;
        public async Task<MonthCycleCommandVM> Handle(MonthCycleCommands.MarkMonthCycleAsDelete request, CancellationToken cancellationToken)
        {
            var response = new MonthCycleCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkMonthCycleDeleted();
                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity deleted successfully.";
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
