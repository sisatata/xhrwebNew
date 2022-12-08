using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BonusEntities = PayrollManagement.Core.Entities.EmployeeBonusProcessAggregate;
using PayrollEntities = PayrollManagement.Core.Entities;
using PayrollManagement.Core.Specifications;
using System.Linq;
using PayrollManagement.Core.Entities.EmployeeBonusProcessAggregate;
using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Core.ApplicationEvents;

namespace PayrollManagement.Application.EmployeeBonusProcessedData.Commands
{
    public class StartEmployeeBonusProcessCommandHandler : IRequestHandler<Commands.V1.StartEmployeeBonusProcess, EmployeeBonusProcessedDataCommandVM>
    {
        public StartEmployeeBonusProcessCommandHandler(IAsyncRepository<BonusEntities.EmployeeBonusProcessedData, Guid> repository,
            IAsyncRepository<PayrollEntities.EmployeeSalary, Guid> repositoryEmployeeSalary,
            IAsyncRepository<PayrollEntities.BonusConfigurationAggregate.BonusConfiguration, Guid> repositoryBonusConfiguration,
            IAsyncRepository<PayrollEntities.SalaryStructureComponent, Guid> repositorySalaryStructureComponent,
            IReferenceRepository<PayrollEntities.Employee, Guid> repositoryEmployee,
            IReferenceRepository<PayrollEntities.EmployeeCustomConfiguration, Guid> repositoryEmployeeCustomConfiguration,
            IReferenceRepository<PayrollEntities.AttendanceCalendar, Guid> repositoryAttendanceCalendar,
            IReferenceRepository<PayrollEntities.EmployeeSalaryComponentVM, Guid> repositoryEmployeeSalaryComponent,
            IReferenceRepository<PayrollEntities.CommonLookUpType, Guid> repositoryCommonLookUpType,

            IEmployeeBonusProcessedDataRepository employeeBonusProcessedDataRepository,
            IServiceBus serviceBus,
            IConfiguration configuration,
            ICurrentUserContext userContext
            )
        {
            _repository = repository;
            _repositoryEmployeeSalary = repositoryEmployeeSalary;
            _repositoryEmployeeSalaryComponent = repositoryEmployeeSalaryComponent;
            _repositoryBonusConfiguration = repositoryBonusConfiguration;
            _repositorySalaryStructureComponent = repositorySalaryStructureComponent;
            _repositoryEmployee = repositoryEmployee;
            _repositoryEmployeeCustomConfiguration = repositoryEmployeeCustomConfiguration;
            _repositoryAttendanceCalendar = repositoryAttendanceCalendar;
            _employeeBonusProcessedDataRepository = employeeBonusProcessedDataRepository;
            _repositoryCommonLookUpType = repositoryCommonLookUpType;
            //_activityLogService = activityLogService;
            _serviceBus = serviceBus;
            _configuration = configuration;
            _userContext = userContext;
        }

        private readonly IAsyncRepository<BonusEntities.EmployeeBonusProcessedData, Guid> _repository;

        private readonly IAsyncRepository<PayrollEntities.EmployeeSalary, Guid> _repositoryEmployeeSalary;
        private readonly IReferenceRepository<PayrollEntities.EmployeeSalaryComponentVM, Guid> _repositoryEmployeeSalaryComponent;
        private readonly IAsyncRepository<PayrollEntities.BonusConfigurationAggregate.BonusConfiguration, Guid> _repositoryBonusConfiguration;
        private readonly IAsyncRepository<PayrollEntities.SalaryStructureComponent, Guid> _repositorySalaryStructureComponent;
        private readonly IReferenceRepository<PayrollEntities.Employee, Guid> _repositoryEmployee;
        private readonly IReferenceRepository<PayrollEntities.EmployeeCustomConfiguration, Guid> _repositoryEmployeeCustomConfiguration;
        private readonly IReferenceRepository<PayrollEntities.AttendanceCalendar, Guid> _repositoryAttendanceCalendar;
        private readonly IReferenceRepository<PayrollEntities.CommonLookUpType, Guid> _repositoryCommonLookUpType;

        private readonly IEmployeeBonusProcessedDataRepository _employeeBonusProcessedDataRepository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserContext _userContext;

        public async Task<EmployeeBonusProcessedDataCommandVM> Handle(Commands.V1.StartEmployeeBonusProcess request, CancellationToken cancellationToken)
        {
            var response = new EmployeeBonusProcessedDataCommandVM
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

                var employeeFilter = new EmployeeEnrollmentForBonusProcessFilterSpecificaion(request.CompanyId.Value, Guid.Empty, request.BonusDate.Value);
                var employees = await _repositoryEmployee.ListAsync(employeeFilter).ConfigureAwait(false);
                if (!employees.Any())
                {
                    response.Message = "No active employee found for bonus";
                    return response;
                }

                if (request.CompanyId == null || request.CompanyId == Guid.Empty)
                {
                    request.CompanyId = employees[0].CompanyId;
                }

                var salryStructureComponentFilter = new SalaryStructureComponentByCompanyFilterSpecificaion(request.CompanyId.Value);
                var salaryStructureComponents = await _repositorySalaryStructureComponent.ListAsync(salryStructureComponentFilter).ConfigureAwait(false);

                var employeeSalaryByCompanyFilter = new EmployeeSalaryByCompanyFilterSpecificaion(request.CompanyId.Value);
                var employeeSalaries = await _repositoryEmployeeSalary.ListAsync(employeeSalaryByCompanyFilter).ConfigureAwait(false);

                var employeeSalaryComponentByCompanyFilter = new EmployeeSalaryComponentByCompanyFilterSpecificaion(request.CompanyId.Value);
                var employeeSalaryComponents = await _repositoryEmployeeSalaryComponent.ListAsync(employeeSalaryComponentByCompanyFilter).ConfigureAwait(false);

                var employeeCustomConfigurationByCompanyFilterSpecificaion = new EmployeeCustomConfigurationByCompanyFilterSpecificaion(request.CompanyId.Value);
                var employeeCustomConfigurations = await _repositoryEmployeeCustomConfiguration.ListAsync(employeeCustomConfigurationByCompanyFilterSpecificaion).ConfigureAwait(false);

                var bonusConfigurationByCompanyAndActiveFilter = new BonusConfigurationByCompanyAndActiveFilterSpecification(request.CompanyId.Value, request.BonusDate.Value);
                var bonusConfigurations = await _repositoryBonusConfiguration.ListAsync(bonusConfigurationByCompanyAndActiveFilter).ConfigureAwait(false);

                var employeeBonusProcessedDataFilter = new EmployeeBonusProcessedDataFilterSpecification(request.CompanyId.Value, request.FinancialYearId.Value, request.BonusTypeId.Value);
                var employeeBonusProcessedData = await _repository.ListAsync(employeeBonusProcessedDataFilter).ConfigureAwait(false);
                var bonusType = await _repositoryCommonLookUpType.GetByIdAsync(request.BonusTypeId.Value);
                var employeeBonusProcessAggregate = new EmployeeBonusProcessAggregate(employeeSalaries.ToList(),
                    employeeSalaryComponents.ToList(),
                    employees,
                    bonusConfigurations,
                    employeeCustomConfigurations.ToList(),
                    salaryStructureComponents.ToList(),
                    employeeBonusProcessedData.ToList(),
                    attendanceCalender,
                    request.BonusDate.Value,
                    request.BonusTypeId.Value

                    );

                employeeBonusProcessAggregate.StartBonusProcess();

                await _employeeBonusProcessedDataRepository.Update(employeeBonusProcessAggregate).ConfigureAwait(false);

                var @event = new PayrollActivityLogEvent
                {
                    SystemKeyword = "Payroll",
                    Comment = $"Bonus processed for bonus type {bonusType?.LookUpTypeName} and Bonus Date {request.BonusDate}",
                    //Entity = employeeBonusProcessAggregate,
                    CommandPublished = DateTime.Now,
                    LoginUserId = _userContext.CurrentUserId,
                    CurrentUserCompanyId = _userContext.CurrentUserCompanyId
                };
                await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);

                response.Status = true;
                response.Message = "bonus processed successfully.";



            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
