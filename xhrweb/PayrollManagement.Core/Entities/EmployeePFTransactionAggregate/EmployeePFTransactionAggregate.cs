using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayrollManagement.Core.Entities.EmployeePFTransactionAggregate
{
    public class EmployeePFTransactionAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        private List<EmployeePFTransaction> _employeePFTransactionListDB { get; set; }
        private List<FinancialYearMonth> _financialYearMonthList { get; set; }
        private AttendanceCalendar _pFYear { get; set; }
        private MonthCycle _pFMonth { get; set; }
        private List<EmployeeCustomConfiguration> _employeeCustomConfiguration { get; set; }
        public EmployeePFTransactionAggregate(List<EmployeePFTransaction> employeePFTransactionListDB,
            List<FinancialYearMonth> financialYearMonthList,
            AttendanceCalendar pFYear,
            MonthCycle pFMonth,
            List<EmployeeCustomConfiguration> employeeCustomConfiguration
            )
        {
            _employeePFTransactionListDB = employeePFTransactionListDB;
            _financialYearMonthList = financialYearMonthList;

            _pFYear = pFYear;
            _pFMonth = pFMonth;
            _employeeCustomConfiguration = employeeCustomConfiguration;

        }

        public EmployeePFTransaction CalculateProvidentFund(Employee employee, decimal basic, decimal gross, DateTime processDate)
        {
            DateTime? _transactionDate = processDate;
            decimal? _companyContribution = (decimal?)null;
            decimal? _employeeContribution = (decimal?)null;
            decimal? _employeeInterestRate = (decimal?)null;
            decimal? _companyInterestRate = (decimal?)null;
            decimal? _interestOnEmployeeContribution = (decimal?)null;
            decimal? _interestOnCompanyContribution = (decimal?)null;
            decimal? _totalContribution = (decimal?)null;
            decimal? _totalInterest = (decimal?)null;
            decimal? _employeeCumulativeBalance = (decimal?)null;
            decimal? _companyCumulativeBalance = (decimal?)null;
            decimal? _totalCumulativeBalance = (decimal?)null;
            string _remarks = "";

            decimal _pfServiceLength = decimal.Zero;
            decimal _pfContribution = decimal.Zero;

            var oPFServiceLength = _employeeCustomConfiguration.FirstOrDefault(x => x.Code.ToLower().Trim() == "pfservicelength" && x.EmployeeId == employee.Id);
            var oPFCalculateOn = _employeeCustomConfiguration.FirstOrDefault(x => x.Code.ToLower().Trim() == "pfcalculateon" && x.EmployeeId == employee.Id);
            var oPFContribution = _employeeCustomConfiguration.FirstOrDefault(x => x.Code.ToLower().Trim() == "pfcontribution" && x.EmployeeId == employee.Id);
            var oPFInterestRate = _employeeCustomConfiguration.FirstOrDefault(x => x.Code.ToLower().Trim() == "pfinterestrate" && x.EmployeeId == employee.Id);

            var lastFinancialMonth = _financialYearMonthList.FirstOrDefault(x => x.MonthNumber == _financialYearMonthList.FirstOrDefault(r => r.Id == _pFMonth.Id).MonthNumber + 1);
            var lastMonthPFTransaction = _employeePFTransactionListDB.FirstOrDefault(x => x.PFMonthId == _pFMonth.Id);


            if (oPFServiceLength != null && !string.IsNullOrEmpty(oPFServiceLength.Value))
            {
                _pfServiceLength = Convert.ToDecimal(oPFServiceLength.Value.Trim());

                if (DateTimeExtension.GetMonthsBetweenDate(employee.JoiningDate, processDate) >= _pfServiceLength)
                {
                    if (oPFContribution != null && !string.IsNullOrEmpty(oPFContribution.Value))
                    {
                        _pfContribution = Convert.ToDecimal(oPFContribution.Value.Trim());
                    }
                    if (oPFInterestRate != null && !string.IsNullOrEmpty(oPFInterestRate.Value))
                    {
                        _employeeInterestRate = Convert.ToDecimal(oPFInterestRate.Value.Trim());
                        _companyInterestRate = Convert.ToDecimal(oPFInterestRate.Value.Trim());
                    }

                    if (oPFCalculateOn.Value.Trim().ToLower() == "basic")
                    {
                        _companyContribution = basic * (_pfContribution / 100);
                        _employeeContribution = basic * (_pfContribution / 100);
                    }
                    else if (oPFCalculateOn.Value.Trim().ToLower() == "gross")
                    {
                        _companyContribution = gross * (_pfContribution / 100);
                        _employeeContribution = gross * (_pfContribution / 100);
                    }

                    _totalContribution = _companyContribution + _employeeContribution;
                    if (lastMonthPFTransaction != null)
                    {
                        _employeeCumulativeBalance += lastMonthPFTransaction.EmployeeCumulativeBalance;
                        _companyCumulativeBalance += lastMonthPFTransaction.CompanyCumulativeBalance;
                    }
                    else
                    {
                        _employeeCumulativeBalance = _employeeContribution;
                        _companyCumulativeBalance = _companyContribution;
                    }

                    _totalCumulativeBalance = _employeeCumulativeBalance + _companyCumulativeBalance;
                }
                else
                    return null;

            }
            else
                return null;

            _remarks = $"Employee Contribution is BDT {_employeeContribution.Value.ToString("0.##")} and Company Contribution is BDT {_companyContribution.Value.ToString("0.##")}";
            var _employeePFTransaction = _employeePFTransactionListDB.ToList().FirstOrDefault(x => x.EmployeeId == employee.Id
                && x.PFYearId.Value == _pFYear.Id && x.PFMonthId == _pFMonth.Id);

            if (_employeePFTransaction == null)
            {
                _employeePFTransaction = new EmployeePFTransaction(Guid.NewGuid());
                _employeePFTransaction.Create(
                                        employee.Id,  //Guid ? employeeId,
                                        employee.PositionId, //Guid ? emlpoyeeDesignationId,
                                        employee.DepartmentId, //Guid ? employeeDepartmentId,
                                        _pFYear.Id, //Guid ? pFYearId,
                                        _pFMonth.Id, //Guid ? pFMonthId,
                                        _transactionDate, //DateTime ? transactionDate,
                                        _companyContribution,   //decimal ? companyContribution,
                                        _employeeContribution, //decimal ? employeeContribution,
                                        _employeeInterestRate, //decimal ? employeeInterestRate,
                                        _companyInterestRate, //decimal ? companyInterestRate,
                                        _interestOnEmployeeContribution, //decimal ? interestOnEmployeeContribution,
                                        _interestOnCompanyContribution,  //decimal ? interestOnCompanyContribution,
                                        _totalContribution, //decimal ? totalContribution,
                                        _totalInterest,  //decimal ? totalInterest,
                                        _employeeCumulativeBalance, //decimal ? employeeCumulativeBalance,
                                        _companyCumulativeBalance,//decimal ? companyCumulativeBalance,
                                        _totalCumulativeBalance,//decimal ? totalCumulativeBalance,
                                        _remarks,//string remarks,
                                        employee.CompanyId
                    );
            }
            else
            {
                if (_employeeContribution > 0)
                {
                    _employeePFTransaction.UpdateEmployeePFTransaction(
                                            employee.Id,  //Guid ? employeeId,
                                            Guid.NewGuid(), //Guid ? emlpoyeeDesignationId,
                                            Guid.NewGuid(), //Guid ? employeeDepartmentId,
                                            _pFYear.Id, //Guid ? pFYearId,
                                            _pFMonth.Id, //Guid ? pFMonthId,
                                            _transactionDate, //DateTime ? transactionDate,
                                            _companyContribution,   //decimal ? companyContribution,
                                            _employeeContribution, //decimal ? employeeContribution,
                                            _employeeInterestRate, //decimal ? employeeInterestRate,
                                            _companyInterestRate, //decimal ? companyInterestRate,
                                            _interestOnEmployeeContribution, //decimal ? interestOnEmployeeContribution,
                                            _interestOnCompanyContribution,  //decimal ? interestOnCompanyContribution,
                                            _totalContribution, //decimal ? totalContribution,
                                            _totalInterest,  //decimal ? totalInterest,
                                            _employeeCumulativeBalance, //decimal ? employeeCumulativeBalance,
                                            _companyCumulativeBalance,//decimal ? companyCumulativeBalance,
                                            _totalCumulativeBalance,//decimal ? totalCumulativeBalance,
                                            _remarks,//string remarks
                                            employee.CompanyId
                                       );
                }
                else
                {
                    _employeePFTransaction.MarkAsDeleteEmployeePFTransaction();
                }
            }
            return _employeePFTransaction;
        }
    }
}
