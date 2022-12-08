using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using PayrollManagement.Core.Specifications;
using PayrollManagement.Core.Entities.EmployeeSalaryProcessAggregate;
using PayrollManagement.Core.Entities.EmployeeIncomeTaxProcessAggregate;
using PayrollManagement.Core.ApplicationEvents;
using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Core.Entities;
using PayrollManagement.Core.Entities.EmployeePFTransactionAggregate;

namespace PayrollManagement.Application.EmployeeSalaryProcessedData.Commands
{
    public class GenerateEmployeeSalaryProcessedDataCommandHandler : IRequestHandler<Commands.V1.GenerateEmployeeSalaryProcessedData, EmployeeSalaryProcessedDataCommandVM>
    {
        public GenerateEmployeeSalaryProcessedDataCommandHandler(IAsyncRepository<PayrollEntities.EmployeeSalaryProcessedData, Guid> repository,
            IAsyncRepository<PayrollEntities.EmployeeSalaryProcessedDataComponent, Guid> repositoryComponent,
            IAsyncRepository<PayrollEntities.EmployeeSalary, Guid> repositoryEmployeeSalary,
            IAsyncRepository<PayrollEntities.SalaryStructureComponent, Guid> repositorySalaryStructureComponent,

            IAsyncRepository<PayrollEntities.IncomeTaxInvestment, Guid> repositoryIncomeTaxInvestment,
            IAsyncRepository<PayrollEntities.IncomeTaxParameter, Guid> repositoryIncomeTaxParameter,
            IAsyncRepository<PayrollEntities.IncomeTaxPayerType, Guid> repositoryIncomeTaxPayerType,
            IAsyncRepository<PayrollEntities.IncomeTaxSlab, Guid> repositoryIncomeTaxSlab,
            IAsyncRepository<PayrollEntities.EmployeePaidIncomeTax, Guid> repositoryEmployeePaidIncomeTax,
            IAsyncRepository<PayrollEntities.EmployeePFTransaction, Guid> repositoryEmployeePFTransaction,

            IReferenceRepository<PayrollEntities.MonthCycle, Guid> repositoryMonthCycle,
            IReferenceRepository<PayrollEntities.Employee, Guid> repositoryEmployee,
            IReferenceRepository<PayrollEntities.AttendanceProcessedData, Guid> repositoryAttendanceProcessedData,
            IReferenceRepository<PayrollEntities.AttendanceCalendar, Guid> repositoryAttendanceCalendar,
            IReferenceRepository<PayrollEntities.EmployeeSalaryComponentVM, Guid> repositoryEmployeeSalaryComponent,
            IReferenceRepository<PayrollEntities.EmployeeCustomConfiguration, Guid> repositoryEmployeeCustomConfiguration,
            IEmployeeSalaryProcessedDataRepository employeeSalaryProcessedDataRepository,
            IReferenceRepository<FinancialYearMonth, Guid> financialYearMonthRepository,
            IServiceBus serviceBus,
            IConfiguration configuration,
            ICurrentUserContext userContext)
        {
            _repository = repository;
            _repositoryComponent = repositoryComponent;
            _repositoryEmployeeSalary = repositoryEmployeeSalary;
            _repositoryEmployeeSalaryComponent = repositoryEmployeeSalaryComponent;
            _repositorySalaryStructureComponent = repositorySalaryStructureComponent;
            _repositoryMonthCycle = repositoryMonthCycle;
            _repositoryEmployee = repositoryEmployee;
            _repositoryAttendanceProcessedData = repositoryAttendanceProcessedData;
            _repositoryAttendanceCalendar = repositoryAttendanceCalendar;
            _employeeSalaryProcessedDataRepository = employeeSalaryProcessedDataRepository;
            _repositoryEmployeeCustomConfiguration = repositoryEmployeeCustomConfiguration;
            _repositoryEmployeePFTransaction = repositoryEmployeePFTransaction;

            _repositoryIncomeTaxInvestment = repositoryIncomeTaxInvestment;
            _repositoryIncomeTaxParameter = repositoryIncomeTaxParameter;
            _repositoryIncomeTaxPayerType = repositoryIncomeTaxPayerType;
            _repositoryIncomeTaxSlab = repositoryIncomeTaxSlab;
            _repositoryEmployeePaidIncomeTax = repositoryEmployeePaidIncomeTax;
            _financialYearMonthRepository = financialYearMonthRepository;
            _serviceBus = serviceBus;
            _configuration = configuration;
            _userContext = userContext;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeeSalaryProcessedData, Guid> _repository;
        private readonly IAsyncRepository<PayrollEntities.EmployeeSalaryProcessedDataComponent, Guid> _repositoryComponent;

        private readonly IAsyncRepository<PayrollEntities.EmployeeSalary, Guid> _repositoryEmployeeSalary;
        private readonly IAsyncRepository<PayrollEntities.SalaryStructureComponent, Guid> _repositorySalaryStructureComponent;
        private readonly IAsyncRepository<PayrollEntities.EmployeePFTransaction, Guid> _repositoryEmployeePFTransaction;

        private readonly IReferenceRepository<PayrollEntities.MonthCycle, Guid> _repositoryMonthCycle;
        private readonly IReferenceRepository<PayrollEntities.Employee, Guid> _repositoryEmployee;
        private readonly IReferenceRepository<PayrollEntities.AttendanceProcessedData, Guid> _repositoryAttendanceProcessedData;
        private readonly IReferenceRepository<PayrollEntities.EmployeeSalaryComponentVM, Guid> _repositoryEmployeeSalaryComponent;

        private readonly IReferenceRepository<PayrollEntities.AttendanceCalendar, Guid> _repositoryAttendanceCalendar;

        private readonly IReferenceRepository<PayrollEntities.EmployeeCustomConfiguration, Guid> _repositoryEmployeeCustomConfiguration;

        private readonly IEmployeeSalaryProcessedDataRepository _employeeSalaryProcessedDataRepository;

        private readonly IAsyncRepository<PayrollEntities.IncomeTaxInvestment, Guid> _repositoryIncomeTaxInvestment;
        private readonly IAsyncRepository<PayrollEntities.IncomeTaxParameter, Guid> _repositoryIncomeTaxParameter;
        private readonly IAsyncRepository<PayrollEntities.IncomeTaxPayerType, Guid> _repositoryIncomeTaxPayerType;
        private readonly IAsyncRepository<PayrollEntities.IncomeTaxSlab, Guid> _repositoryIncomeTaxSlab;
        private readonly IAsyncRepository<PayrollEntities.EmployeePaidIncomeTax, Guid> _repositoryEmployeePaidIncomeTax;
        private readonly IReferenceRepository<FinancialYearMonth, Guid> _financialYearMonthRepository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserContext _userContext;
        public async Task<EmployeeSalaryProcessedDataCommandVM> Handle(Commands.V1.GenerateEmployeeSalaryProcessedData request, CancellationToken cancellationToken)
        {
            var response = new EmployeeSalaryProcessedDataCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                if (request.FinancialYearId == null || request.FinancialYearId == Guid.Empty)
                {
                    response.Message = "Financial not sent in request";
                    return response;
                }

                var attendanceCalender = await _repositoryAttendanceCalendar.GetByIdAsync(request.FinancialYearId.Value).ConfigureAwait(false);

                if (attendanceCalender == null)
                {
                    response.Message = "No Active Financial Year found";
                    return response;
                }

                if (request.MonthCycleId == null || request.MonthCycleId == Guid.Empty)
                {
                    response.Message = "Month Cycle not sent in request";
                    return response;
                }

                var monthCycle = await _repositoryMonthCycle.GetByIdAsync(request.MonthCycleId.Value).ConfigureAwait(false);

                if (monthCycle == null || monthCycle.RunningFlag == false)
                {
                    response.Message = "Month Cycle not found on Active";
                    return response;
                }

                var financialYearMonths = await _financialYearMonthRepository.GetAll().ConfigureAwait(false);

                IReadOnlyList<PayrollEntities.Employee> employees = new List<PayrollEntities.Employee>();
                //if (request.EmployeeId != null && request.EmployeeId != Guid.Empty)
                //{
                //    var employee = await _repositoryEmployee.GetByIdAsync(request.EmployeeId.Value).ConfigureAwait(false);

                //    employees = new List<PayrollEntities.Employee> { employee };
                //}
                //else
                //{
                //    employees = await _repositoryEmployee.GetAll().ConfigureAwait(false);
                //}
                var employeeFilter = new EmployeeEnrollmentForSalaryGenerateFilterSpecificaion(attendanceCalender.CompanyId, request.EmployeeId, monthCycle.MonthStartDate, monthCycle.MonthEndDate);
                employees = await _repositoryEmployee.ListAsync(employeeFilter).ConfigureAwait(false);
                if (!employees.Any())
                {
                    response.Message = "No active employee found for this month";
                    return response;
                }

                if (request.CompanyId == null || request.CompanyId == Guid.Empty)
                {
                    request.CompanyId = employees[0].CompanyId;
                }

                var attendanceProcessedDataFilter = new AttendanceProcessedDataFilterSpecification(request.CompanyId.Value, monthCycle.MonthStartDate, monthCycle.MonthEndDate);
                var processedDataDB = await _repositoryAttendanceProcessedData.ListAsync(attendanceProcessedDataFilter).ConfigureAwait(false);

                var salryStructureComponentFilter = new SalaryStructureComponentByCompanyFilterSpecificaion(request.CompanyId.Value);
                var salaryStructureComponents = await _repositorySalaryStructureComponent.ListAsync(salryStructureComponentFilter).ConfigureAwait(false);

                var employeeSalaryByCompanyFilter = new EmployeeSalaryByCompanyFilterSpecificaion(request.CompanyId.Value);
                var employeeSalaries = await _repositoryEmployeeSalary.ListAsync(employeeSalaryByCompanyFilter).ConfigureAwait(false);

                var employeeSalaryComponentByCompanyFilter = new EmployeeSalaryComponentByCompanyFilterSpecificaion(request.CompanyId.Value);
                var employeeSalaryComponents = await _repositoryEmployeeSalaryComponent.ListAsync(employeeSalaryComponentByCompanyFilter).ConfigureAwait(false);

                var employeeSalaryProcessedDataByCompanyFilter = new EmployeeSalaryProcessedDataByCompanyFilterSpecification(request.CompanyId.Value, attendanceCalender.Id, monthCycle.Id);
                var employeeSalaryProcessedData = await _repository.ListAsync(employeeSalaryProcessedDataByCompanyFilter).ConfigureAwait(false);

                var employeeSalaryProcessedDataCompByCompanyFilter = new EmployeeSalaryProcessedDataCompByCompanyFilterSpecification(request.CompanyId.Value);
                var employeeSalaryProcessedDataComponent = await _repositoryComponent.ListAsync(employeeSalaryProcessedDataCompByCompanyFilter).ConfigureAwait(false);

                var employeeCustomConfigurationByCompanyFilterSpecificaion = new EmployeeCustomConfigurationByCompanyFilterSpecificaion(request.CompanyId.Value);
                var employeeCustomConfigurations = await _repositoryEmployeeCustomConfiguration.ListAsync(employeeCustomConfigurationByCompanyFilterSpecificaion).ConfigureAwait(false);

                var incomeTaxInvestmentFilterSpecification = new IncomeTaxInvestmentFilterSpecification(request.CompanyId.Value, monthCycle.MonthStartDate, monthCycle.MonthEndDate);
                var incomeTaxInvestments = await _repositoryIncomeTaxInvestment.ListAsync(incomeTaxInvestmentFilterSpecification).ConfigureAwait(false);

                var employeePaidIncomeTaxFilterSpecification = new EmployeePaidIncomeTaxFilterSpecification(request.CompanyId.Value, monthCycle.Id, monthCycle.FinancialYearId);
                var employeePaidIncomeTaxes = await _repositoryEmployeePaidIncomeTax.ListAsync(employeePaidIncomeTaxFilterSpecification).ConfigureAwait(false);

                var incomeTaxSlabFilterSpecification = new IncomeTaxSlabFilterSpecification(request.CompanyId.Value, monthCycle.MonthStartDate, monthCycle.MonthEndDate);
                var incomeTaxSlabs = await _repositoryIncomeTaxSlab.ListAsync(incomeTaxSlabFilterSpecification).ConfigureAwait(false);

                var incomeTaxParameterFilterSpecification = new IncomeTaxParameterFilterSpecification(request.CompanyId.Value, monthCycle.MonthStartDate, monthCycle.MonthEndDate);
                var incomeTaxParameters = await _repositoryIncomeTaxParameter.ListAsync(incomeTaxParameterFilterSpecification).ConfigureAwait(false);

                var incomeTaxPayerTypeFilterSpecification = new IncomeTaxPayerTypeFilterSpecification(request.CompanyId.Value);
                var incomeTaxPayerTypes = await _repositoryIncomeTaxPayerType.ListAsync(incomeTaxPayerTypeFilterSpecification).ConfigureAwait(false);

                var employeePFTransactionCompanyFilterSpecificaion = new EmployeePFTransactionCompanyFilterSpecificaion(request.CompanyId.Value);
                var employeePFTransactionList = await _repositoryEmployeePFTransaction.ListAsync(employeePFTransactionCompanyFilterSpecificaion).ConfigureAwait(false);

                var employeePFTransactionAggregate = new EmployeePFTransactionAggregate(employeePFTransactionList.ToList(),
                    financialYearMonths.ToList(),
                    attendanceCalender,
                    monthCycle, employeeCustomConfigurations.ToList());

                var employeeIncomeTaxProcessedDataAggregate = new EmployeeIncomeTaxProcessedDataAggregate(incomeTaxSlabs.ToList(),
                    incomeTaxPayerTypes.ToList(),
                    incomeTaxParameters.ToList(),
                    incomeTaxInvestments.ToList(),
                    employeePaidIncomeTaxes.ToList(),
                    attendanceCalender,
                    monthCycle
                    );

                var employeeSalaryProcessedDataAggregate = new EmployeeSalaryProcessedDataAggregate(employeeSalaries.ToList(),
                    employeeSalaryComponents.ToList(),
                    attendanceCalender,
                    monthCycle,
                    employees,
                    processedDataDB.ToList(),
                    salaryStructureComponents.ToList(),
                    employeeSalaryProcessedData.ToList(),
                    employeeSalaryProcessedDataComponent.ToList(),
                    employeeCustomConfigurations.ToList(),
                    employeeIncomeTaxProcessedDataAggregate,
                    employeePFTransactionAggregate
                    );

                employeeSalaryProcessedDataAggregate.GenerateSalaryData();

                await _employeeSalaryProcessedDataRepository.Update(employeeSalaryProcessedDataAggregate).ConfigureAwait(false);

                var @event = new PayrollActivityLogEvent
                {
                    SystemKeyword = "Payroll",
                    Comment = $"Salary processed for {monthCycle?.MonthCycleName}, {attendanceCalender.Year}",
                    //Entity = employeeBonusProcessAggregate,
                    CommandPublished = DateTime.Now,
                    LoginUserId = _userContext.CurrentUserId,
                    CurrentUserCompanyId = _userContext.CurrentUserCompanyId
                };
                await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);


                response.Status = true;
                response.Message = "Salary generated successfully.";

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
