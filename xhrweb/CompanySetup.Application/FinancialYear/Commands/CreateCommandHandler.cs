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
   public  class CreateCommandHandler:IRequestHandler<FinancialYearCommands.V1.CreateFinancialYear,FinancialYearCommandVM>
    {

        public CreateCommandHandler(IAsyncRepository<Core.Entities.FinancialYear, Guid> repository)
        {
            _repository = repository;
        }
        private readonly IAsyncRepository<Core.Entities.FinancialYear, Guid> _repository;


        public async Task<FinancialYearCommandVM> Handle(FinancialYearCommands.V1.CreateFinancialYear request, CancellationToken cancellationToken)
        {
            var response = new FinancialYearCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = Core.Entities.FinancialYear.CreateFinancialYear( request.CompanyId,request.FinancialYearName,request.FinancialYearLocalizedName,
                    request.FinancialYearStartDate,request.FinancialYearEndDate,request.IsRunningYear,request.SortOrder,request.IsDeleted);

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
