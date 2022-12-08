using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using LeaveManagement.Core.Interfaces;
using LeaveManagement.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaveManagement.Core.Entities.LeaveBalanceAggregate
{
    public class LeaveBalanceAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        public LeaveBalanceAggregate(Guid companyId, LeaveCalendar leaveCalendar, List<LeaveType> leaveTypes,
            IReadOnlyList<Employee> employees, List<LeaveBalance> employeeLeaveBalanceDB,
            IReadOnlyList<EmployeeCustomConfiguration> employeeCustomConfigurations,
            IReadOnlyList<LeaveTypeGroup> leaveTypeGroups,
            IReadOnlyList<AttendanceProcessedData> attendanceProcessedDatas,
            FinancialYearMonth lastFinancialYearMonth)
        {
            Guard.Against.NullOrEmptyGuid(companyId, nameof(companyId));
            Guard.Against.Null(leaveCalendar, nameof(leaveCalendar));
            Guard.Against.Null(leaveTypes, nameof(leaveTypes));
            Guard.Against.Null(employees, nameof(employees));

            CompanyId = companyId;
            LeaveCalendar = leaveCalendar;
            _leaveTypes = leaveTypes;
            _employees = employees;
            _employeeLeaveBalanceDB = employeeLeaveBalanceDB;
            _employeeCustomConfigurations = employeeCustomConfigurations;
            _leaveTypeGroups = leaveTypeGroups;
            _attendanceProcessedDatas = attendanceProcessedDatas;
            _lastFinancialYearMonth = lastFinancialYearMonth;
        }

        public Guid CompanyId { get; private set; }
        public LeaveCalendar LeaveCalendar { get; private set; }

        public FinancialYearMonth _lastFinancialYearMonth { get; private set; }

        private readonly List<LeaveType> _leaveTypes = new List<LeaveType>();
        //public IReadOnlyCollection<LeaveType> LeaveTypes => _leaveTypes.AsReadOnly();

        private readonly IReadOnlyList<Employee> _employees = new List<Employee>();
        private List<LeaveBalance> _employeeLeaveBalanceDB;
        private readonly IAsyncRepository<LeaveBalance, Guid> _leaveBalanceRepository;

        public IReadOnlyCollection<Employee> Employees => _employees;

        private readonly List<LeaveBalance> _leaveBalances = new List<LeaveBalance>();
        public IReadOnlyCollection<LeaveBalance> LeaveBalances => _leaveBalances.AsReadOnly();

        private readonly IReadOnlyList<EmployeeCustomConfiguration> _employeeCustomConfigurations = new List<EmployeeCustomConfiguration>();
        public IReadOnlyList<LeaveTypeGroup> _leaveTypeGroups = new List<LeaveTypeGroup>();
        public IReadOnlyList<AttendanceProcessedData> _attendanceProcessedDatas = new List<AttendanceProcessedData>();
        async public void PrepareLeaveBalance()
        {
            // calculate leave balance for a single employee
            foreach (var employee in _employees)
            {
                var oLeaveTypeGroup = new LeaveTypeGroup(0);
                var leaveTypeGroup = _employeeCustomConfigurations.FirstOrDefault(lt => lt.EmployeeId == employee.Id)?.Value;
                if (!string.IsNullOrEmpty(leaveTypeGroup))
                {
                    oLeaveTypeGroup = _leaveTypeGroups.FirstOrDefault(x => x.LeaveTypeGroupName.Trim().ToLower() == leaveTypeGroup.Trim().ToLower());
                    if (oLeaveTypeGroup == null)
                        continue;
                }
                else
                { continue; }
                //var empLeaveTypes = _leaveTypes.FindAll(l => l.LeaveTypeGroupId == oLeaveTypeGroup.Id);

                foreach (var leaveType in _leaveTypes)
                {
                    var oDB = _employeeLeaveBalanceDB.FirstOrDefault(x => x.EmployeeId == employee.Id &&
                    x.LeaveTypeId == leaveType.Id && x.LeaveCalendar == LeaveCalendar.Year);
                    if (oDB == null)
                    {
                        if (!leaveType.IsDeleted && leaveType.LeaveTypeGroupId == oLeaveTypeGroup.Id)
                        {
                            var balance = CalculateBalance(employee, leaveType);
                            if (balance != null)
                            {
                                _leaveBalances.Add(balance);
                            }
                        }

                    }
                    else
                    {
                        if (leaveType.LeaveTypeGroupId == oLeaveTypeGroup.Id)
                        {
                            if (leaveType.IsDeleted)
                            {
                                oDB.DeleteLeaveBalance();
                            }
                            else
                            {
                                if (!leaveType.IsLeaveDaysFixed)
                                {
                                    if (!leaveType.IsBasedWorkingDays)
                                    {
                                        if (employee.JoiningDate <= LeaveCalendar.YearStartDate)
                                        {
                                            oDB.UpdateBalance(leaveType.Balance);
                                        }

                                        if (employee.JoiningDate > LeaveCalendar.YearStartDate)
                                        {
                                            var monthlyBalance = (double)leaveType.Balance / (LeaveCalendar.YearEndDate.MonthDifference(LeaveCalendar.YearStartDate) + 1);
                                            var elligibleMonths = LeaveCalendar.YearEndDate.MonthDifference(employee.JoiningDate) + 1;
                                            var partialBalance = Math.Floor(monthlyBalance * elligibleMonths);
                                            oDB.UpdateBalance(partialBalance);
                                        }
                                    }
                                    else
                                    {
                                        var empAttnDatas = _attendanceProcessedDatas
                                        .ToList().FindAll(r => r.EmployeeId == employee.Id
                                        && r.FinancialYearId == LeaveCalendar.Id && employee.JoiningDate <= DateTime.Now.AddYears(-1));
                                        var totalLeavedays = empAttnDatas == null ? 0.0 : Math.Floor(empAttnDatas.Count / 18.0);

                                        var lastYearLeaveBalance = _employeeLeaveBalanceDB.FirstOrDefault(x => x.CompanyId == leaveType.CompanyId
                                        && x.LeaveCalendar == _lastFinancialYearMonth.FinancialYearName
                                        && x.LeaveTypeId == leaveType.Id);
                                        //var lastYearForwardedValue = 0;

                                        if (lastYearLeaveBalance != null && leaveType.IsCarryForward)
                                        {
                                            if (lastYearLeaveBalance.RemainingBalance + totalLeavedays <= leaveType.ConsecutiveLimit)
                                                totalLeavedays = lastYearLeaveBalance.RemainingBalance + totalLeavedays;
                                            else
                                                totalLeavedays = leaveType.ConsecutiveLimit;
                                        }
                                        oDB.UpdateBalance(totalLeavedays);
                                    }
                                }
                                else
                                {
                                    oDB.UpdateBalance(leaveType.Balance);
                                }
                                //if (balance != null)
                                //{
                                //    oDB.UpdateBalance(balance.RemainingBalance);
                                //}
                            }
                        }
                        else
                        {
                            oDB.DeleteLeaveBalance();
                        }
                        _leaveBalances.Add(oDB);
                    }
                }
            }

        }

        private LeaveBalance CalculateBalance(Employee employee, LeaveType leaveType)
        {
            if (!leaveType.IsLeaveDaysFixed)
            {
                if (!leaveType.IsBasedWorkingDays)
                {
                    if (employee.JoiningDate <= LeaveCalendar.YearStartDate)
                    {
                        return NewBalance(employee, leaveType, leaveType.Balance);
                    }

                    if (employee.JoiningDate > LeaveCalendar.YearStartDate)
                    {
                        var monthlyBalance = (double)leaveType.Balance / (LeaveCalendar.YearEndDate.MonthDifference(LeaveCalendar.YearStartDate) + 1);
                        var elligibleMonths = LeaveCalendar.YearEndDate.MonthDifference(employee.JoiningDate) + 1;
                        var partialBalance = Math.Floor(monthlyBalance * elligibleMonths);
                        return NewBalance(employee, leaveType, partialBalance);
                    }
                }
                else
                {
                    var empAttnDatas = _attendanceProcessedDatas
                                        .ToList().FindAll(r => r.EmployeeId == employee.Id
                                        && r.FinancialYearId == LeaveCalendar.Id && employee.JoiningDate <= DateTime.Now.AddYears(-1));
                    var totalLeavedays = empAttnDatas == null ? 0.0 : Math.Floor(empAttnDatas.Count / 18.0);

                    var lastYearLeaveBalance = _employeeLeaveBalanceDB.FirstOrDefault(x => x.CompanyId == leaveType.CompanyId
                                        && x.LeaveCalendar == _lastFinancialYearMonth.FinancialYearName
                                        && x.LeaveTypeId == leaveType.Id);
                    //var lastYearForwardedValue = 0;

                    if (lastYearLeaveBalance != null && leaveType.IsCarryForward)
                    {
                        if (lastYearLeaveBalance.RemainingBalance + totalLeavedays <= leaveType.ConsecutiveLimit)
                            totalLeavedays = lastYearLeaveBalance.RemainingBalance + totalLeavedays;
                        else
                            totalLeavedays = leaveType.ConsecutiveLimit;
                    }

                    return NewBalance(employee, leaveType, totalLeavedays);
                }
            }
            else
            {
                return NewBalance(employee, leaveType, leaveType.Balance);
            }

            return null;
        }

        //private LeaveBalance CalculateExistingBalance(Employee employee, LeaveType leaveType)
        //{
        //    if (!leaveType.IsLeaveDaysFixed)
        //    {
        //        if (employee.JoiningDate <= LeaveCalendar.YearStartDate)
        //        {
        //            var leaveBalance = LeaveBalance.ad(employee, leaveType, leaveType.Balance);
        //        }

        //        if (employee.JoiningDate > LeaveCalendar.YearStartDate)
        //        {
        //            var monthlyBalance = (double)leaveType.Balance / (LeaveCalendar.YearEndDate.MonthDifference(LeaveCalendar.YearStartDate) + 1);
        //            var elligibleMonths = LeaveCalendar.YearEndDate.MonthDifference(employee.JoiningDate) + 1;
        //            var partialBalance = Math.Floor(monthlyBalance * elligibleMonths);
        //            return NewBalance(employee, leaveType, partialBalance);
        //        }
        //    }
        //    else
        //    {
        //        return NewBalance(employee, leaveType, leaveType.Balance);
        //    }

        //    return null;
        //}

        private LeaveBalance NewBalance(Employee employee, LeaveType leaveType, double balance)
        {
            var leaveBalance = LeaveBalance.CreateLeaveBalance(CompanyId, employee.Id, leaveType.Id, LeaveCalendar.Year, balance, 0, balance);
            leaveBalance.State = TrackingState.Added;
            return leaveBalance;
        }
    }
}
