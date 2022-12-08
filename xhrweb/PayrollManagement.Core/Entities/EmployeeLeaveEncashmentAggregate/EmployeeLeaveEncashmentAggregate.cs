using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayrollManagement.Core.Entities.EmployeeLeaveEncashmentAggregate
{
    public class EmployeeLeaveEncashmentAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        public EmployeeLeaveEncashmentAggregate(Guid companyId, List<LeaveType> leaveTypes,
            Employee employee, List<LeaveBalance> employeeLeaveBalances,
            IReadOnlyList<EmployeeCustomConfiguration> employeeCustomConfigurations,
            IReadOnlyList<AttendanceProcessedData> attendanceProcessedDatas,
            IReadOnlyList<FinancialYearMonth> financialYearMonths,
            IReadOnlyList<EmployeeSalary> employeeSalaryList,
            IReadOnlyList<EmployeeSalaryComponentVM> employeeSalaryComponentList,
            IReadOnlyList<SalaryStructureComponent> salaryStructureComponents,
            IReadOnlyList<EmployeeLeaveEncashment> employeeLeaveEncashments)
        {
            Guard.Against.NullOrEmptyGuid(companyId, nameof(companyId));
            Guard.Against.Null(leaveTypes, nameof(leaveTypes));

            _companyId = companyId;
            _leaveTypes = leaveTypes;
            _employee = employee;
            _employeeLeaveBalances = employeeLeaveBalances;
            _employeeCustomConfigurations = employeeCustomConfigurations;
            _attendanceProcessedDatas = attendanceProcessedDatas;
            _financialYearMonths = financialYearMonths;
            _employeeSalaryList = employeeSalaryList;
            _employeeSalaryComponentList = employeeSalaryComponentList;
            _salaryStructureComponents = salaryStructureComponents;
            _employeeLeaveEncashments = employeeLeaveEncashments;

            EmployeeLeaveEncashment = new EmployeeLeaveEncashment(Guid.NewGuid());
        }

        public EmployeeLeaveEncashmentAggregate(EmployeeLeaveEncashment employeeLeaveEncashment)
        {
            EmployeeLeaveEncashment = employeeLeaveEncashment;
        }

        private Guid _companyId { get; set; }
        private IReadOnlyList<FinancialYearMonth> _financialYearMonths { get; set; }
        private IReadOnlyList<LeaveType> _leaveTypes { get; set; }
        private IReadOnlyList<LeaveBalance> _employeeLeaveBalances { get; set; }
        private Employee _employee { get; set; }
        private IReadOnlyList<EmployeeCustomConfiguration> _employeeCustomConfigurations { get; set; }
        private IReadOnlyList<AttendanceProcessedData> _attendanceProcessedDatas { get; set; }
        private IReadOnlyList<EmployeeSalary> _employeeSalaryList { get; set; }
        private IReadOnlyList<EmployeeSalaryComponentVM> _employeeSalaryComponentList { get; set; }
        private IReadOnlyList<SalaryStructureComponent> _salaryStructureComponents { get; set; }
        private IReadOnlyList<EmployeeLeaveEncashment> _employeeLeaveEncashments { get; set; }
        public EmployeeLeaveEncashment EmployeeLeaveEncashment { get; set; }

        public void StartEmployeeLeaveEncashment(Guid? employeeId,
         Guid? leaveTypeId,
         DateTime encashDate,
         Guid? financialYearId,
         Guid? monthCycleId,
         decimal? eLEncashedDays,
         string remarks)
        {
            Int16 _totalDaysInMonth = 0;
            decimal? _grossSalary = 0.0M;
            decimal? perDaySalary = 0.0M;
            decimal _payableBasic = 0;
            decimal? totalAbsentDeductionAmount = 0.0M;
            Int16 _totalAbsentDays = 0;
            var perDayAttnDeductionAmount = 0.0M;

            var lastFinancialYearMonth = _financialYearMonths.FirstOrDefault(x => x.YearNumber == 2 && x.MonthEndDate <= DateTime.Now.AddMonths(-11).Date);
            if (lastFinancialYearMonth == null)
            {
                throw new ArgumentNullException("Financial Year months not found");
            }
            _totalDaysInMonth = (Int16)((lastFinancialYearMonth.MonthEndDate - lastFinancialYearMonth.MonthStartDate).TotalDays + 1);
            var employeeSalary = _employeeSalaryList.FirstOrDefault(es => es.EmployeeId == employeeId && es.StartDate >= lastFinancialYearMonth.MonthStartDate && es.StartDate <= lastFinancialYearMonth.MonthEndDate);
            if(employeeSalary == null)
            {
                throw new ArgumentNullException("Required salary criteria not found");

            }
            _grossSalary = employeeSalary.GrossSalary;
            //_payableSalary = _grossSalary;
            perDaySalary = _grossSalary / _totalDaysInMonth;
            var _empSalaryComponentList = _employeeSalaryComponentList.ToList().FindAll(x => x.EmployeeSalaryId == employeeSalary.Id);


            //Basic

            var basicComponent = from p in _empSalaryComponentList
                                 join sc in _salaryStructureComponents on p.SalaryStructureComponentId equals sc.Id
                                 where sc.SalaryComponentName.ToUpper() == "BASIC"
                                 select p;


            if (!basicComponent.Any())
                return;
            var _basicComponent = basicComponent.FirstOrDefault();
            _payableBasic = _basicComponent.ComponentAmount.Value;

            _totalAbsentDays = (Int16)_attendanceProcessedDatas.ToList().FindAll(x => x.Status.ToUpper() == "A" && x.EmployeeId == employeeId
            && x.PunchDate >= lastFinancialYearMonth.MonthStartDate && x.PunchDate <= lastFinancialYearMonth.MonthEndDate).Count;

            if (_totalAbsentDays > 0)
            {
                var oAbsentDeductionOn = _employeeCustomConfigurations.FirstOrDefault(x => x.Code.ToLower() == "absentdeductionon" && x.EmployeeId == employeeId);

                if (oAbsentDeductionOn != null && oAbsentDeductionOn.Value.ToLower() == "gross")
                {
                    perDayAttnDeductionAmount = _grossSalary.Value / 30;
                }
                else
                {
                    perDayAttnDeductionAmount = _basicComponent.ComponentAmount.Value / 30;
                }

                totalAbsentDeductionAmount = perDayAttnDeductionAmount * _totalAbsentDays;
            }

            var encashedAmount = ((_grossSalary - totalAbsentDeductionAmount) / _totalDaysInMonth) * eLEncashedDays;
            var existingObj = _employeeLeaveEncashments.FirstOrDefault(x => x.LeaveTypeId == leaveTypeId
            && x.FinancialYearId == financialYearId && x.MonthCycleId == monthCycleId);
            if (existingObj == null)
            {
                EmployeeLeaveEncashment.Create(employeeId, leaveTypeId, encashDate, financialYearId, monthCycleId, eLEncashedDays, encashedAmount, remarks);
            }

            else
            {
                EmployeeLeaveEncashment = existingObj;
                EmployeeLeaveEncashment.UpdateEmployeeLeaveEncashment(employeeId, leaveTypeId, encashDate, financialYearId, monthCycleId, existingObj.ELEncashedDays + eLEncashedDays, existingObj.EncashedAmount + encashedAmount, remarks);
            }
        }
    }
}
