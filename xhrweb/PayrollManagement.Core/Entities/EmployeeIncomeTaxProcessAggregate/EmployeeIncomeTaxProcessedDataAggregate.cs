using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using PayrollManagement.Core.Interfaces;
using System.Linq;

namespace PayrollManagement.Core.Entities.EmployeeIncomeTaxProcessAggregate
{
    public class EmployeeIncomeTaxProcessedDataAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        private List<IncomeTaxSlab> _incomeTaxSlabList;
        private List<IncomeTaxPayerType> _incomeTaxPayerTypeList;
        private List<IncomeTaxParameter> _incomeTaxParameterList;
        private List<IncomeTaxInvestment> _incomeTaxInvestmentList;
        private List<EmployeePaidIncomeTax> _employeePaidIncomeTaxListDB { get; set; }
        public EmployeePaidIncomeTax EmployeePaidIncomeTax { get; set; }

        public AttendanceCalendar AttendanceCalendar { get; private set; }
        public MonthCycle _monthCycle { get; private set; }
        public EmployeeIncomeTaxProcessedDataAggregate(List<IncomeTaxSlab> incomeTaxSlabList,
            List<IncomeTaxPayerType> incomeTaxPayerTypeList,
            List<IncomeTaxParameter> incomeTaxParameterList,
            List<IncomeTaxInvestment> incomeTaxInvestmentList,
            List<EmployeePaidIncomeTax> employeePaidIncomeTaxListDB,
            AttendanceCalendar attendanceCalendar,
            MonthCycle monthCycle)
        {
            _incomeTaxSlabList = incomeTaxSlabList;
            _incomeTaxPayerTypeList = incomeTaxPayerTypeList;
            _incomeTaxParameterList = incomeTaxParameterList;
            _incomeTaxInvestmentList = incomeTaxInvestmentList;
            _employeePaidIncomeTaxListDB = employeePaidIncomeTaxListDB;

            AttendanceCalendar = attendanceCalendar;
            _monthCycle = monthCycle;
        }

        public EmployeePaidIncomeTax CalculateIncomeTax(Guid employeeId, decimal basic, decimal houseRent, decimal medicalAllowance, decimal conveyance,
            decimal festivalBonus, decimal CCToProvidentFund, int taxPayerTypeId, DateTime processDate)
        {
            if (!_incomeTaxParameterList.Any())
                return null;

            decimal _monthlyTaxAmount = 0;
            decimal _totalTaxableIncome = 0;
            decimal _minimumTaxAmount = 5000;
            decimal _houseRentThreshold = 0;
            decimal _medicalThreshold = 0;
            decimal _conveyanceThreshold = 0;
            string _remarks = "";
            //Basic 
            _totalTaxableIncome = _totalTaxableIncome + (basic * 12);

            //House Rent

            decimal _houseRentLimitAmount = 0;
            decimal _houseRentLimitPercentageOfBasic = 0;
            var _houseRentParameter = _incomeTaxParameterList.FirstOrDefault(x => x.ParameterName.ToLower() == "houserent" && x.StartDate <= processDate && x.EndDate >= processDate);

            if (_houseRentParameter.LimitAmount > 0 && _houseRentParameter.LimitPercentageOfBasic > 0)
            {
                if (basic * (_houseRentParameter.LimitPercentageOfBasic / 100) < _houseRentParameter.LimitAmount)
                {
                    _houseRentThreshold = basic * (_houseRentParameter.LimitPercentageOfBasic.Value / 100);
                }
                else
                {
                    _houseRentThreshold = _houseRentParameter.LimitAmount.Value;
                }
            }
            else if (_houseRentParameter.LimitAmount > 0)
            {
                _houseRentThreshold = _houseRentParameter.LimitAmount.Value;

            }
            else if (_houseRentParameter.LimitPercentageOfBasic > 0)
            {
                _houseRentThreshold = basic * (_houseRentParameter.LimitPercentageOfBasic.Value / 100);
            }

            if (_houseRentThreshold > 0 && _houseRentThreshold < houseRent)
            {
                _totalTaxableIncome = _totalTaxableIncome + ((houseRent - _houseRentThreshold) * 12);
            }

            //Medical

            decimal _medicalLimitAmount = 0;
            decimal _medicalLimitPercentageOfBasic = 0;
            var _medicalParameter = _incomeTaxParameterList.FirstOrDefault(x => x.ParameterName.ToLower() == "medical" && x.StartDate <= processDate && x.EndDate >= processDate);

            if (_medicalParameter.LimitAmount > 0 && _medicalParameter.LimitPercentageOfBasic > 0)
            {
                if (basic * (_medicalParameter.LimitPercentageOfBasic / 100) < _medicalParameter.LimitAmount)
                {
                    _medicalThreshold = basic * (_medicalParameter.LimitPercentageOfBasic.Value / 100);
                }
                else
                {
                    _medicalThreshold = _medicalParameter.LimitAmount.Value;
                }
            }
            else if (_medicalParameter.LimitAmount > 0)
            {
                _medicalThreshold = _medicalParameter.LimitAmount.Value;

            }
            else if (_medicalParameter.LimitPercentageOfBasic > 0)
            {
                _medicalThreshold = basic * (_medicalParameter.LimitPercentageOfBasic.Value / 100);
            }

            if (_medicalThreshold > 0 && _medicalThreshold < medicalAllowance)
            {
                _totalTaxableIncome = _totalTaxableIncome + ((medicalAllowance - _medicalThreshold) * 12);
            }


            //Conveyance

            decimal _conveyanceLimitAmount = 0;
            decimal _conveyanceLimitPercentageOfBasic = 0;
            var _conveyanceParameter = _incomeTaxParameterList.FirstOrDefault(x => x.ParameterName.ToLower() == "conveyance" && x.StartDate <= processDate && x.EndDate >= processDate);

            if (_conveyanceParameter.LimitAmount > 0 && _conveyanceParameter.LimitPercentageOfBasic > 0)
            {
                if (basic * (_conveyanceParameter.LimitPercentageOfBasic / 100) < _conveyanceParameter.LimitAmount)
                {
                    _conveyanceThreshold = basic * (_conveyanceParameter.LimitPercentageOfBasic.Value / 100);
                }
                else
                {
                    _conveyanceThreshold = _conveyanceParameter.LimitAmount.Value;
                }
            }
            else if (_conveyanceParameter.LimitAmount > 0)
            {
                _conveyanceThreshold = _conveyanceParameter.LimitAmount.Value;

            }
            else if (_conveyanceParameter.LimitPercentageOfBasic > 0)
            {
                _conveyanceThreshold = basic * (_conveyanceParameter.LimitPercentageOfBasic.Value / 100);
            }

            if (_conveyanceThreshold > 0 && _conveyanceThreshold < conveyance)
            {
                _totalTaxableIncome = _totalTaxableIncome + ((conveyance - _conveyanceThreshold) * 12);
            }

            // Festival Bonus
            _totalTaxableIncome = _totalTaxableIncome + (festivalBonus * 2);

            /*Employers Contribution To Provident Fund */
            _totalTaxableIncome = _totalTaxableIncome + (CCToProvidentFund * 12);

            /* Investment consideration  */
            decimal _investmentAmount = 0;
            decimal _eligibleForInvestmentAmount = 0;
            decimal _waiverAmount = 0;

            var _currentInvestmentSetting = _incomeTaxInvestmentList.FirstOrDefault(x => x.StartDate <= processDate && x.EndDate >= processDate);

            if (_currentInvestmentSetting != null)
            {
                _eligibleForInvestmentAmount = (_currentInvestmentSetting.InvestmentPercentage.Value / 100) * _totalTaxableIncome;
                _waiverAmount = _eligibleForInvestmentAmount * (_currentInvestmentSetting.WaiverPercentage.Value / 100);
            }

            /* Tax Amount Calculation */
            decimal _taxLiability = 0;
            decimal _yearlyTaxAmount = 0;
            //decimal _monthlyTaxAmount = 0;
            decimal _totalTaxableIncomeForCalculation = _totalTaxableIncome;
            decimal _taxableAmount = 0;

            var _currentTaxSlabs = _incomeTaxSlabList.FindAll(x => x.StartDate <= processDate && x.EndDate >= processDate);
            for (int i = 1; i <= _currentTaxSlabs.Count; i++)
            {
                decimal _limit = 0;
                decimal _percentage = 0;
                var _taxSlab = _currentTaxSlabs.FirstOrDefault(x => x.SlabOrder.Value == i);
                if (_taxSlab != null)
                {
                    _limit = _taxSlab.SlabAmount.Value;
                    _percentage = _taxSlab.TaxablePercent.Value;
                    if (_totalTaxableIncomeForCalculation > _limit)
                    {
                        _taxableAmount = _limit;
                    }
                    else
                    {
                        _taxableAmount = _totalTaxableIncomeForCalculation;
                    }
                    _taxLiability = _taxLiability + (_taxableAmount * _percentage / 100);
                    _totalTaxableIncomeForCalculation = _totalTaxableIncomeForCalculation - _taxableAmount;
                }
            }

            if (_taxLiability > 0)
            {
                _yearlyTaxAmount = _taxLiability - _waiverAmount;
            }

            if (_yearlyTaxAmount >0 && _yearlyTaxAmount < _minimumTaxAmount)
            {
                _monthlyTaxAmount = _minimumTaxAmount / 12;

            }
            else
            {
                _monthlyTaxAmount = _yearlyTaxAmount / 12;
            }
            _remarks = $"Taxable monthly income is BDT {_monthlyTaxAmount.ToString("0.##")}";
            var _employeePaidIncomeTax = _employeePaidIncomeTaxListDB.FirstOrDefault(x => x.EmployeeId == employeeId
                && x.FinancialYearId == AttendanceCalendar.Id && x.MonthCycleId == _monthCycle.Id);
            if (_employeePaidIncomeTax == null)
            {
                _employeePaidIncomeTax = new EmployeePaidIncomeTax(Guid.NewGuid());
                _employeePaidIncomeTax.Create(
                                employeeId,      //Guid ? employeeId,
                                AttendanceCalendar.Year,  //string financialYear,
                                _monthCycle.MonthCycleName, //string monthName,
                                AttendanceCalendar.Id, //Guid ? financialYearId,
                                _monthCycle.Id, //Guid ? monthCycleId,
                                basic,  //decimal ? basic,
                                houseRent, //decimal ? houseRent,
                                medicalAllowance, //decimal ? medical,
                                conveyance, //decimal ? conveyance,
                                festivalBonus, //decimal ? festivalBonus,
                                _monthlyTaxAmount,   //decimal ? taxAmount,
                                processDate, //DateTime processingDate,
                                _remarks, //string remarks,
                                AttendanceCalendar.CompanyId//Guid ? companyId
                    );
            }
            else
            {
                _employeePaidIncomeTax.UpdateEmployeePaidIncomeTax(
                                               employeeId,      //Guid ? employeeId,
                                               AttendanceCalendar.Year,  //string financialYear,
                                               _monthCycle.MonthCycleName, //string monthName,
                                               AttendanceCalendar.Id, //Guid ? financialYearId,
                                               _monthCycle.Id, //Guid ? monthCycleId,
                                               basic,  //decimal ? basic,
                                               houseRent, //decimal ? houseRent,
                                               medicalAllowance, //decimal ? medical,
                                               conveyance, //decimal ? conveyance,
                                               festivalBonus, //decimal ? festivalBonus,
                                               _monthlyTaxAmount,   //decimal ? taxAmount,
                                               processDate, //DateTime processingDate,
                                               _remarks, //string remarks,
                                               AttendanceCalendar.CompanyId//Guid ? companyId
                                   );
            }
            return _employeePaidIncomeTax;

        }
    }
}
