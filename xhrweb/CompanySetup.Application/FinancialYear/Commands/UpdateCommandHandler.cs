using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompanySetup.Application.FinancialYear.Commands.Models;
using CompanySetup.Core.Interfaces;
using MediatR;

namespace CompanySetup.Application.FinancialYear.Commands
{
   public  class UpdateCommandHandler:IRequestHandler<FinancialYearCommands.V1.UpdateFinancialYear,FinancialYearCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<Core.Entities.FinancialYear, Guid> repository)
        {
            _repository = repository;
        }
        private readonly IAsyncRepository<Core.Entities.FinancialYear, Guid> _repository;

        public async Task<FinancialYearCommandVM> Handle(FinancialYearCommands.V1.UpdateFinancialYear request, CancellationToken cancellationToken)
        {
            var response = new FinancialYearCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateFinancialYear
                    (
                        (request.CompanyId == Guid.Empty || request.CompanyId == null) ? entity.CompanyId : request.CompanyId,
                        request.FinancialYearName ?? entity.FinancialYearName,
                        request.FinancialYearLocalizedName ?? entity.FinancialYearLocalizedName,
                        request.FinancialYearStartDate ,
                        request.FinancialYearEndDate ,
                        request.IsRunningYear,
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
