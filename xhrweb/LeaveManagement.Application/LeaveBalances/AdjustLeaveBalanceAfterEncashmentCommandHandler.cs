using LeaveManagement.Application.LeaveBalances.Models;
using LeaveManagement.Core.Entities;
using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using LeaveManagement.Core.Interfaces;
using LeaveManagement.Core.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static LeaveManagement.Application.LeaveBalances.LeaveBalanceCommands.V1;

namespace LeaveManagement.Application.LeaveBalances
{
    public class AdjustLeaveBalanceAfterEncashmentCommandHandler : IRequestHandler<AdjustLeaveBalanceAfterEncashmentCommand, LeaveBalanceVM>
    {

        public AdjustLeaveBalanceAfterEncashmentCommandHandler(IReferenceRepository<Employee, Guid> employeeRepository,
            IReferenceRepository<LeaveCalendar, Guid> leaveCalendarRepository,
            IAsyncRepository<LeaveType, Guid> leaveTypeRepository,
            IAsyncRepository<LeaveBalance, Guid> employeeLeaveBalanceRepository,
            ILeaveBalanceRepository leaveBalanceRepository,
            IReferenceRepository<EmployeeCustomConfiguration, Guid> employeeCustomConfigurationRepository,
            IAsyncRepository<LeaveManagement.Core.Entities.LeaveTypeGroup, int> leaveTypeGroupRepository,
            IReferenceRepository<AttendanceProcessedData, Guid> attendanceProcessedDataRepository,
            IReferenceRepository<FinancialYearMonth, Guid> financialYearMonthRepository
            )
        {
            _employeeRepository = employeeRepository;
            _leaveCalendarRepository = leaveCalendarRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _employeeLeaveBalanceRepository = employeeLeaveBalanceRepository;
            _leaveBalanceRepository = leaveBalanceRepository;
            _employeeCustomConfigurationRepository = employeeCustomConfigurationRepository;
            _leaveTypeGroupRepository = leaveTypeGroupRepository;
            _attendanceProcessedDataRepository = attendanceProcessedDataRepository;
            _financialYearMonthRepository = financialYearMonthRepository;
        }

        private readonly IReferenceRepository<Employee, Guid> _employeeRepository;
        private readonly IReferenceRepository<LeaveCalendar, Guid> _leaveCalendarRepository;
        private readonly IAsyncRepository<LeaveType, Guid> _leaveTypeRepository;
        private readonly IAsyncRepository<LeaveBalance, Guid> _employeeLeaveBalanceRepository;
        private readonly IAsyncRepository<LeaveManagement.Core.Entities.LeaveTypeGroup, int> _leaveTypeGroupRepository;
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;
        private readonly IReferenceRepository<EmployeeCustomConfiguration, Guid> _employeeCustomConfigurationRepository;
        private readonly IReferenceRepository<AttendanceProcessedData, Guid> _attendanceProcessedDataRepository;
        private readonly IReferenceRepository<FinancialYearMonth, Guid> _financialYearMonthRepository;

        public async Task<LeaveBalanceVM> Handle(AdjustLeaveBalanceAfterEncashmentCommand request, CancellationToken cancellationToken)
        {
            var response = new LeaveBalanceVM
            {
                Status = false,
                Message = LeaveConstants.Error
            };
            if (request == null) return response;

            try
            {
                IReadOnlyList<Employee> employees = new List<Employee>();
                if (request.EmployeeId != null && request.EmployeeId != Guid.Empty)
                {
                    var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId.Value).ConfigureAwait(false);
                    employees = new List<Employee> { employee };
                }
                else
                {
                    employees = await _employeeRepository.GetAll().ConfigureAwait(false);
                }
                //var leaveCalendar = await _leaveCalendarRepository.GetByIdAsync(request.LeaveCalendarId).ConfigureAwait(false);

                //var leaveTypeFilter = new LeaveTypeFilterSpecification(request.CompanyId);
                //var leaveTypes = await _leaveTypeRepository.ListAsync(leaveTypeFilter).ConfigureAwait(false);

                //var employeeCustomConfigurationByCompanyFilter = new EmployeeCustomConfigurationByCompanyFilterSpecificaion(request.CompanyId, "LeaveGroup");
                //var employeeCustomConfigurations = await _employeeCustomConfigurationRepository.ListAsync(employeeCustomConfigurationByCompanyFilter).ConfigureAwait(false);

                //var leaveTypeGroupByCompanyQuery = new LeaveTypeGroupByCompanySpecification(request.CompanyId);
                //var leaveTypeGroups = await _leaveTypeGroupRepository.ListAsync(leaveTypeGroupByCompanyQuery).ConfigureAwait(false);

                //var employeeLeaveBalanceFilter = new EmployeeLeaveBalanceSpecification(request.CompanyId, false);
                //var employeeLeaveTypeBalance = await _employeeLeaveBalanceRepository.ListAsync(employeeLeaveBalanceFilter).ConfigureAwait(false);

                //var attnProcessDataQuery = new AttendanceProcessDataFilterSpecificaion();
                //var attnProcessDatas = await _attendanceProcessedDataRepository.ListAsync(attnProcessDataQuery).ConfigureAwait(false);

                //var leaveYears = await _financialYearMonthRepository.GetAll().ConfigureAwait(false);
                //var lastLeaveYear = leaveYears.FirstOrDefault(x => x.YearNumber == 2 && x.CompanyId== request.CompanyId);

                var leaveBalanceQuery = new LeaveBalanceByTypeSpecification(request.LeaveTypeId, request.LeaveCalendar, request.EmployeeId.Value, false);
                var employeeLeaveBalances = await _employeeLeaveBalanceRepository.ListAsync(leaveBalanceQuery).ConfigureAwait(false);

                if (!employeeLeaveBalances.Any())
                    return response;
                var employeeLeaveBalance = employeeLeaveBalances[0];
                employeeLeaveBalance.AdjustBalance((double)request.LeaveEncashedDays);

                //var leaveBalance = new LeaveBalanceAggregate(request.CompanyId, leaveCalendar,
                //    leaveTypes.ToList(), employees, employeeLeaveTypeBalance.ToList(),
                //    employeeCustomConfigurations,
                //    leaveTypeGroups, attnProcessDatas, lastLeaveYear);
                //leaveBalance.PrepareLeaveBalance();

                await _employeeLeaveBalanceRepository.UpdateAsync(employeeLeaveBalance).ConfigureAwait(false);

                response.Status = true;
                response.Message = LeaveConstants.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
