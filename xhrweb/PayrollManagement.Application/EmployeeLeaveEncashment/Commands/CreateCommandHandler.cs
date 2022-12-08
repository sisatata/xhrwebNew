using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;
using PayrollManagement.Core.Entities;
using PayrollManagement.Core.Specifications;
using System.Linq;
using PayrollManagement.Core.Entities.EmployeeLeaveEncashmentAggregate;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;

namespace PayrollManagement.Application.EmployeeLeaveEncashment.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.StartEmployeeLeaveEncashment, EmployeeLeaveEncashmentCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.EmployeeLeaveEncashment, Guid> repository,
            IReferenceRepository<Employee, Guid> employeeRepository,
            IReferenceRepository<LeaveType, Guid> leaveTypeRepository,
            IReferenceRepository<LeaveBalance, Guid> employeeLeaveBalanceRepository,
            IReferenceRepository<EmployeeCustomConfiguration, Guid> employeeCustomConfigurationRepository,
            IReferenceRepository<AttendanceProcessedData, Guid> attendanceProcessedDataRepository,
            IReferenceRepository<FinancialYearMonth, Guid> financialYearMonthRepository,
            IAsyncRepository<PayrollEntities.EmployeeSalary, Guid> employeeSalaryRepository,
            IReferenceRepository<PayrollEntities.EmployeeSalaryComponentVM, Guid> employeeSalaryComponentRepository,
            IAsyncRepository<PayrollEntities.SalaryStructureComponent, Guid> repositorySalaryStructureComponent,
            IReferenceRepository<AttendanceCalendar, Guid> leaveCalenderRepository,
             IServiceBus serviceBus,
            IConfiguration configuration
    )
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _employeeLeaveBalanceRepository = employeeLeaveBalanceRepository;
            _employeeCustomConfigurationRepository = employeeCustomConfigurationRepository;
            _attendanceProcessedDataRepository = attendanceProcessedDataRepository;
            _financialYearMonthRepository = financialYearMonthRepository;
            _employeeSalaryRepository = employeeSalaryRepository;
            _employeeSalaryComponentRepository = employeeSalaryComponentRepository;
            _repositorySalaryStructureComponent = repositorySalaryStructureComponent;
            _leaveCalenderRepository = leaveCalenderRepository;

            _serviceBus = serviceBus;
            _configuration = configuration;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeeLeaveEncashment, Guid> _repository;
        private readonly IReferenceRepository<Employee, Guid> _employeeRepository;
        private readonly IReferenceRepository<LeaveType, Guid> _leaveTypeRepository;
        private readonly IReferenceRepository<LeaveBalance, Guid> _employeeLeaveBalanceRepository;
        private readonly IReferenceRepository<EmployeeCustomConfiguration, Guid> _employeeCustomConfigurationRepository;
        private readonly IReferenceRepository<AttendanceProcessedData, Guid> _attendanceProcessedDataRepository;
        private readonly IReferenceRepository<FinancialYearMonth, Guid> _financialYearMonthRepository;
        private readonly IReferenceRepository<AttendanceCalendar, Guid> _leaveCalenderRepository;
        private readonly IAsyncRepository<PayrollEntities.EmployeeSalary, Guid> _employeeSalaryRepository;
        private readonly IReferenceRepository<PayrollEntities.EmployeeSalaryComponentVM, Guid> _employeeSalaryComponentRepository;
        private readonly IAsyncRepository<PayrollEntities.SalaryStructureComponent, Guid> _repositorySalaryStructureComponent;

        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;

        public async Task<EmployeeLeaveEncashmentCommandVM> Handle(Commands.V1.StartEmployeeLeaveEncashment request, CancellationToken cancellationToken)
        {
            var response = new EmployeeLeaveEncashmentCommandVM
            {
                Status = false,
                Message = "error"
            };
            try
            {
                var employeeQuery = new EmployeeEnrollmentForLeaveEncashFilterSpecificaion(request.CompanyId, request.EmployeeId, request.EncashDate);
                var employees = await _employeeRepository.ListAsync(employeeQuery).ConfigureAwait(false);

                if (!employees.Any())
                {
                    return response;
                }
                var employee = employees.FirstOrDefault();

                var leaveTypeQuery = new LeaveTypeForLeaveEncashFilterSpecificaion(request.CompanyId);
                var leaveTypes = await _leaveTypeRepository.ListAsync(leaveTypeQuery).ConfigureAwait(false);

                var leaveBalanceQuery = new EmployeeLeaveBalanceForLeaveEncashFilterSpecificaion(request.CompanyId, request.EmployeeId, request.EncashDate);
                var employeeLeaveBalances = await _employeeLeaveBalanceRepository.ListAsync(leaveBalanceQuery).ConfigureAwait(false);

                var employeeCustomConfigurationByCompanyFilterSpecificaion = new EmployeeCustomConfigurationByCompanyFilterSpecificaion(request.CompanyId);
                var employeeCustomConfigurations = await _employeeCustomConfigurationRepository.ListAsync(employeeCustomConfigurationByCompanyFilterSpecificaion).ConfigureAwait(false);

                var attnProcessDataQuery = new AttendanceProcessedEmployeeDataFilterSpecification(request.CompanyId, request.EmployeeId.Value, request.EncashDate.AddMonths(-30), request.EncashDate);
                var attnProcessDatas = await _attendanceProcessedDataRepository.ListAsync(attnProcessDataQuery).ConfigureAwait(false);

                var leaveYears = await _financialYearMonthRepository.GetAll().ConfigureAwait(false);
                //var lastLeaveYear = leaveYears.FirstOrDefault(x => x.YearNumber == 2 && x.CompanyId == request.CompanyId);

                var leaveCalender = await _leaveCalenderRepository.GetByIdAsync(request.FinancialYearId.Value).ConfigureAwait(false);

                var employeeSalaryQuery = new EmployeeSalaryFilterSpecificaion(request.EmployeeId.Value);
                var employeeSalaries = await _employeeSalaryRepository.ListAsync(employeeSalaryQuery).ConfigureAwait(false);

                var employeeSalaryCompQuery = new EmployeeSalaryComponentByCompanyFilterSpecificaion(request.CompanyId);
                var employeeSalaryComponents = await _employeeSalaryComponentRepository.ListAsync(employeeSalaryCompQuery).ConfigureAwait(false);

                var salryStructureComponentFilter = new SalaryStructureComponentByCompanyFilterSpecificaion(request.CompanyId);
                var salaryStructureComponents = await _repositorySalaryStructureComponent.ListAsync(salryStructureComponentFilter).ConfigureAwait(false);

                var employeeLeaveEncashQuery = new EmployeeLeaveEncashmentByEmpFilterSpecificaion(request.EmployeeId.Value);
                var employeeLeaveEncashes = await _repository.ListAsync(employeeLeaveEncashQuery).ConfigureAwait(false);


                var aggregate = new EmployeeLeaveEncashmentAggregate(request.CompanyId, leaveTypes.ToList(),
                    employee, employeeLeaveBalances.ToList(), employeeCustomConfigurations, attnProcessDatas,
                    leaveYears, employeeSalaries, employeeSalaryComponents,
                    salaryStructureComponents, employeeLeaveEncashes);


                aggregate.StartEmployeeLeaveEncashment(
                         request.EmployeeId,
                         request.LeaveTypeId,
                         request.EncashDate,
                         request.FinancialYearId,
                         request.MonthCycleId,
                         request.ELEncashedDays,
                         request.Remarks
                );
                if (aggregate.EmployeeLeaveEncashment.State == TrackingState.Added)
                {
                    var data = await _repository.AddAsync(aggregate.EmployeeLeaveEncashment);

                    response.Status = true;
                    response.Message = "entity created successfully.";
                    response.Id = data.Id;
                }
                else if (aggregate.EmployeeLeaveEncashment.State == TrackingState.Modified)
                {
                    await _repository.UpdateAsync(aggregate.EmployeeLeaveEncashment);

                    response.Status = true;
                    response.Message = "entity created successfully.";
                    response.Id = aggregate.EmployeeLeaveEncashment.Id;
                }

                // publish the event in message queue.

                var @event = new Core.ApplicationEvents.LeaveEncashmentEvent
                {
                    CompanyId = request.CompanyId,
                    EmployeeId = request.EmployeeId.Value,
                    LeaveTypeId = request.LeaveTypeId.Value,
                    LeaveEncashedDays = request.ELEncashedDays,
                    LeaveCalendar = leaveCalender.Year,
                    CommandPublished = DateTime.Now
                };

                await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
