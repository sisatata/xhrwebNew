using Attendance.Core.Entities;
using Attendance.Core.Interfaces;
using Attendance.Core.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Attendance.Application.AttendanceProcessedData.Commands.Commands.V1;
using AttendanceEntities = Attendance.Core.Entities;
using ShiftAllocationEntities = Attendance.Core.Entities.ShiftAllocationAggregate;
using Attendance.Core.Entities.AttendanceProcessAggregate;
using Attendance.Persistence.Repositories;
using System.Linq;

namespace Attendance.Application.AttendanceProcessedData.Commands
{
    public class AttendanceProcessDataCommandHandler : IRequestHandler<AttendanceProcessDataCommand, AttendanceProcessedCommandVM>
    {
        private readonly IReferenceRepository<Employee, Guid> _employeeRepository;
        private readonly IReferenceRepository<AttendanceCalendar, Guid> _leaveCalendarRepository;
        private readonly IAttendanceProcessDataRepository _processDataRepository;
        private readonly IAsyncRepository<AttendanceEntities.AttendanceProcessedData, Guid> _attendanceProcessedDB;
        private readonly IAsyncRepository<AttendanceEntities.Shift, Guid> _shiftRepository;
        private readonly IAsyncRepository<ShiftAllocationEntities.ShiftAllocation, Guid> _shiftAllocationRepository;
        private readonly IReferenceRepository<AttendanceEntities.AttendanceCommon, Guid> _attendanceRepository;

        private readonly IAsyncRepository<AttendanceEntities.AttendanceRequest, Guid> _attendanceRequestRepository;
        private readonly IReferenceRepository<AttendanceEntities.EmployeeLeave, Guid> _employeeLeaveRepository;
        private readonly IAsyncRepository<AttendanceEntities.Holiday, Guid> _holidayRepository;
        private readonly IReferenceRepository<AttendanceEntities.MonthCycle, Guid> _monthCycleRepository;

        public AttendanceProcessDataCommandHandler(IReferenceRepository<Employee, Guid> employeeRepository,
            IReferenceRepository<AttendanceCalendar, Guid> leaveCalendarRepository,
            IAttendanceProcessDataRepository processDataRepository,
            IAsyncRepository<AttendanceEntities.Shift, Guid> shiftRepository,
            IAsyncRepository<ShiftAllocationEntities.ShiftAllocation, Guid> shiftAllocationRepository,
            IReferenceRepository<AttendanceEntities.AttendanceCommon, Guid> attendanceRepository,
            IAsyncRepository<AttendanceEntities.AttendanceProcessedData, Guid> attendanceProcessedDB,
            IAsyncRepository<AttendanceEntities.AttendanceRequest, Guid> attendanceRequestRepository,
            IReferenceRepository<AttendanceEntities.EmployeeLeave, Guid> employeeLeaveRepository,
            IAsyncRepository<AttendanceEntities.Holiday, Guid> holidayRepository,
            IReferenceRepository<AttendanceEntities.MonthCycle, Guid> monthCycleRepository)
        {
            _employeeRepository = employeeRepository;
            _leaveCalendarRepository = leaveCalendarRepository;
            _processDataRepository = processDataRepository;
            _shiftRepository = shiftRepository;
            _shiftAllocationRepository = shiftAllocationRepository;
            _attendanceProcessedDB = attendanceProcessedDB;
            _attendanceRepository = attendanceRepository;
            _attendanceRequestRepository = attendanceRequestRepository;
            _employeeLeaveRepository = employeeLeaveRepository;
            _holidayRepository = holidayRepository;
            _monthCycleRepository = monthCycleRepository;

        }

        public async Task<AttendanceProcessedCommandVM> Handle(AttendanceProcessDataCommand request, CancellationToken cancellationToken)
        {
            var response = new AttendanceProcessedCommandVM
            {

                Status = false,
                Message = "Error happened"
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
                    var employeeFilter = new EmployeeFilterSpecification(request.CompanyId, request.ProcessingStartDate, request.ProcessingEndDate);
                    employees = await _employeeRepository.ListAsync(employeeFilter).ConfigureAwait(false);
                }

                if (!employees.Any())
                    return response;
                if (request.CompanyId == null || request.CompanyId == Guid.Empty)
                {
                    request.CompanyId = employees[0].CompanyId;
                }

                request.ProcessingEndDate = request.ProcessingEndDate.AddHours(24).AddSeconds(-1);

                if (request.AttendanceCalendarId == null || request.AttendanceCalendarId == Guid.Empty)
                {
                    var attendanceCalcFilter = new AttendanceCalenderFilterSpecification(request.CompanyId, request.ProcessingStartDate);
                    var attendanceCalendars = await _leaveCalendarRepository.ListAsync(attendanceCalcFilter).ConfigureAwait(false);
                    if (attendanceCalendars.Any())
                    {
                        request.AttendanceCalendarId = attendanceCalendars[0].Id;
                    }
                    else
                        return response;
                }

                var leaveCalendar = await _leaveCalendarRepository.GetByIdAsync(request.AttendanceCalendarId).ConfigureAwait(false);

                var shiftFilter = new ShiftFilterSpecification(request.CompanyId, false);
                var shifts = await _shiftRepository.ListAsync(shiftFilter).ConfigureAwait(false);

                var monthCycleFilter = new MonthCycleFilterSpecification(request.CompanyId, leaveCalendar.Id, request.ProcessingStartDate.AddDays(-1), request.ProcessingEndDate);
                var monthCycles = await _monthCycleRepository.ListAsync(monthCycleFilter).ConfigureAwait(false);

                var employeeLeaveFilter = new EmployeeLeaveFilterSpecification(request.CompanyId, request.ProcessingStartDate, request.ProcessingEndDate);
                var employeeLeaves = await _employeeLeaveRepository.ListAsync(employeeLeaveFilter).ConfigureAwait(false);

                var shiftAllocationFilter = new ShiftAllocationFilterSpecification(request.CompanyId, false);
                var shiftAllocation = await _shiftAllocationRepository.ListAsync(shiftAllocationFilter).ConfigureAwait(false);

                var attendanceFilter = new AttendanceFilterSpecification(request.CompanyId, request.ProcessingStartDate,
                    request.ProcessingEndDate, false);
                var attendanceRecords = await _attendanceRepository.ListAsync(attendanceFilter).ConfigureAwait(false);

                var holidayFilter = new HolidayFilterSpecification(request.CompanyId, request.ProcessingStartDate,
                   request.ProcessingEndDate);
                var holidays = await _holidayRepository.ListAsync(holidayFilter).ConfigureAwait(false);

                holidays = holidays.ToList().FindAll(x => x.StartDate >= leaveCalendar.YearStartDate && x.EndDate <= leaveCalendar.YearEndDate);

                var processedDataFilter = new AttendanceProcessedDataFilterSpecification(request.CompanyId, false);
                var processedDataDB = await _attendanceProcessedDB.ListAsync(processedDataFilter).ConfigureAwait(false);

                var attendanceRequestFilter = new AttendanceRequestFilterSpecification(request.CompanyId, request.ProcessingStartDate,
                    request.ProcessingEndDate, false);
                var attendanceRequestData = await _attendanceRequestRepository.ListAsync(attendanceRequestFilter).ConfigureAwait(false);

                var attendanceProcessAggregate = new AttendanceProcessAggregate(request.CompanyId,
                    request.ProcessingStartDate,
                    request.ProcessingEndDate,
                    leaveCalendar, employees, shifts, shiftAllocation, attendanceRecords, processedDataDB,
                    attendanceRequestData, employeeLeaves, holidays, monthCycles);
                attendanceProcessAggregate.ProcessAttendanceData();
                await _processDataRepository.Update(attendanceProcessAggregate).ConfigureAwait(false);

                response.Status = true;
                response.Message = "success";
            }
            catch (Exception ex)
            {

                //throw ex;
            }

            return response;
        }
    }
}
