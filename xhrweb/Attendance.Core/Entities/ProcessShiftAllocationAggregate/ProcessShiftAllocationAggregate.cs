using ASL.Hrms.SharedKernel;
using Attendance.Core.Entities.ShiftAllocationAggregate;
using Attendance.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Attendance.Core.Entities.ProcessShiftAllocationAggregate
{
    public class ProcessShiftAllocationAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        public ProcessShiftAllocationAggregate(IReadOnlyList<Company> companies,
            IReadOnlyList<Employee> employees,
            IReadOnlyList<ShiftGroup> shiftGroups,
            IReadOnlyList<Shift> shifts,
            IReadOnlyList<AttendanceCalendar> attendanceCalendars,
            IReadOnlyList<MonthCycle> monthCycles,
            IReadOnlyList<ShiftAllocation> shiftAllocations,
            IReadOnlyList<EmployeeCustomConfiguration> employeeCustomConfigurations)
        {
            ShiftAllocationList = new List<ShiftAllocation>();
            _companies = companies;
            _employees = employees;
            _shiftGroups = shiftGroups;
            _shifts = shifts;
            _attendanceCalendars = attendanceCalendars;
            _monthCycles = monthCycles;
            _employeeCustomConfigurations = employeeCustomConfigurations;

            _shiftAllocations = shiftAllocations;

        }

        private IReadOnlyList<Company> _companies { get; set; }
        private IReadOnlyList<Employee> _employees { get; set; }
        private IReadOnlyList<ShiftGroup> _shiftGroups { get; set; }
        private IReadOnlyList<Shift> _shifts { get; set; }
        private IReadOnlyList<AttendanceCalendar> _attendanceCalendars { get; set; }
        private IReadOnlyList<MonthCycle> _monthCycles { get; set; }
        private IReadOnlyList<ShiftAllocation> _shiftAllocations { get; set; }
        private IReadOnlyList<EmployeeCustomConfiguration> _employeeCustomConfigurations { get; set; }
        public List<ShiftAllocation> ShiftAllocationList { get; set; }

        public async void StartShiftAllocationProcess()
        {
            if (_companies == null || _companies.Count < 1)
                return;
            if (_employees == null || _employees.Count < 1)
                return;
            if (_shiftGroups == null || _shiftGroups.Count < 1)
                return;
            if (_shifts == null || _shifts.Count < 1)
                return;
            if (_monthCycles == null || _monthCycles.Count < 1)
                return;

            foreach (var _company in _companies)
            {
                var _employeeListByComp = (from e in _employees
                                           where e.CompanyId == _company.Id && e.StatusId == 1
                                           select e).ToList();
                if (_employeeListByComp == null || _employeeListByComp.Count < 1)
                    continue;

                var _monthListByComp = (from m in _monthCycles
                                        where m.CompanyId == _company.Id
                                        orderby m.MonthStartDate
                                        select m).ToList();
                if (_monthListByComp == null || _monthListByComp.Count < 1)
                    continue;

                foreach (var _employee in _employeeListByComp)
                {
                    var _shiftGroupName = _employeeCustomConfigurations.FirstOrDefault(r => r.EmployeeId == _employee.Id)?.Value;
                    if (string.IsNullOrEmpty(_shiftGroupName))
                        continue;
                    var _shiftGroup = (from g in _shiftGroups
                                       where g.ShiftGroupName.Trim().ToLower() == _shiftGroupName.Trim().ToLower()
                                       select g).FirstOrDefault();

                    var _shiftListByGroup = (from s in _shifts
                                             where s.ShiftGroupID == _shiftGroup?.Id
                                             select s).ToList();

                    if (_shiftListByGroup == null || _shiftListByGroup.Count < 1)
                        continue;

                    foreach (var _monthCycle in _monthListByComp)
                    {
                        if (_employee.JoiningDate.Date > _monthCycle.MonthEndDate || (_employee.QuitDate.HasValue && _employee.QuitDate.Value.Date < _monthCycle.MonthStartDate))
                            continue;
                        var _empShiftAlreadyAllocated = _shiftAllocations.FirstOrDefault(x => x.MonthCycleId == _monthCycle.Id
                        && x.EmployeeId == _employee.Id && x.IsDeleted == false && x.FinancialYearId == _monthCycle.FinancialYearId);
                        if (_empShiftAlreadyAllocated == null)
                        {

                            //
                            Guid financialYearId = _monthCycle.FinancialYearId;
                            string dutyYear = _attendanceCalendars.FirstOrDefault(x => x.Id == financialYearId)?.Year;
                            Guid monthCycleId = _monthCycle.Id;
                            string dutyMonth = _monthCycle.MonthName;
                            Guid employeeId = _employee.Id;
                            Guid companyId = _employee.CompanyId;
                            Guid primaryShiftId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
                            int rotationDay = _shiftGroup.RotationDay;

                            bool isDeleted = false;

                            string[] c = new string[31];
                            //

                            var startDate = _monthCycle.MonthStartDate.Date;
                            var endDate = _monthCycle.MonthEndDate.Date;

                            if (rotationDay == 0)// single shift
                            {
                                var shift = _shiftListByGroup.FirstOrDefault(s => s.IsActive);
                                while (startDate <= endDate)
                                {
                                    if (_employee.JoiningDate.Date > startDate || (_employee.QuitDate.HasValue && _employee.QuitDate.Value.Date < startDate))
                                    {
                                        startDate = startDate.AddDays(1);
                                        continue;
                                    }

                                    if (_shiftGroup.WeekendNames.Trim().ToLower().Contains(startDate.DayOfWeek.ToString().ToLower()))
                                        c[startDate.Day - 1] = "W";
                                    else
                                        c[startDate.Day - 1] = shift.ShiftCode;

                                    startDate = startDate.AddDays(1);
                                }
                            }
                            else if (rotationDay > 1) // multiple shift and rotating
                            {
                                _shiftListByGroup = _shiftListByGroup.OrderBy(x => x.RollingSequence).ToList();
                                List<string> shiftCodes = new List<string>();
                                shiftCodes.Add(_shiftListByGroup[0].ShiftCode);
                                if (_shiftListByGroup.Count > 1)
                                    shiftCodes.Add(_shiftListByGroup[1].ShiftCode);
                                if (_shiftListByGroup.Count > 2)
                                    shiftCodes.Add(_shiftListByGroup[2].ShiftCode);
                                var continueingShift = GetPreviousShift(startDate.AddDays(-1), _employee.Id);
                                int shiftIndex = 0;
                                if (!string.IsNullOrEmpty(continueingShift))
                                {
                                    shiftIndex = shiftCodes.FindIndex(x => x == continueingShift);
                                }
                                int k = (int)(startDate.DayOfWeek) + 1;
                                int numberOfWeekends = NumberOfWeekends(_shiftGroup.WeekendNames.Trim());
                                while (startDate <= endDate)
                                {
                                    while (k <= rotationDay && startDate <= endDate)
                                    {
                                        if (_employee.JoiningDate.Date > startDate || (_employee.QuitDate.HasValue && _employee.QuitDate.Value.Date < startDate))
                                        {
                                            startDate = startDate.AddDays(1);
                                            continue;
                                        }
                                        if (_shiftGroup.WeekendNames.Trim().ToLower().Contains(startDate.DayOfWeek.ToString().ToLower()))
                                        {
                                            c[startDate.Day - 1] = "W";
                                            if (numberOfWeekends == 1 && rotationDay == 7)
                                                k = 8;
                                        }
                                        else
                                            c[startDate.Day - 1] = shiftCodes[shiftIndex];
                                        startDate = startDate.AddDays(1);
                                        k = k + 1;
                                    }
                                    k = 1;// (int)(startDate.DayOfWeek) + 1;
                                    switch (shiftIndex)
                                    {
                                        case 0:
                                            shiftIndex = 1;
                                            break;
                                        case 1:
                                            if (_shiftListByGroup.Count > 2)
                                                shiftIndex = 2;
                                            else
                                                shiftIndex = 0;
                                            break;
                                        case 2:
                                            shiftIndex = 0;
                                            break;
                                        default:
                                            break;
                                    }
                                }

                            }

                            var newSiftAllocation = new ShiftAllocation(Guid.NewGuid());
                            newSiftAllocation.SetShiftAllocation(financialYearId,
                                dutyYear,
                                monthCycleId,
                                dutyMonth,
                                employeeId,
                                companyId,
                                primaryShiftId,
                                rotationDay,
                                c[0],
                                c[1],
                                c[2],
                                c[3],
                                c[4],
                                c[5],
                                c[6],
                                c[7],
                                c[8],
                                c[9],
                                c[10],
                                c[11],
                                c[12],
                                c[13],
                                c[14],
                                c[15],
                                c[16],
                                c[17],
                                c[18],
                                c[19],
                                c[20],
                                c[21],
                                c[22],
                                c[23],
                                c[24],
                                c[25],
                                c[26],
                                c[27],
                                c[28],
                                c[29],
                                c[30],
                                isDeleted
                                );
                            ShiftAllocationList.Add(newSiftAllocation);
                        }
                    }

                }
            }

        }

        private int NumberOfWeekends(string weekEnds)
        {
            if (string.IsNullOrEmpty(weekEnds))
            { return 0; }

            string[] weekendsArr = weekEnds.Split(';');
            if (weekendsArr.Length > 1 && !string.IsNullOrEmpty(weekendsArr[1].Trim()))
                return 2;
            else if (weekendsArr.Length == 1)
                return 1;
            else
                return 0;

        }

        private string GetPreviousShift(DateTime processingDate, Guid employeeId)
        {
            var monthCycle = _monthCycles.FirstOrDefault(x => processingDate >= x.MonthStartDate && processingDate <= x.MonthEndDate);
            if (monthCycle == null)
                return "";
            var shiftAllocation = _shiftAllocations.FirstOrDefault(r => r.FinancialYearId == monthCycle.FinancialYearId
                    && r.MonthCycleId == monthCycle.Id
                    && r.EmployeeId == employeeId);
            if (shiftAllocation == null)
            {
                shiftAllocation = ShiftAllocationList.FirstOrDefault(r => r.FinancialYearId == monthCycle.FinancialYearId
                    && r.MonthCycleId == monthCycle.Id
                    && r.EmployeeId == employeeId);
            }

            if (shiftAllocation != null)
            {
                var oShift = shiftAllocation.GetType().GetProperty("C" + processingDate.Day.ToString()).GetValue(shiftAllocation, null);
                if (oShift == null || oShift.ToString() ==  "W")
                {
                    return "";
                }
                else if (oShift.ToString() == "H")
                {
                    return GetPreviousShift(processingDate.AddDays(-1), employeeId);
                }
                else
                {
                    return oShift.ToString();
                }
            }
            return "";
        }
    }
}
