using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using PayrollManagement.Core.Entities.BonusConfigurationAggregate;
using PayrollManagement.Core.Entities.EmployeeIncomeTaxProcessAggregate;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ASL.Hrms.SharedKernel.Enums;

namespace PayrollManagement.Core.Entities.EmployeeBonusProcessAggregate
{
    public class EmployeeBonusProcessAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        private List<EmployeeSalary> _employeeSalaryList;
        private List<EmployeeSalaryComponentVM> _employeeSalaryComponentList;
        private EmployeeIncomeTaxProcessedDataAggregate _employeeIncomeTaxProcessedDataAggregate { get; }
        public List<EmployeePaidIncomeTax> EmployeePaidIncomeTaxList { get; set; }
        private List<EmployeeCustomConfiguration> _employeeCustomConfiguration { get; set; }
        private IReadOnlyList<Employee> _employees { get; }
        private IReadOnlyList<BonusConfiguration> _bonusConfigurationList { get; set; }
        private DateTime _processDate { get; set; }
        private Guid _bonusTypeId { get; set; }
        public List<EmployeeBonusProcessedData> EmployeeBonusProcessedDataList { get; set; }
        private List<EmployeeBonusProcessedData> _employeeBonusProcessedDataDB { get; set; }
        private List<SalaryStructureComponent> _salaryStructureComponents;
        private AttendanceCalendar _attendanceCalendar { get; set; }
        public EmployeeBonusProcessAggregate(List<EmployeeSalary> employeeSalaryList,
            List<EmployeeSalaryComponentVM> employeeSalaryComponentList,
            IReadOnlyList<Employee> employees,
            IReadOnlyList<BonusConfiguration> bonusConfigurations,
            List<EmployeeCustomConfiguration> employeeCustomConfiguration,
            //EmployeeIncomeTaxProcessedDataAggregate employeeIncomeTaxProcessedDataAggregate,
            List<SalaryStructureComponent> salaryStructureComponents,
            List<EmployeeBonusProcessedData> employeeBonusProcessedDataDB,
            AttendanceCalendar attendanceCalendar,
            DateTime processDate,
            Guid bonusTypeId
            )
        {
            _employeeSalaryList = employeeSalaryList;
            _employeeSalaryComponentList = employeeSalaryComponentList;
            _employees = employees;
            _bonusConfigurationList = bonusConfigurations;
            _employeeCustomConfiguration = employeeCustomConfiguration;
            //_employeeIncomeTaxProcessedDataAggregate = employeeIncomeTaxProcessedDataAggregate;
            EmployeeBonusProcessedDataList = new List<EmployeeBonusProcessedData>();
            _processDate = processDate;
            _salaryStructureComponents = salaryStructureComponents;
            _employeeBonusProcessedDataDB = employeeBonusProcessedDataDB;
            _attendanceCalendar = attendanceCalendar;
            _bonusTypeId = bonusTypeId;
        }

        async public void StartBonusProcess()
        {
            foreach (var _employee in _employees)
            {
                decimal _basicOrGrossAmount = decimal.Zero;
                decimal _payableBasic = 0;
                decimal _payableHouseRent = 0;
                decimal _payableMedicalAllowance = 0;
                decimal _payableConveyance = 0;

                decimal _payableBonus = decimal.Zero;
                var _empServiceLength = _processDate.GetMonthsBetweenDate(_employee.JoiningDate);
                var _bonusConfiguration = _bonusConfigurationList.FirstOrDefault(x => x.RangeFromInMonth <= _empServiceLength && x.RangeToInMonth >= _empServiceLength);
                if (_bonusConfiguration == null)
                {
                    var employeeBonusProcessedDelete = _employeeBonusProcessedDataDB.FirstOrDefault(x => x.EmployeeId == _employee.Id
                && x.FinancialYearId == _attendanceCalendar.Id && x.BonusTypeId == _bonusTypeId);
                    if (employeeBonusProcessedDelete != null)
                    {
                        employeeBonusProcessedDelete.MarkAsDeleteEmployeeBonusProcessedData();
                        EmployeeBonusProcessedDataList.Add(employeeBonusProcessedDelete);
                    }
                    continue;
                }

                var employeeSalary = _employeeSalaryList.FirstOrDefault(es => es.EmployeeId == _employee.Id);
                var _empSalaryComponentList = _employeeSalaryComponentList.FindAll(x => x.EmployeeSalaryId == employeeSalary.Id);

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
                                         where sc.SalaryComponentName.ToUpper() == "HOUSERENT"
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

                if (_bonusConfiguration.BasicOrGross == BasicOrGross.Basic)
                {
                    _basicOrGrossAmount = _payableBasic;
                }
                else
                {
                    _basicOrGrossAmount = employeeSalary.GrossSalary.Value;
                }

                if (_bonusConfiguration.FixedAmount != null && _bonusConfiguration.FixedAmount > 0)
                {
                    if (_bonusConfiguration.IsPaidFull)
                    {
                        _payableBonus = Convert.ToDecimal(_bonusConfiguration.FixedAmount);
                    }
                    else
                    {

                    }
                }
                else if (_bonusConfiguration.PercentOfBasicOrGross != null && _bonusConfiguration.PercentOfBasicOrGross > 0)
                {
                    if (_bonusConfiguration.IsPaidFull)
                    {
                        _payableBonus = (_basicOrGrossAmount * _bonusConfiguration.PercentOfBasicOrGross.Value) / 100;
                    }
                    else if (_bonusConfiguration.PartialOn == PartialOn.DayWise)
                    {
                        var _totalDays = (_bonusConfiguration.RangeToInMonth.Value * 30) + 29;
                        var _empServiceLengthinDays = (_processDate - _employee.JoiningDate).TotalDays;
                        _payableBonus = (((_basicOrGrossAmount * _bonusConfiguration.PercentOfBasicOrGross.Value) / 100) / _totalDays) *
                            (decimal)_empServiceLengthinDays;
                    }
                    else if (_bonusConfiguration.PartialOn == PartialOn.MonthWise)
                    {
                        _payableBonus = (((_basicOrGrossAmount * _bonusConfiguration.PercentOfBasicOrGross.Value) / 100) / _bonusConfiguration.RangeToInMonth.Value) *
                            (decimal)_empServiceLength;
                    }
                }

                var employeeBonusProcessed = _employeeBonusProcessedDataDB.FirstOrDefault(x => x.EmployeeId == _employee.Id
                && x.FinancialYearId == _attendanceCalendar.Id && x.BonusTypeId == _bonusTypeId);


                if (employeeBonusProcessed == null)
                {
                    employeeBonusProcessed = new EmployeeBonusProcessedData(Guid.NewGuid());
                    employeeBonusProcessed.Create(_employee.Id,//  Guid ? employeeId,
                                                    _bonusTypeId,// Guid ? bonusTypeId,
                                                    _processDate,// DateTime ? bonusDate,
                                                    _attendanceCalendar.Id,// Guid ? financialYearId,
                                                    employeeSalary.PaymentOptionId,// Int16 ? paymentOptionId,
                                                    _employee.CompanyId,// Guid ? companyId,
                                                    _employee.BranchId,// Guid ? branchId,
                                                    _employee.DepartmentId,// Guid ? departmentId,
                                                    _employee.PositionId,//  Guid ? positionId,
                                                    _employee.JoiningDate,//  DateTime ? joiningDate,
                                                    employeeSalary.GradeId,// Guid ? gradeId,
                                                    Guid.Empty,//  Guid ? religionId,
                                                    _payableBasic,// decimal ? basic,
                                                    _payableHouseRent,// decimal ? houseRent,
                                                    _payableMedicalAllowance,//  decimal ? medical,
                                                    _payableConveyance,// decimal ? conveyance,
                                                    decimal.Zero,// decimal ? food,
                                                    employeeSalary.GrossSalary.Value,//  decimal ? grossSalary,
                                                    _payableBonus,// decimal ? payableBonus,
                                                    0,// decimal ? taxDeductedAmount,
                                                    _payableBonus,//  decimal ? netPayableBonus,
                                                    _payableBasic,// decimal ? basic_V2,
                                                    _payableHouseRent,// decimal ? houseRent_V2,
                                                    employeeSalary.GrossSalary.Value,// decimal ? grossSalary_V2,
                                                    _payableBonus,// decimal ? payableBonus_V2,
                                                    0,//  decimal ? taxDeductedAmount_V2,
                                                    _payableBonus,//  decimal ? netPayableBonus_V2,
                                                    $"",//  string remarks,
                                                    _bonusConfiguration.Id// Guid ? bonusConfigurationId
         );
                }
                else
                {
                    employeeBonusProcessed.UpdateEmployeeBonusProcessedData(_employee.Id,//  Guid ? employeeId,
                                                    _bonusTypeId,// Guid ? bonusTypeId,
                                                    _processDate,// DateTime ? bonusDate,
                                                    _attendanceCalendar.Id,// Guid ? financialYearId,
                                                    employeeSalary.PaymentOptionId,// Int16 ? paymentOptionId,
                                                    _employee.CompanyId,// Guid ? companyId,
                                                    _employee.BranchId,// Guid ? branchId,
                                                    _employee.DepartmentId,// Guid ? departmentId,
                                                    _employee.PositionId,//  Guid ? positionId,
                                                    _employee.JoiningDate,//  DateTime ? joiningDate,
                                                    employeeSalary.GradeId,// Guid ? gradeId,
                                                    Guid.Empty,//  Guid ? religionId,
                                                    _payableBasic,// decimal ? basic,
                                                    _payableHouseRent,// decimal ? houseRent,
                                                    _payableMedicalAllowance,//  decimal ? medical,
                                                    _payableConveyance,// decimal ? conveyance,
                                                    decimal.Zero,// decimal ? food,
                                                    employeeSalary.GrossSalary.Value,//  decimal ? grossSalary,
                                                    _payableBonus,// decimal ? payableBonus,
                                                    0,// decimal ? taxDeductedAmount,
                                                    _payableBonus,//  decimal ? netPayableBonus,
                                                    _payableBasic,// decimal ? basic_V2,
                                                    _payableHouseRent,// decimal ? houseRent_V2,
                                                    employeeSalary.GrossSalary.Value,// decimal ? grossSalary_V2,
                                                    _payableBonus,// decimal ? payableBonus_V2,
                                                    0,//  decimal ? taxDeductedAmount_V2,
                                                    _payableBonus,//  decimal ? netPayableBonus_V2,
                                                    $"",//  string remarks,
                                                    _bonusConfiguration.Id// Guid ? bonusConfigurationId
                            );
                }
                EmployeeBonusProcessedDataList.Add(employeeBonusProcessed);
            }
        }
    }
}
