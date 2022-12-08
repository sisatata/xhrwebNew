using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompanySetup.Application.FinancialYear.Commands.Models;
using CompanySetup.Core.Interfaces;

namespace CompanySetup.Application.FinancialYear.Commands
{
   public  class MarkAsDeleteCommandHandler
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<Core.Entities.FinancialYear, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<Core.Entities.FinancialYear, Guid> _repository;
        public async Task<FinancialYearCommandVM> Handle(FinancialYearCommands.V1.MarkFinancialYearAsDelete request, CancellationToken cancellationToken)
        {
            var response = new FinancialYearCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkFinancialYearDeleted();
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
