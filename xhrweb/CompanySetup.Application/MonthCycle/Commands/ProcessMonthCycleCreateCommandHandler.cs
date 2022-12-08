using CompanySetup.Application.MonthCycle.Commands.Models;
using CompanySetup.Core.Entities.ProcessMonthCycleAggregate;
using CompanySetup.Core.Interfaces;
using CompanySetup.Core.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.MonthCycle.Commands
{
    public class ProcessMonthCycleCreateCommandHandler : IRequestHandler<MonthCycleCommands.ProcessMonthCycleCreate, MonthCycleCommandVM>
    {
        public ProcessMonthCycleCreateCommandHandler(IAsyncRepository<Core.Entities.MonthCycle, Guid> repository,
            IAsyncRepository<Core.Entities.CompanyAggregate.Company, Guid> repositoryCompany,
            IAsyncRepository<Core.Entities.FinancialYear, Guid> repositoryFinancialYear,
            IMonthCycleProcessRepository monthCycleProcessRepository,
            IReferenceRepository<Core.Entities.CompanyWiseConfiguration, Guid> repositoryCompanyWiseConfiguration)
        {
            _repository = repository;
            _repositoryCompany = repositoryCompany;
            _repositoryFinancialYear = repositoryFinancialYear;
            _monthCycleProcessRepository = monthCycleProcessRepository;
            _repositoryCompanyWiseConfiguration = repositoryCompanyWiseConfiguration;
        }
        private readonly IAsyncRepository<Core.Entities.MonthCycle, Guid> _repository;
        private readonly IAsyncRepository<Core.Entities.CompanyAggregate.Company, Guid> _repositoryCompany;
        private readonly IAsyncRepository<Core.Entities.FinancialYear, Guid> _repositoryFinancialYear;
        private readonly IMonthCycleProcessRepository _monthCycleProcessRepository;
        private readonly IReferenceRepository<Core.Entities.CompanyWiseConfiguration, Guid> _repositoryCompanyWiseConfiguration;

        public async Task<MonthCycleCommandVM> Handle(MonthCycleCommands.ProcessMonthCycleCreate request, CancellationToken cancellationToken)
        {
            var response = new MonthCycleCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {

                var companyActiveFilter = new CompanyActiveFilterSpecification();
                var companyActiveList = await _repositoryCompany.ListAsync(companyActiveFilter).ConfigureAwait(false);

                if (request.CompanyId != null && request.CompanyId != Guid.Empty)
                {
                    companyActiveList = companyActiveList.ToList().FindAll(x => x.Id == request.CompanyId);
                }
                var companyWiseConfigurationQuery = new CompanyWiseConfigurationAllFilterSpecification();
                var companyWiseConfigurations = await _repositoryCompanyWiseConfiguration.ListAsync(companyWiseConfigurationQuery).ConfigureAwait(false);
                var financialYearQuery = new FinancialYearAllFilterSpecification();
                var financialYears = await _repositoryFinancialYear.ListAsync(financialYearQuery).ConfigureAwait(false);

                var monthCycleQuery = new MonthCycleAllFilterSpecification();
                var monthCycles = await _repository.ListAsync(monthCycleQuery).ConfigureAwait(false);

                var processMonthCycleAggregate = new ProcessMonthCycleAggregate(companyActiveList,
                   financialYears,
                   monthCycles,
                   companyWiseConfigurations
                   );

                processMonthCycleAggregate.StartMonthCycleCreateProcess();
                await _monthCycleProcessRepository.SaveMonthCycle(processMonthCycleAggregate).ConfigureAwait(false);
                response.Status = true;
                response.Message = "entity created successfully.";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
