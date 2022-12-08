using ASL.Hrms.SharedKernel;
//using Attendance.Core.Entities;
using PayrollManagement.Core.Entities.EmployeeIncomeTaxProcessAggregate;
using PayrollManagement.Core.Entities.EmployeePFTransactionAggregate;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayrollManagement.Core.Entities.EmployeeSalaryProcessAggregate
{
    public class EmployeeSalaryProcessedDataAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        private List<EmployeeSalary> _employeeSalaryList;
        private List<EmployeeSalaryComponentVM> _employeeSalaryComponentList;
        private List<SalaryStructureComponent> _salaryStructureComponents;
        public List<EmployeeSalaryProcessedData> EmployeeSalaryProcessedDataList { get; set; }
        public List<EmployeeSalaryProcessedDataComponent> EmployeeSalaryProcessedDataComponentList { get; set; }

        public List<EmployeePaidIncomeTax> EmployeePaidIncomeTaxList { get; set; }
        public List<EmployeePFTransaction> EmployeePFTransactionList { get; set; }
        private List<EmployeeSalaryProcessedData> _employeeSalaryProcessedDataListDB { get; set; }
        private List<EmployeeSalaryProcessedDataComponent> _employeeSalaryProcessedDataComponentListDB { get; set; }

        private List<EmployeeCustomConfiguration> _employeeCustomConfiguration { get; set; }

        //public List<EmployeeSalaryProcessedDataComponent> EmployeeSalaryProcessedDataComponentList { get; set; }
        public DateTime ProcessingStartDate { get; private set; }
        public DateTime ProcessingEndDate { get; private set; }
        public AttendanceCalendar AttendanceCalendar { get; private set; }
        public MonthCycle _monthCycle { get; private set; }
        private IReadOnlyList<Employee> _employees { get; }
        private List<AttendanceProcessedData> _attendanceProcessedDataList { get; }
        private EmployeeIncomeTaxProcessedDataAggregate _employeeIncomeTaxProcessedDataAggregate { get; }
        private PayrollManagement.Core.Entities.EmployeePFTransactionAggregate.EmployeePFTransactionAggregate _employeePFTransactionAggregate { get; }
        public EmployeeSalaryProcessedDataAggregate(List<EmployeeSalary> employeeSalaryList,
            List<EmployeeSalaryComponentVM> employeeSalaryComponentList,

            //DateTime processingStartDate,
            //DateTime processingEndDate,
            AttendanceCalendar attendanceCalendar,
            MonthCycle monthCycle,
            IReadOnlyList<Employee> employees,
            List<AttendanceProcessedData> attendanceProcessedDataList,
            List<SalaryStructureComponent> salaryStructureComponents,
            List<EmployeeSalaryProcessedData> employeeSalaryProcessedDataListDB,
            List<EmployeeSalaryProcessedDataComponent> employeeSalaryProcessedDataComponentListDB,
            List<EmployeeCustomConfiguration> employeeCustomConfiguration,
            EmployeeIncomeTaxProcessedDataAggregate employeeIncomeTaxProcessedDataAggregate,
            PayrollManagement.Core.Entities.EmployeePFTransactionAggregate.EmployeePFTransactionAggregate employeePFTransactionAggregate)
        {
            _employeeSalaryList = employeeSalaryList;
            _employeeSalaryComponentList = employeeSalaryComponentList;
            EmployeeSalaryProcessedDataList = new List<EmployeeSalaryProcessedData>();
            EmployeeSalaryProcessedDataComponentList = new List<EmployeeSalaryProcessedDataComponent>();
            //ProcessingStartDate = processingStartDate;
            //ProcessingEndDate = processingEndDate;
            AttendanceCalendar = attendanceCalendar;
            _monthCycle = monthCycle;
            _employees = employees;
            _attendanceProcessedDataList = attendanceProcessedDataList;
            _salaryStructureComponents = salaryStructureComponents;
            _employeeSalaryProcessedDataListDB = employeeSalaryProcessedDataListDB;
            _employeeSalaryProcessedDataComponentListDB = employeeSalaryProcessedDataComponentListDB;
            _employeeCustomConfiguration = employeeCustomConfiguration;
            _employeeIncomeTaxProcessedDataAggregate = employeeIncomeTaxProcessedDataAggregate;
            _employeePFTransactionAggregate = employeePFTransactionAggregate;
        }
        async public void GenerateSalaryData()
        {

            foreach (var employee in _employees)
            {
                Int16 _totalDaysInMonth = 0;
                Int16 _totalWorkingDays = 0;
                Int16 _totalPresentDays = 0;
                Int16 _totalAbsentDays = 0;
                Int16 _totalLateDays = 0;
                Int16 _totalLeaveDays = 0;
                Int16 _weeklyOffDays = 0;
                Int16 _governmentOffDays = 0;
                Int16 _totalWorkingHoliday = 0;
                string _oTHour = "";
                decimal? _oTRate = 0.0M;
                decimal? _oTAmount = 0.0M;
                decimal? _grossSalary = 0.0M;
                decimal? _payableSalary = 0.0M;
                decimal? _totalDeductionAmount = 0.0M;
                decimal? totalAbsentDeductionAmount = 0.0M;
                decimal? totalLateDeductionAmount = 0.0M;
                decimal? _netPayableAmount = 0.0M;
                Int16? _stampCost = 0;
                DateTime _processDate = DateTime.Now;
                decimal _payableBasic = 0;
                decimal _payableHouseRent = 0;
                decimal _payableMedicalAllowance = 0;
                decimal _payableConveyance = 0;
                decimal _payableFestivalBonus = 0;
                decimal _payableCCToProvidentFund = 0;
                int taxPayerType = 1;

                //
                decimal? totalHolidaySalary = 0.0M;
                decimal? JoinOrQuitDeduct = 0.0M;
                decimal? perDaySalary = 0.0M;
                var perDayAttnDeductionAmount = 0.0M;
                var perDayLateDeductionAmount = 0.0M;
                decimal _joinOrQuitDays = 0.0M;
                _totalWorkingDays = (Int16)_monthCycle.TotalWorkingDays;
                _totalDaysInMonth = (Int16)((_monthCycle.MonthEndDate - _monthCycle.MonthStartDate).TotalDays + 1);
                // calculation purpose

                var _empAttendanceProcessedDataList = _attendanceProcessedDataList.FindAll(r => r.EmployeeId == employee.Id);
                _totalPresentDays = (Int16)_empAttendanceProcessedDataList.FindAll(x => x.Status.ToUpper() == "P" ||
                x.Status.ToUpper() == "L").Count;
                // no salary generate if one not present for a single day
                if (_totalPresentDays < 1)
                    continue;
                _totalLateDays = (Int16)_empAttendanceProcessedDataList.FindAll(x => x.Status.ToUpper() == "L").Count;

                _totalAbsentDays = (Int16)_empAttendanceProcessedDataList.FindAll(x => x.Status.ToUpper() == "A" &&
                x.PunchDate >= employee.JoiningDate && (employee.QuitDate == null || x.PunchDate <= employee.QuitDate)).Count;
                _totalLeaveDays = (Int16)_empAttendanceProcessedDataList.FindAll(x => x.Status.ToUpper() == "CL" ||
                x.Status.ToUpper() == "ML" || x.Status.ToUpper() == "SL" || x.Status.ToUpper() == "EL").Count;
                _weeklyOffDays = (Int16)_empAttendanceProcessedDataList.FindAll(x => x.Status.ToUpper() == "W").Count;
                _governmentOffDays = (Int16)_empAttendanceProcessedDataList.FindAll(x => x.Status.ToUpper() == "H").Count;
                _totalWorkingHoliday = (Int16)_empAttendanceProcessedDataList.FindAll(x => x.Status.ToUpper() == "PH"
                || x.Status.ToUpper() == "PW").Count;
                //_oTHour = _empAttendanceProcessedDataList.sum(x => x.Status.ToLower() == "ph"
                //|| x.Status.ToLower() == "wh");

                string lateDeductionRule = "";
                var employeeSalary = _employeeSalaryList.FirstOrDefault(es => es.EmployeeId == employee.Id);
                _grossSalary = employeeSalary.GrossSalary;
                _payableSalary = _grossSalary;
                perDaySalary = _grossSalary / _totalDaysInMonth;
                var _empSalaryComponentList = _employeeSalaryComponentList.FindAll(x => x.EmployeeSalaryId == employeeSalary.Id);
                /*foreach(var item in _salaryStructureComponents)
                {
                    item.SalaryComponentName.Trim();
                }*/

                //Basic

                var basicComponent = from p in _empSalaryComponentList
                                     join sc in _salaryStructureComponents on p.SalaryStructureComponentId equals sc.Id
                                     where sc.SalaryComponentName.ToUpper() == "BASIC"
                                     select p;


                if (!basicComponent.Any())
                    continue;
                var _basicComponent = basicComponent.FirstOrDefault();
                _payableBasic = _basicComponent.ComponentAmount.Value;

                //HouseRent 
                var houseRentComponent = from p in _empSalaryComponentList
                                         join sc in _salaryStructureComponents on p.SalaryStructureComponentId equals sc.Id
                                         where sc.SalaryComponentName.ToUpper() == "HOUSE RENT"
                                         select p;

                if (houseRentComponent.Any())
                {
                    var _houseRentComponent = houseRentComponent.FirstOrDefault();
                    _payableHouseRent = _houseRentComponent.ComponentAmount.Value;
                }

                //Medical 
                var medicalAllowanceComponent = from p in _empSalaryComponentList
                                                join sc in _salaryStructureComponents on p.SalaryStructureComponentId equals sc.Id
                                                where sc.SalaryComponentName.ToUpper() == "MEDICAL"
                                                select p;

                if (medicalAllowanceComponent.Any())
                {
                    var _medicalAllowanceComponent = medicalAllowanceComponent.FirstOrDefault();
                    _payableMedicalAllowance = _medicalAllowanceComponent.ComponentAmount.Value;
                }

                //Conveyance 
                var payableConveyanceComponent = from p in _empSalaryComponentList
                                                 join sc in _salaryStructureComponents on p.SalaryStructureComponentId equals sc.Id
                                                 where sc.SalaryComponentName.ToUpper() == "CONVEYANCE"
                                                 select p;

                if (payableConveyanceComponent.Any())
                {
                    var _payableConveyanceComponent = payableConveyanceComponent.FirstOrDefault();
                    _payableConveyance = _payableConveyanceComponent.ComponentAmount.Value;
                }

                if (_totalAbsentDays > 0)
                {
                    var oAbsentDeductionOn = _employeeCustomConfiguration.FirstOrDefault(x => x.Code.ToLower() == "absentdeductionon" && x.EmployeeId == employee.Id);

                    if (oAbsentDeductionOn != null && oAbsentDeductionOn.Value.ToLower() == "gross")
                    {
                        perDayAttnDeductionAmount = _grossSalary.Value / 30;
                    }
                    else
                    {
                        perDayAttnDeductionAmount = _basicComponent.ComponentAmount.Value / 30;
                    }

                    totalAbsentDeductionAmount = perDayAttnDeductionAmount * _totalAbsentDays;
                    _totalDeductionAmount += totalAbsentDeductionAmount;
                    //_payableBasic -= totalAbsentDeductionAmount.Value;
                }

                if (_totalLateDays > 0)
                {
                    var oLateDeductionConfig = _employeeCustomConfiguration.FirstOrDefault(x => x.Code.ToLower() == "latedeductonapplicable" && x.EmployeeId == employee.Id);
                    if (oLateDeductionConfig != null && !string.IsNullOrEmpty(oLateDeductionConfig.Value))
                    {
                        oLateDeductionConfig.Value = oLateDeductionConfig.Value.Trim().ToLower();

                        //var _host = GetValueFromString(oLateDeductionConfig.Value, "Server", ";");

                        string firstSetting = "";
                        string firstSettingType = "";
                        string firstSettingValue = "";
                        string secondSetting = "";
                        string secondSettingType = "";
                        string secondSettingValue = "";

                        List<string> configs = oLateDeductionConfig.Value.Split('>').ToList();
                        firstSetting = configs[0].ToString();
                        if (firstSetting.Split("=").Length > 1)
                        {
                            firstSettingType = firstSetting.Split("=").ToList()[0].ToString();
                            firstSettingValue = firstSetting.Split("=").ToList()[1].ToString();
                        }
                        if (configs.Count > 1)
                        {
                            secondSetting = configs[1].ToString();
                            secondSettingType = secondSetting.Split("=").ToList()[0].ToString();
                            if (secondSetting.Split("=").Length > 1)
                            {
                                secondSettingType = secondSetting.Split("=").ToList()[0].ToString();
                                secondSettingValue = secondSetting.Split("=").ToList()[1].ToString();
                            }
                        }
                        int late = 0;
                        int deduct = 0;
                        if (firstSettingType == "gross")
                        {
                            if (firstSettingValue.Split(":").Length > 1)
                            {
                                late = Convert.ToInt32(firstSettingValue.Split(":")[0]);
                                deduct = Convert.ToInt32(firstSettingValue.Split(":")[1]);
                                decimal modLate = _totalLateDays / late;
                                modLate = Math.Floor(modLate);
                                perDayLateDeductionAmount = _grossSalary.Value / 30;
                                totalLateDeductionAmount = perDayLateDeductionAmount * modLate * deduct;
                                _totalDeductionAmount += totalLateDeductionAmount;
                                lateDeductionRule = $"{deduct} day(s)  gross deduct for {late} days late. Per day gross {perDayLateDeductionAmount.ToString("0.##")} and deducted day(s) {modLate}. Total Late {_totalLateDays}";
                            }
                        }
                        //_payableBasic -= totalAbsentDeductionAmount.Value;
                    }
                }

                var oHolidayConfig = _employeeCustomConfiguration.FirstOrDefault(x => x.Code == "HOLIDAY_BILL" && x.EmployeeId == employee.Id);
                if (oHolidayConfig != null && oHolidayConfig.Value.ToUpper() == "TRUE")
                {
                    if (_totalWorkingHoliday > 0)
                    {
                        totalHolidaySalary = perDaySalary * _totalWorkingHoliday;
                    }
                }


                // if ot enabled
                var oOTConfig = _employeeCustomConfiguration.FirstOrDefault(x => x.Code == "OT_BILL" && x.EmployeeId == employee.Id);
                if (oOTConfig != null && oOTConfig.Value.ToUpper() == "TRUE")
                {
                    var OTSeconds = _empAttendanceProcessedDataList.Sum(x => x.OTHour.Value.TimeOfDay.TotalSeconds);
                    if (OTSeconds > 900)
                    {
                        _oTRate = (_basicComponent.ComponentAmount.Value * 2) / 208;
                        var _oTSpan = TimeSpan.FromSeconds(OTSeconds);
                        _oTAmount = _oTRate * (decimal)_oTSpan.TotalHours;
                        _oTHour = string.Format("{0}:{1}:{2}", ((int)_oTSpan.TotalHours), _oTSpan.Minutes, _oTSpan.Seconds);
                    }
                }

                if (employee.JoiningDate > _monthCycle.MonthStartDate)
                {
                    _joinOrQuitDays = (decimal)((employee.JoiningDate.Date - _monthCycle.MonthStartDate.Date).TotalDays);
                    JoinOrQuitDeduct = _joinOrQuitDays * perDaySalary;
                    _totalDeductionAmount += JoinOrQuitDeduct;
                }

                if (employee.QuitDate.HasValue && employee.QuitDate < _monthCycle.MonthStartDate)
                {
                    _joinOrQuitDays = _joinOrQuitDays + (decimal)((_monthCycle.MonthEndDate - employee.QuitDate.Value).TotalDays + 1);
                    JoinOrQuitDeduct = JoinOrQuitDeduct + ((decimal)((_monthCycle.MonthEndDate - employee.QuitDate.Value).TotalDays + 1) * perDaySalary);
                    _totalDeductionAmount += JoinOrQuitDeduct;
                }
                // check incometax deduction enable or disable will implement in future
                var _employeePaidIncomeTax = new EmployeePaidIncomeTax(Guid.NewGuid());

                var oIncomeTaxApplicable = _employeeCustomConfiguration.FirstOrDefault(x => x.Code.ToLower() == "incometaxapplicable" && x.EmployeeId == employee.Id);
                if (oIncomeTaxApplicable != null && oIncomeTaxApplicable.Value.ToUpper() == "TRUE")
                {
                    _employeePaidIncomeTax = _employeeIncomeTaxProcessedDataAggregate.CalculateIncomeTax(employee.Id, _payableBasic, _payableHouseRent, _payableMedicalAllowance, _payableConveyance,
                      0, 0, taxPayerType, _monthCycle.MonthStartDate);
                    if (_employeePaidIncomeTax != null && _employeePaidIncomeTax.TaxAmount > 0)
                    {
                        _payableSalary -= _employeePaidIncomeTax.TaxAmount;
                        _totalDeductionAmount += _employeePaidIncomeTax.TaxAmount;
                        if (EmployeePaidIncomeTaxList == null)
                            EmployeePaidIncomeTaxList = new List<EmployeePaidIncomeTax>();
                        EmployeePaidIncomeTaxList.Add(_employeePaidIncomeTax);
                    }
                }

                // check PF(Provident Fund) deduction enable or disable 
                var _employeePFTransaction = new EmployeePFTransaction(Guid.NewGuid());

                var oPFDeductionApplicable = _employeeCustomConfiguration.FirstOrDefault(x => x.Code.ToLower() == "pfapplicable" && x.EmployeeId == employee.Id);
                if (oPFDeductionApplicable != null && oPFDeductionApplicable.Value.ToUpper() == "TRUE")
                {
                    _employeePFTransaction = _employeePFTransactionAggregate.CalculateProvidentFund(employee, _payableBasic, _grossSalary.Value, _monthCycle.MonthStartDate);
                    if (_employeePFTransaction != null && _employeePFTransaction.EmployeeContribution > 0)
                    {
                        _payableSalary -= _employeePFTransaction.EmployeeContribution;
                        _totalDeductionAmount += _employeePFTransaction.EmployeeContribution;
                        if (EmployeePFTransactionList == null)
                            EmployeePFTransactionList = new List<EmployeePFTransaction>();
                        EmployeePFTransactionList.Add(_employeePFTransaction);
                    }
                }

                _payableSalary = (_payableSalary.Value + totalHolidaySalary.Value + _oTAmount.Value) -
                    (totalAbsentDeductionAmount + JoinOrQuitDeduct + totalLateDeductionAmount);

                _netPayableAmount = _payableSalary - _stampCost;

                var employeeSalaryProcessed = _employeeSalaryProcessedDataListDB.FirstOrDefault(x => x.EmployeeId == employee.Id
                && x.FinancialYearId == AttendanceCalendar.Id && x.MonthCycleId == _monthCycle.Id);


                if (employeeSalaryProcessed == null)
                {
                    employeeSalaryProcessed = new EmployeeSalaryProcessedData(Guid.NewGuid());
                    employeeSalaryProcessed.Create(employee.Id,
                        AttendanceCalendar.Id,
                        _monthCycle.Id,
                        1,
                        employee.CompanyId,//Guid ? companyId,
                        employee.BranchId, //Guid ? branchId,
                        employee.DepartmentId, //Guid ? departmentId,
                        employee.PositionId, //Guid ? positionId,
                        employeeSalary.GradeId, //Guid ? gradeId,
                        _totalDaysInMonth, //Int16 ? totalDaysInMonth,
                        _totalWorkingDays, //Int16 ? totalWorkingDays,
                        _totalPresentDays, //Int16 ? totalPresentDays,
                        _totalAbsentDays, //Int16 ? totalAbsentDays,
                        _totalLeaveDays, //Int16 ? totalLeaveDays,
                        _weeklyOffDays, //Int16 ? weeklyOffDays,
                        _governmentOffDays, //Int16 ? governmentOffDays,
                        _totalWorkingHoliday, //Int16 ? totalWorkingHoliday,
                        _oTHour, //decimal ? oTHour,
                        _oTRate, //decimal ? oTRate,
                        _grossSalary,//decimal ? grossSalary,
                        _payableSalary, //decimal ? payableSalary,
                        _totalDeductionAmount,//decimal ? totalDeductionAmount,
                        _netPayableAmount,  //decimal ? netPayableAmount,
                        _processDate, //DateTime ? processDate,
                        _stampCost,//Int16 ? stampCost,
                        false,//Boolean isDeleted,
                        employeeSalary.Id, //Guid ? emloyeeSalaryId,
                        false, //Boolean isApproved,
                        Guid.Empty, //Guid ? approvedBy,
                        DateTime.MinValue, //DateTime ? approvedTime,
                        "" //string remarks
                        );
                }
                else
                {
                    employeeSalaryProcessed.UpdateEmployeeSalaryProcessedData(employee.Id,
                            AttendanceCalendar.Id,
                            _monthCycle.Id,
                            1,
                            employee.CompanyId,//Guid ? companyId,
                            employee.BranchId, //Guid ? branchId,
                            employee.DepartmentId, //Guid ? departmentId,
                            employee.PositionId, //Guid ? positionId,
                            employeeSalary.GradeId, //Guid ? gradeId,
                            _totalDaysInMonth, //Int16 ? totalDaysInMonth,
                            _totalWorkingDays, //Int16 ? totalWorkingDays,
                            _totalPresentDays, //Int16 ? totalPresentDays,
                            _totalAbsentDays, //Int16 ? totalAbsentDays,
                            _totalLeaveDays, //Int16 ? totalLeaveDays,
                            _weeklyOffDays, //Int16 ? weeklyOffDays,
                            _governmentOffDays, //Int16 ? governmentOffDays,
                            _totalWorkingHoliday, //Int16 ? totalWorkingHoliday,
                            _oTHour, //decimal ? oTHour,
                            _oTRate, //decimal ? oTRate,
                            _grossSalary,//decimal ? grossSalary,
                            _payableSalary, //decimal ? payableSalary,
                            _totalDeductionAmount,//decimal ? totalDeductionAmount,
                            _netPayableAmount,  //decimal ? netPayableAmount,
                            _processDate, //DateTime ? processDate,
                            _stampCost,//Int16 ? stampCost,
                            false,//Boolean isDeleted,
                            employeeSalary.Id, //Guid ? emloyeeSalaryId,
                            false, //Boolean isApproved,
                            Guid.Empty, //Guid ? approvedBy,
                            DateTime.MinValue, //DateTime ? approvedTime,
                            "" //string remarks
                            );
                }
                EmployeeSalaryProcessedDataList.Add(employeeSalaryProcessed);

                var oList = _employeeSalaryProcessedDataComponentListDB.FindAll(x => x.EmployeeSalaryProcessedDataId == employeeSalaryProcessed.Id);
                foreach (var item in oList)
                {
                    item.MarkAsDeleteEmployeeSalaryProcessedDataComponent();
                }
                EmployeeSalaryProcessedDataComponentList.AddRange(oList);
                foreach (var item in _empSalaryComponentList)
                {
                    var oModel = EmployeeSalaryProcessedDataComponentList.FirstOrDefault(x => x.EmployeeSalaryComponentId == item.Id
                    && x.EmployeeSalaryProcessedDataId == employeeSalaryProcessed.Id);
                    if (oModel == null)
                    {
                        oModel = new EmployeeSalaryProcessedDataComponent(Guid.NewGuid());
                        oModel.Create(employeeSalaryProcessed.Id,
                            item.Id, Guid.Empty,
                            item.ComponentAmount,
                            "B", item.CompanyId, false, item.DisplayName, "");
                        EmployeeSalaryProcessedDataComponentList.Add(oModel);
                    }
                    else
                    {
                        oModel.UpdateEmployeeSalaryProcessedDataComponent(employeeSalaryProcessed.Id,
                            item.Id, Guid.Empty,
                            item.ComponentAmount,
                            "B", item.CompanyId, false, item.DisplayName, "");
                        EmployeeSalaryProcessedDataComponentList.Add(oModel);
                    }
                }

                //------- Holiday Bill 
                if (totalHolidaySalary > 0)
                {
                    var oHlidayBill = EmployeeSalaryProcessedDataComponentList.FirstOrDefault(x => x.DisplayName != null
                    && x.DisplayName.ToUpper() == "HOLIDAY BILL"
                    && x.EmployeeSalaryProcessedDataId == employeeSalaryProcessed.Id);
                    if (oHlidayBill == null)
                    {
                        oHlidayBill = new EmployeeSalaryProcessedDataComponent(Guid.NewGuid());
                        oHlidayBill.Create(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            totalHolidaySalary,
                            "B", employeeSalaryProcessed.CompanyId, false,
                            "Holiday Bill", $"Holiday amount : {totalHolidaySalary.Value.ToString("0.##")}, days : {_totalWorkingHoliday}, per day amount : {perDaySalary.Value.ToString("0.##")}");
                        EmployeeSalaryProcessedDataComponentList.Add(oHlidayBill);
                    }
                    else
                    {
                        oHlidayBill.UpdateEmployeeSalaryProcessedDataComponent(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            totalHolidaySalary,
                            "B", employeeSalaryProcessed.CompanyId, false,
                            "Holiday Bill",
                            $"Holiday amount : {totalHolidaySalary.Value.ToString("0.##")}, days : {_totalWorkingHoliday}, per day amount : {perDaySalary.Value.ToString("0.##")}");
                        EmployeeSalaryProcessedDataComponentList.Add(oHlidayBill);
                    }
                }

                // OT amount--------------
                if (_oTAmount > 0)
                {
                    var oOTBill = EmployeeSalaryProcessedDataComponentList.FirstOrDefault(x => x.DisplayName != null && x.DisplayName.ToUpper() == "OT BILL"
                    && x.EmployeeSalaryProcessedDataId == employeeSalaryProcessed.Id);
                    if (oOTBill == null)
                    {
                        oOTBill = new EmployeeSalaryProcessedDataComponent(Guid.NewGuid());
                        oOTBill.Create(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            _oTAmount,
                            "B", employeeSalaryProcessed.CompanyId, false,
                            "OT Bill", $"OT amount : {_oTAmount.Value.ToString("0.##")}, time : {_oTHour}, per hour amount : {_oTRate.Value.ToString("0.##")}");
                        EmployeeSalaryProcessedDataComponentList.Add(oOTBill);
                    }
                    else
                    {
                        oOTBill.UpdateEmployeeSalaryProcessedDataComponent(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            _oTAmount,
                            "B", employeeSalaryProcessed.CompanyId, false,
                            "OT Bill",
                            $"OT amount : {_oTAmount.Value.ToString("0.##")}, time : {_oTHour}, per hour amount : {_oTRate.Value.ToString("0.##")}");
                        EmployeeSalaryProcessedDataComponentList.Add(oOTBill);
                    }
                }
                // Absent amount--------------
                if (totalAbsentDeductionAmount > 0)
                {
                    var oAbsentDeduction = EmployeeSalaryProcessedDataComponentList.FirstOrDefault(x => x.DisplayName != null &&
                x.DisplayName.ToUpper() == "ABSENT DEDUCTION" && x.EmployeeSalaryProcessedDataId == employeeSalaryProcessed.Id);
                    if (oAbsentDeduction == null)
                    {
                        oAbsentDeduction = new EmployeeSalaryProcessedDataComponent(Guid.NewGuid());
                        oAbsentDeduction.Create(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            totalAbsentDeductionAmount,
                            "D", employeeSalaryProcessed.CompanyId, false,
                            "Absent Deduction", $"Absent deduction amount : {totalAbsentDeductionAmount.Value.ToString("0.##")}, days : {_totalAbsentDays}, per day amount : {perDayAttnDeductionAmount.ToString("0.##")}");
                        EmployeeSalaryProcessedDataComponentList.Add(oAbsentDeduction);
                    }
                    else
                    {
                        oAbsentDeduction.UpdateEmployeeSalaryProcessedDataComponent(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            totalAbsentDeductionAmount,
                            "D", employeeSalaryProcessed.CompanyId, false,
                            "Absent Deduction",
                            $"Absent deduction amount : {totalAbsentDeductionAmount.Value.ToString("0.##")}, days : {_totalAbsentDays}, per day amount : {perDayAttnDeductionAmount.ToString("0.##")}");
                        EmployeeSalaryProcessedDataComponentList.Add(oAbsentDeduction);
                    }
                }

                // Late Deduction amount--------------
                if (totalLateDeductionAmount > 0)
                {
                    var oLateDeduction = EmployeeSalaryProcessedDataComponentList.FirstOrDefault(x => x.DisplayName != null &&
                x.DisplayName.ToUpper() == "LATE DEDUCTION" && x.EmployeeSalaryProcessedDataId == employeeSalaryProcessed.Id);
                    if (oLateDeduction == null)
                    {
                        oLateDeduction = new EmployeeSalaryProcessedDataComponent(Guid.NewGuid());
                        oLateDeduction.Create(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            totalLateDeductionAmount,
                            "D", employeeSalaryProcessed.CompanyId, false, "Late Deduction",
                            lateDeductionRule);
                        EmployeeSalaryProcessedDataComponentList.Add(oLateDeduction);
                    }
                    else
                    {
                        oLateDeduction.UpdateEmployeeSalaryProcessedDataComponent(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            totalLateDeductionAmount,
                            "D", employeeSalaryProcessed.CompanyId, false,
                            "Late Deduction",
                            lateDeductionRule);
                        EmployeeSalaryProcessedDataComponentList.Add(oLateDeduction);
                    }
                }
                // Join or Quit Deduction--------------
                if (JoinOrQuitDeduct > 0)
                {
                    var oJoinOrQuitDeduction = EmployeeSalaryProcessedDataComponentList.FirstOrDefault(x => x.DisplayName != null &&
                    x.DisplayName.ToUpper() == "JOIN OR QUIT DEDUCTION" && x.EmployeeSalaryProcessedDataId == employeeSalaryProcessed.Id);
                    if (oJoinOrQuitDeduction == null)
                    {
                        oJoinOrQuitDeduction = new EmployeeSalaryProcessedDataComponent(Guid.NewGuid());
                        oJoinOrQuitDeduction.Create(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            JoinOrQuitDeduct,
                            "D", employeeSalaryProcessed.CompanyId, false,
                            "Join or Quit Deduction", $"Join or quit deduction amount : {JoinOrQuitDeduct.Value.ToString("0.##")}, days : {_joinOrQuitDays}, per day amount : {perDaySalary.Value.ToString("0.##")}");
                        EmployeeSalaryProcessedDataComponentList.Add(oJoinOrQuitDeduction);
                    }
                    else
                    {
                        oJoinOrQuitDeduction.UpdateEmployeeSalaryProcessedDataComponent(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            JoinOrQuitDeduct,
                            "D", employeeSalaryProcessed.CompanyId, false,
                            "Join or Quit Deduction",
                            $"Join or quit deduction amount : {JoinOrQuitDeduct.Value.ToString("0.##")}, days : {_joinOrQuitDays}, per day amount : {perDaySalary.Value.ToString("0.##")}");
                        EmployeeSalaryProcessedDataComponentList.Add(oJoinOrQuitDeduction);
                    }
                }

                // IncomeTax deduct--------------
                if (_employeePaidIncomeTax != null && _employeePaidIncomeTax.TaxAmount > 0)
                {
                    var oTaxDeduction = EmployeeSalaryProcessedDataComponentList.FirstOrDefault(x => x.DisplayName != null &&
                x.DisplayName.ToUpper() == "INCOMETAX DEDUCTION" && x.EmployeeSalaryProcessedDataId == employeeSalaryProcessed.Id);
                    if (oTaxDeduction == null)
                    {
                        oTaxDeduction = new EmployeeSalaryProcessedDataComponent(Guid.NewGuid());
                        oTaxDeduction.Create(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            _employeePaidIncomeTax.TaxAmount,
                            "D", employeeSalaryProcessed.CompanyId, false,
                            "Incometax Deduction",
                            _employeePaidIncomeTax.Remarks);
                        EmployeeSalaryProcessedDataComponentList.Add(oTaxDeduction);
                    }
                    else
                    {
                        oTaxDeduction.UpdateEmployeeSalaryProcessedDataComponent(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            _employeePaidIncomeTax.TaxAmount,
                            "D", employeeSalaryProcessed.CompanyId, false,
                            "Incometax Deduction",
                            _employeePaidIncomeTax.Remarks);
                        EmployeeSalaryProcessedDataComponentList.Add(oTaxDeduction);
                    }
                }

                // PF(Provident Fund) deduct--------------
                if (_employeePFTransaction != null && _employeePFTransaction.EmployeeContribution > 0)
                {
                    var oTaxDeduction = EmployeeSalaryProcessedDataComponentList.FirstOrDefault(x => x.DisplayName != null &&
                x.DisplayName.ToUpper() == "PF DEDUCTION" && x.EmployeeSalaryProcessedDataId == employeeSalaryProcessed.Id);
                    if (oTaxDeduction == null)
                    {
                        oTaxDeduction = new EmployeeSalaryProcessedDataComponent(Guid.NewGuid());
                        oTaxDeduction.Create(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            _employeePFTransaction.EmployeeContribution,
                            "D", employeeSalaryProcessed.CompanyId, false,
                            "PF Deduction",
                            _employeePFTransaction.Remarks);
                        EmployeeSalaryProcessedDataComponentList.Add(oTaxDeduction);
                    }
                    else
                    {
                        oTaxDeduction.UpdateEmployeeSalaryProcessedDataComponent(employeeSalaryProcessed.Id,
                            Guid.Empty, Guid.Empty,
                            _employeePFTransaction.EmployeeContribution,
                            "D", employeeSalaryProcessed.CompanyId, false,
                            "PF Deduction",
                            _employeePFTransaction.Remarks);
                        EmployeeSalaryProcessedDataComponentList.Add(oTaxDeduction);
                    }
                }
            }

        }

        private string GetValueFromString(string _content, string _keyword, string _separator)
        {
            int start = _content.ToLower().IndexOf(_keyword.ToLower());
            start = start + (_keyword).Length + 1;
            int end = _content.IndexOf(_separator, start);
            end = end - start;
            var strValue = _content.Substring(start, end);
            return strValue.Trim();
        }
    }
}
