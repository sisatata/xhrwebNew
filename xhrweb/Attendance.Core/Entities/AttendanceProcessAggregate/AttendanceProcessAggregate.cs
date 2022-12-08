using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using Attendance.Core.Entities.ShiftAllocationAggregate;
using Attendance.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Attendance.Core.Entities.AttendanceProcessAggregate
{
    public class AttendanceProcessAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        //private readonly ILogger<AttendanceProcessAggregate> _logger;
        //private readonly IReadOnlyList<Shift> _shifts;
        private readonly IReadOnlyList<AttendanceCommon> _attendances;

        public IReadOnlyList<Shift> _shifts { get; }
        public Guid CompanyId { get; private set; }
        public DateTime ProcessingStartDate { get; private set; }
        public DateTime ProcessingEndDate { get; private set; }
        public AttendanceCalendar AttendanceCalendar { get; private set; }
        public IReadOnlyList<Employee> _employees { get; }
        public IReadOnlyList<AttendanceProcessedData> _processedDataDB { get; }
        public IReadOnlyList<ShiftAllocation> _shiftAllocations { get; }

        public IReadOnlyList<AttendanceRequest> _attendanceRequestData { get; }
        public IReadOnlyList<EmployeeLeave> _employeeLeaves { get; }
        public IReadOnlyList<Holiday> _holidays { get; }
        public IReadOnlyList<MonthCycle> _monthCycles { get; }

        public List<AttendanceProcessedData> attendanceProcessedDatas = new List<AttendanceProcessedData>();
        public IReadOnlyCollection<Employee> Employees => _employees;
        public AttendanceProcessAggregate(Guid companyId, DateTime processingStartDate, DateTime processingEndDate,
            AttendanceCalendar attendanceCalendar,
            IReadOnlyList<Employee> employees,
            IReadOnlyList<Shift> shifts,
            IReadOnlyList<ShiftAllocation> shiftAllocations,
            IReadOnlyList<AttendanceCommon> attendances,
            IReadOnlyList<AttendanceProcessedData> processedDataDB,
            IReadOnlyList<AttendanceRequest> attendanceRequestData,
            IReadOnlyList<EmployeeLeave> employeeLeaves,
            IReadOnlyList<Holiday> holidays,
            IReadOnlyList<MonthCycle> monthCycles//, ILogger<AttendanceProcessAggregate> logger
            )
        {
            CompanyId = companyId;
            ProcessingStartDate = processingStartDate;
            ProcessingEndDate = processingEndDate;
            AttendanceCalendar = attendanceCalendar;
            _employees = employees;
            _shifts = shifts;
            _shiftAllocations = shiftAllocations;
            _attendances = attendances;
            _processedDataDB = processedDataDB;
            _attendanceRequestData = attendanceRequestData;
            _employeeLeaves = employeeLeaves;
            _holidays = holidays;
            _monthCycles = monthCycles;
            //_logger = logger;
        }

        async public void ProcessAttendanceData()
        {
            var monthCycle = _monthCycles.FirstOrDefault(x => ProcessingStartDate >= x.MonthStartDate && ProcessingStartDate <= x.MonthEndDate);

            if (monthCycle == null)
                return;

            var employees = from e in _employees
                            join sa in _shiftAllocations on e.Id equals sa.EmployeeId
                            join m in _monthCycles on sa.MonthCycleId equals m.Id
                            where sa.FinancialYearId == AttendanceCalendar.Id
                            && sa.MonthCycleId == monthCycle.Id
                            select e;

           
            // calculate leave balance for a single employee
            var actualStartDate = ProcessingStartDate;
            foreach (var employee in employees.ToList())
            {
                ProcessingStartDate = actualStartDate;
                while (ProcessingStartDate <= ProcessingEndDate)
                {
                    try
                    {


                        DateTime processingDate = ProcessingStartDate.Date;
                        string attendanceStatus = "A";
                        Boolean IsEmployeeInLeave = false;
                        Boolean IsDateInHoliday = false;
                        DateTime lateTime = DateTime.MinValue;
                        DateTime oTHour = DateTime.MinValue;
                        DateTime regularHour = DateTime.MinValue;
                        string remarks = "";
                        decimal? timeInLatitude = null;
                        decimal? timeInLongitude = null;
                        decimal? timeOutLatitude = null;
                        decimal? timeOutLongitude = null;
                        string EmployeeRemarks = "";
                        string ClockInAddress = "";
                        string ClockOutAddress = "";

                        var shift = GetWorkingShift(processingDate, employee.Id);
                        if (shift == null)
                        {
                            ProcessingStartDate = ProcessingStartDate.AddDays(1);
                            continue;
                        }

                        DateTime shiftIn = processingDate.Add(shift.ShiftIn.TimeOfDay).AddSeconds(59);
                        DateTime shiftOut = processingDate.Add(shift.ShiftOut.TimeOfDay).AddSeconds(59);
                        DateTime shiftLate = processingDate.Add(shift.ShiftLate.TimeOfDay).AddSeconds(59);
                        DateTime earlyOut = processingDate.Add(shift.EarlyOut.TimeOfDay).AddSeconds(59);
                        DateTime startRange = shiftIn.AddMinutes(-119);
                        if (startRange > DateTime.Now)
                        {
                            ProcessingStartDate = ProcessingStartDate.AddDays(1);
                            continue;
                        }

                        DateTime endRange = GetShiftEndRange(processingDate, employee.Id);

                        string dayStatus = GetActualDayStatus(processingDate, employee.Id, out IsEmployeeInLeave, out IsDateInHoliday);

                        var attendanceListByEmp = _attendances.ToList().FindAll(x => x.EmployeeId == employee.Id
                        && x.IsDeleted == false && x.AttendanceDate.Value.Date == processingDate.Date);

                        var missOutRequest = _attendanceRequestData.ToList().FirstOrDefault(x => x.EmployeeId == employee.Id
                            && x.StartTime.Value.Date == processingDate
                            && x.ApprovalStatus == ((int)ApprovalStatuses.Approved).ToString()
                            && x.RequestTypeId == ((int)AttendanceRequestTypes.ForgotOutTimePunch));

                        var missInRequest = _attendanceRequestData.ToList().FirstOrDefault(x => x.EmployeeId == employee.Id
                                       && x.StartTime.Value.Date == processingDate
                                       && x.ApprovalStatus == ((int)ApprovalStatuses.Approved).ToString()
                                       && x.RequestTypeId == ((int)AttendanceRequestTypes.ForgotInTimePunch));

                        if ((attendanceListByEmp != null && attendanceListByEmp.Count > 0)
                            || (missInRequest != null)
                            || (missOutRequest != null))
                        {
                            // Get Time in
                            var timeIn = (from p in attendanceListByEmp
                                          where p.AttendanceDate >= startRange &&
                                                p.AttendanceDate < endRange &&
                                                (p.ClockTimeType == (ushort)ClockTypes.InTime ||
                                                (p.ClockTimeType == 0 && p.Punchtype == (ushort)PunchTypes.PunchMachine))
                                          select p)
                                               .Min(r => r.AttendanceDate);
                            if (timeIn != null)
                            {
                                var oTimeIn = attendanceListByEmp.FirstOrDefault(r => r.AttendanceDate == timeIn);
                                if (oTimeIn != null)
                                {
                                    timeInLatitude = oTimeIn.Latitude;
                                    timeInLongitude = oTimeIn.Longitude;
                                    ClockInAddress = oTimeIn.ClockTimeAddress;
                                    if (!string.IsNullOrEmpty(oTimeIn.Remarks))
                                        EmployeeRemarks = string.Format("In Time Remarks: {0}.", oTimeIn.Remarks);
                                }
                                if (timeIn > shiftLate)
                                {
                                    var lateInRequest = _attendanceRequestData.ToList().FirstOrDefault(x => x.EmployeeId == employee.Id && x.StartTime.Value.Date == processingDate
                                    && x.ApprovalStatus == ((int)ApprovalStatuses.Approved).ToString());
                                    if (lateInRequest == null)
                                    {
                                        attendanceStatus = "L";
                                        lateTime = processingDate.Add((timeIn.Value - shiftIn));
                                    }
                                    else
                                    {
                                        attendanceStatus = "P";
                                        lateTime = DateTime.MinValue;

                                        // miss punch In time request aproved


                                    }
                                }
                                else
                                    attendanceStatus = "P";
                            }
                            else if (missInRequest != null)
                            {
                                if (timeIn == null && missInRequest.StartTime != null)
                                {
                                    remarks = string.Format("Clock In Request for '{0}'"
                                        , missInRequest.StartTime.Value.ToString("HH:mm:ss"));
                                }
                                timeIn = missInRequest.StartTime;
                                attendanceStatus = "P";
                                lateTime = DateTime.MinValue;
                            }

                            var timeOut = (from p in attendanceListByEmp
                                           where p.AttendanceDate != timeIn && p.AttendanceDate >= startRange &&
                                                    p.AttendanceDate < endRange &&
                                                    (p.ClockTimeType == (ushort)ClockTypes.OutTime ||
                                                (p.ClockTimeType == 0 && p.Punchtype == (ushort)PunchTypes.PunchMachine))
                                           select p).Max(r => r.AttendanceDate);
                            if (timeOut != null)
                            {
                                var oTimeOut = attendanceListByEmp.FirstOrDefault(r => r.AttendanceDate == timeOut);
                                if (oTimeOut != null)
                                {
                                    timeOutLatitude = oTimeOut.Latitude;
                                    timeOutLongitude = oTimeOut.Longitude;
                                    ClockOutAddress = oTimeOut.ClockTimeAddress;
                                    if (!string.IsNullOrEmpty(oTimeOut.Remarks))
                                        EmployeeRemarks = string.Format("{0} Out Time Remarks: {1}.", EmployeeRemarks, oTimeOut.Remarks);
                                }

                                if (timeOut > shiftOut)
                                {
                                    oTHour = processingDate.Add(timeOut.Value - shiftOut);
                                }

                                if (timeOut < earlyOut)
                                {
                                    remarks = "Early Out";
                                }
                            }

                            // miss punch Out time request aproved

                            if (missOutRequest != null)
                            {
                                if (timeOut != null && missOutRequest.StartTime != null)
                                {
                                    remarks = string.Format("Wrong Clock Out time '{0}' replaced with '{1}'"
                                        , timeOut.Value.ToString("HH:mm:ss"), missOutRequest.StartTime.Value.ToString("HH:mm:ss"));
                                }
                                else if (timeOut == null && missOutRequest.StartTime != null)
                                {
                                    remarks = string.Format("Clock Out Request for '{0}'"
                                        , missOutRequest.StartTime.Value.ToString("HH:mm:ss"));
                                    if (!string.IsNullOrEmpty(remarks))
                                        EmployeeRemarks = missOutRequest.Reason;
                                }
                                timeOut = missOutRequest.StartTime;
                            }


                            if (timeIn != null && timeOut != null)
                            {
                                regularHour = processingDate.Add(timeOut.Value - timeIn.Value);
                            }

                            // missed clockin but has clockout
                            if (timeIn == null && timeOut != null)
                            {
                                attendanceStatus = "L";
                            }

                            //Considering Holiday/Weekend

                            if ("W H".Contains(dayStatus) || IsEmployeeInLeave)
                            {
                                attendanceStatus = dayStatus;
                                shiftIn = DateTime.MinValue;
                                shiftOut = DateTime.MinValue;
                            }
                            if (IsEmployeeInLeave)
                            {
                                timeIn = DateTime.MinValue;
                                timeOut = DateTime.MinValue;
                            }

                            if ("W H".Contains(dayStatus) && (timeIn != null || timeOut != null))
                            {
                                if (dayStatus == "W")
                                    attendanceStatus = "PW";
                                else
                                    attendanceStatus = "PH";
                            }

                            var recordDB = (from p in _processedDataDB
                                            where p.EmployeeId == employee.Id &&
                                                  p.PunchDate == processingDate
                                            select p).FirstOrDefault();
                            if (recordDB == null)
                                NewProcessData(employee.Id, processingDate, AttendanceCalendar.Year, processingDate.Month,
                                    timeIn,
                                    timeOut,
                                    ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? DateTime.MinValue : processingDate.Add(shift.ShiftIn.TimeOfDay),
                                    ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? DateTime.MinValue : processingDate.Add(shift.ShiftOut.TimeOfDay),
                                    DateTime.MinValue,
                                    DateTime.MinValue,
                                    null,
                                    ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? DateTime.MinValue : lateTime,
                                    ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? Guid.Empty : shift.Id,
                                    ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? dayStatus : shift.ShiftCode,
                                    regularHour, oTHour, attendanceStatus, attendanceStatus, null, null, null, remarks, false, this.AttendanceCalendar.Id, null,
                                    timeInLatitude, timeInLongitude, timeOutLatitude, timeOutLongitude,
                                    EmployeeRemarks, ClockInAddress, ClockOutAddress);
                            else
                                ModifyProcessData(
                                    recordDB,
                                    employee.Id,
                                    processingDate,
                                    AttendanceCalendar.Year,
                                    processingDate.Month,
                                timeIn,
                                timeOut,
                                ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? DateTime.MinValue : processingDate.Add(shift.ShiftIn.TimeOfDay),
                                ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? DateTime.MinValue : processingDate.Add(shift.ShiftOut.TimeOfDay),
                                DateTime.MinValue,
                                DateTime.MinValue,
                                null,
                                ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? DateTime.MinValue : lateTime,
                                ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? Guid.Empty : shift.Id,
                                ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? dayStatus : shift.ShiftCode,
                                regularHour, oTHour, attendanceStatus, attendanceStatus, null, null, null, remarks, false, this.AttendanceCalendar.Id, null,
                                timeInLatitude, timeInLongitude, timeOutLatitude, timeOutLongitude,
                                    EmployeeRemarks, ClockInAddress, ClockOutAddress);
                        }
                        else
                        {
                            if (shiftIn.AddHours(1) < DateTime.Now || ("W H".Contains(dayStatus) || IsEmployeeInLeave))
                            {
                                var alreadyProcessedRecord = (from p in _processedDataDB
                                                              where p.EmployeeId == employee.Id &&
                                                                    p.PunchDate == processingDate
                                                              select p).FirstOrDefault();

                                if (alreadyProcessedRecord == null)
                                {
                                    NewProcessData(employee.Id, processingDate, AttendanceCalendar.Year, processingDate.Month,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? dayStatus : shift.ShiftCode,
                                        DateTime.MinValue, DateTime.MinValue,
                                        ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? dayStatus : "A",
                                        ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? dayStatus : "A",
                                        null, null, null, "", false, this.AttendanceCalendar.Id, null, null, null, null, null,
                                        "", "", "");
                                }
                                else
                                {
                                    ModifyProcessData(alreadyProcessedRecord, employee.Id, processingDate, AttendanceCalendar.Year, processingDate.Month,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? dayStatus : shift.ShiftCode,
                                        DateTime.MinValue, DateTime.MinValue,
                                        ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? dayStatus : "A",
                                        ("W H".Contains(dayStatus) || IsEmployeeInLeave) ? dayStatus : "A",
                                        null, null, null, "", false, this.AttendanceCalendar.Id, null, null, null, null, null, "", "", "");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessingStartDate = ProcessingStartDate.AddDays(1);
                    }
                    ProcessingStartDate = ProcessingStartDate.AddDays(1);
                }
            }
        }
        private Shift GetWorkingShift(DateTime processingDate, Guid employeeId)
        {
            var monthCycle = _monthCycles.FirstOrDefault(x => processingDate >= x.MonthStartDate && processingDate <= x.MonthEndDate);
            if (monthCycle == null)
                return null;
            var shiftAllocation = _shiftAllocations.FirstOrDefault(r => r.FinancialYearId.ToString() == this.AttendanceCalendar.Id.ToString()
                    //&& r.DutyMonth == processingDate.ToString("MMMM")
                    && r.MonthCycleId == monthCycle.Id
                    && r.EmployeeId == employeeId);

            if (shiftAllocation != null)
            {
                string shiftName = "";
                var oShift = shiftAllocation.GetType().GetProperty("C" + processingDate.Day.ToString()).GetValue(shiftAllocation, null);
                if (oShift != null)
                {
                    shiftName = oShift.ToString();// shiftAllocation.GetType().GetProperty("C" + processingDate.Day.ToString()).GetValue(shiftAllocation, null).ToString();
                }
                var shift = _shifts.FirstOrDefault(r => r.IsDeleted == false);
                if (!"W H".Contains(shiftName))
                    shift = _shifts.FirstOrDefault(r => r.ShiftCode == shiftName && r.IsDeleted == false);
                else
                {

                    var previousShiftDate = processingDate.AddDays(-4);
                    if (previousShiftDate.Year != processingDate.Year)
                        previousShiftDate = previousShiftDate.AddDays(4);
                    var prevMonthCycle = _monthCycles.FirstOrDefault(x => previousShiftDate >= x.MonthStartDate && previousShiftDate <= x.MonthEndDate);
                    if (prevMonthCycle == null)
                        return null;
                    shiftAllocation = _shiftAllocations.FirstOrDefault(r => r.FinancialYearId.ToString() == this.AttendanceCalendar.Id.ToString()
                                                                        //&& r.DutyMonth == previousShiftDate.ToString("MMMM")
                                                                        && r.MonthCycleId == prevMonthCycle.Id
                                                                        && r.EmployeeId == employeeId);
                    if (shiftAllocation != null)
                    {
                        var oS = shiftAllocation.GetType().GetProperty("C" + previousShiftDate.Day.ToString()).GetValue(shiftAllocation, null);
                        if (oS == null)
                            return null;
                        shiftName = oS.ToString();
                        shift = _shifts.FirstOrDefault(r => r.ShiftCode == shiftName && r.IsDeleted == false);
                    }
                }
                return shift;
            }
            return null;
        }

        private DateTime GetShiftEndRange(DateTime processingDate, Guid employeeId)
        {
            var monthCycle = _monthCycles.FirstOrDefault(x => processingDate >= x.MonthStartDate && processingDate <= x.MonthEndDate);
            if (monthCycle == null)
                return processingDate;
            DateTime returningDate = processingDate.AddDays(1).AddMinutes(-1);
            var shiftAllocation = _shiftAllocations.FirstOrDefault(r => r.FinancialYearId.ToString() == this.AttendanceCalendar.Id.ToString()
                    //&& r.DutyMonth == processingDate.ToString("MMMM")
                    && r.MonthCycleId == monthCycle.Id
                    && r.EmployeeId == employeeId);
            if (shiftAllocation != null)
            {
                string shiftName = "";
                var oShift = shiftAllocation.GetType().GetProperty("C" + processingDate.Day.ToString()).GetValue(shiftAllocation, null);
                if (oShift != null)
                {
                    shiftName = oShift.ToString();// shiftAllocation.GetType().GetProperty("C" + processingDate.Day.ToString()).GetValue(shiftAllocation, null).ToString();
                }
                //string shiftName = shiftAllocation.GetType().GetProperty("C" + processingDate.AddDays(1).Day.ToString()).GetValue(shiftAllocation, null).ToString();
                var shift = _shifts.FirstOrDefault(r => r.IsDeleted == false);
                if (!"W H".Contains(shiftName))
                {
                    shift = _shifts.FirstOrDefault(r => r.ShiftCode == shiftName && r.IsDeleted == false);
                    returningDate = processingDate.AddDays(1).Add(shift.ShiftIn.TimeOfDay).AddMinutes(-120);
                }
                else
                {
                    var nextShiftDate = processingDate.AddDays(3);
                    var nextMonthCycle = _monthCycles.FirstOrDefault(x => nextShiftDate >= x.MonthStartDate && nextShiftDate <= x.MonthEndDate);

                    if (nextMonthCycle == null)
                        return returningDate;

                    shiftAllocation = _shiftAllocations.FirstOrDefault(r => r.FinancialYearId.ToString() == this.AttendanceCalendar.Id.ToString()
                                                                        //&& r.DutyMonth == nextShiftDate.ToString("MMMM")
                                                                        && r.MonthCycleId == nextMonthCycle.Id
                                                                        && r.EmployeeId == employeeId);
                    if (shiftAllocation != null)
                    {
                        oShift = shiftAllocation.GetType().GetProperty("C" + nextShiftDate.Day.ToString()).GetValue(shiftAllocation, null);
                        if (oShift == null)
                            return returningDate;
                        shiftName = oShift.ToString();
                        shift = _shifts.FirstOrDefault(r => r.ShiftCode == shiftName && r.IsDeleted == false);
                    }
                }
                returningDate = processingDate.AddDays(1).Add(shift.ShiftIn.TimeOfDay).AddMinutes(-120);

            }
            return returningDate;
        }
        private string GetActualDayStatus(DateTime processingDate, Guid employeeId, out Boolean IsEmployeeInLeave, out Boolean IsDateInHoliday)
        {
            IsDateInHoliday = false;
            IsEmployeeInLeave = false;
            string shiftName = "W";
            var companyHoliday = _holidays.FirstOrDefault(r => processingDate>= r.StartDate && processingDate <= r.EndDate);
            if (companyHoliday != null)
            {
                shiftName = "H";
                IsDateInHoliday = true;
                return shiftName;
            }
            var employeeLeave = (from el in _employeeLeaves
                                 where el.EmployeeId == employeeId && el.StartDate.Date <= processingDate && el.EndDate.Date >= processingDate
                                 select el).FirstOrDefault();
            if (employeeLeave != null)
            {
                shiftName = employeeLeave.LeaveTypeShortName;
                IsEmployeeInLeave = true;
                return shiftName;
            }
            var monthCycle = _monthCycles.FirstOrDefault(x => processingDate >= x.MonthStartDate && processingDate <= x.MonthEndDate);
            if (monthCycle == null)
                return shiftName;

            var shiftAllocation = _shiftAllocations.FirstOrDefault(r => r.FinancialYearId.ToString() == this.AttendanceCalendar.Id.ToString()
                      //&& r.DutyMonth == processingDate.ToString("MMMM")
                      && r.MonthCycleId == monthCycle.Id
                     && r.EmployeeId == employeeId);
            if (shiftAllocation != null)
            {
                var oShift = shiftAllocation.GetType().GetProperty("C" + processingDate.Day.ToString()).GetValue(shiftAllocation, null);
                if (oShift == null)
                    return shiftName;

                shiftName = shiftAllocation.GetType().GetProperty("C" + processingDate.Day.ToString()).GetValue(shiftAllocation, null).ToString();
            }
            return shiftName;

        }
        private void NewProcessData(Guid? employeeId,
         DateTime punchDate,
         string punchYear,
         Int32? punchMonth,
         DateTime? timeIn,
         DateTime? timeOut,
         DateTime? shiftIn,
         DateTime? shiftOut,
         DateTime? breakIn,
         DateTime? breakOut,
         DateTime? breakLate,
         DateTime? late,
         Guid? shiftId,
         string shiftCode,
         DateTime? regularHour,
         DateTime? oTHour,
         string status,
         string status_V2,
         DateTime? buyerShiftIn,
         DateTime? buyerShiftOut,
         DateTime? buyerOTTime,
         string remarks,
         Boolean isLunchOut,
         Guid? financialYearId,
         Guid? monthCycleId,
         decimal? timeInLatitude,
         decimal? timeInLongitude,
         decimal? timeOutLatitude,
         decimal? timeOutLongitude,

          string employeeRemarks, string clockInAddress, string clockOutAddress)
        {
            var attendanceProcessedData = AttendanceProcessedData.Create(
                employeeId,
                punchDate,
                punchYear,
                punchMonth,
                timeIn,
                timeOut,
                shiftIn,
                shiftOut,
                breakIn,
                breakOut,
                breakLate,
                late,
                shiftId,
                shiftCode,
                regularHour,
                oTHour,
                status,
                status_V2,
                buyerShiftIn,
                buyerShiftOut,
                buyerOTTime,
                remarks,
                isLunchOut,
                financialYearId,
                monthCycleId,
                timeInLatitude,
                timeInLongitude,
                timeOutLatitude,
                timeOutLongitude,
                employeeRemarks,
                clockInAddress,
                clockOutAddress
            );
            attendanceProcessedData.State = TrackingState.Added;
            attendanceProcessedDatas.Add(attendanceProcessedData);
        }

        private void ModifyProcessData(AttendanceProcessedData attendanceProcessedData, Guid? employeeId,
         DateTime punchDate,
         string punchYear,
         Int32? punchMonth,
         DateTime? timeIn,
         DateTime? timeOut,
         DateTime? shiftIn,
         DateTime? shiftOut,
         DateTime? breakIn,
         DateTime? breakOut,
         DateTime? breakLate,
         DateTime? late,
         Guid? shiftId,
         string shiftCode,
         DateTime? regularHour,
         DateTime? oTHour,
         string status,
         string status_V2,
         DateTime? buyerShiftIn,
         DateTime? buyerShiftOut,
         DateTime? buyerOTTime,
         string remarks,
         Boolean isLunchOut,
         Guid? financialYearId,
         Guid? monthCycleId,
         decimal? timeInLatitude,
         decimal? timeInLongitude,
         decimal? timeOutLatitude,
         decimal? timeOutLongitude,
         string employeeRemarks,
         string clockInAddress,
         string clockOutAddress)
        {
            //attendanceProcessedData.EmployeeId = employeeId;
            //attendanceProcessedData.PunchDate = punchDate;
            //attendanceProcessedData.PunchYear = punchYear;
            //attendanceProcessedData.PunchMonth = punchMonth;
            attendanceProcessedData.TimeIn = timeIn;
            attendanceProcessedData.TimeOut = timeOut;
            attendanceProcessedData.ShiftIn = shiftIn;
            attendanceProcessedData.ShiftOut = shiftOut;
            attendanceProcessedData.BreakIn = breakIn;
            attendanceProcessedData.BreakOut = breakOut;
            attendanceProcessedData.BreakLate = breakLate;
            attendanceProcessedData.Late = late;
            attendanceProcessedData.ShiftId = shiftId;
            attendanceProcessedData.ShiftCode = shiftCode;
            attendanceProcessedData.RegularHour = regularHour;
            attendanceProcessedData.OTHour = oTHour;
            attendanceProcessedData.Status = status;
            attendanceProcessedData.Status_V2 = status_V2;
            attendanceProcessedData.BuyerShiftIn = buyerShiftIn;
            attendanceProcessedData.BuyerShiftOut = buyerShiftOut;
            attendanceProcessedData.BuyerOTTime = buyerOTTime;
            attendanceProcessedData.Remarks = remarks;
            attendanceProcessedData.IsLunchOut = isLunchOut;
            attendanceProcessedData.FinancialYearId = financialYearId;
            attendanceProcessedData.MonthCycleId = monthCycleId;
            attendanceProcessedData.TimeInLatitude = timeInLatitude;
            attendanceProcessedData.TimeInLongitude = timeInLongitude;
            attendanceProcessedData.TimeOutLatitude = timeOutLatitude;
            attendanceProcessedData.TimeOutLongitude = timeOutLongitude;
            attendanceProcessedData.EmployeeRemarks = employeeRemarks;
            attendanceProcessedData.ClockInAddress = clockInAddress;
            attendanceProcessedData.ClockOutAddress = clockOutAddress;
            attendanceProcessedData.State = TrackingState.Modified;
            attendanceProcessedDatas.Add(attendanceProcessedData);
        }
    }
}
