using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Entities.CompanyAggregate;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ASL.Hrms.SharedKernel.ExtensionMethods;

namespace CompanySetup.Core.Entities.ProcessMonthCycleAggregate
{
    public class ProcessMonthCycleAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        private IReadOnlyList<Company> _companies { get; }
        private IReadOnlyList<FinancialYear> _financialYears { get; set; }
        private IReadOnlyList<MonthCycle> _monthCycles { get; set; }
        private IReadOnlyList<CompanyWiseConfiguration> _companyWiseConfigurationList { get; set; }
        public List<MonthCycle> MonthCycleList { get; set; }

        public ProcessMonthCycleAggregate(IReadOnlyList<Company> companies,
            IReadOnlyList<FinancialYear> financialYears,
            IReadOnlyList<MonthCycle> monthCycles,
            IReadOnlyList<CompanyWiseConfiguration> companyWiseConfigurationList)
        {
            MonthCycleList = new List<MonthCycle>();
            _companies = companies;
            _financialYears = financialYears;
            _monthCycles = monthCycles;
            _companyWiseConfigurationList = companyWiseConfigurationList;
        }

        public async void StartMonthCycleCreateProcess()
        {
            if (_companies == null || _companies.Count < 1)
                return;
            if (_financialYears == null || _financialYears.Count < 1)
                return;

            foreach (var _company in _companies)
            {
                var currentMonthNo = DateTime.Now.Month;
                int monthStartDay = 1;
                int monthEndDay = 30;
                var _compFinanYear = _financialYears.ToList().FindAll(x => x.CompanyId == _company.Id);
                var monthConfiguration = _companyWiseConfigurationList.
                    FirstOrDefault(x => x.CompanyId == _company.Id && x.Code.ToLower().Trim() == "monthstartend")?.Value;
                if (!string.IsNullOrEmpty(monthConfiguration))
                {
                    string[] values = monthConfiguration.Split(':');
                    if (values.Length == 2)
                    {
                        monthStartDay = Convert.ToInt32(values[0]);
                        monthEndDay = Convert.ToInt32(values[1]);
                    }
                }
                int q = 0;
                var maxMonthInDB = _monthCycles.FirstOrDefault(x => x.MonthEndDate == _monthCycles.Max(q => q.MonthEndDate));
                if (maxMonthInDB == null && _compFinanYear.Min(x => x.FinancialYearStartDate) < DateTime.Now.Date)
                {
                    q = 0 - (DateTime.Now.GetMonthsBetweenDate(_compFinanYear.Min(x => x.FinancialYearStartDate)));
                }
                else if (maxMonthInDB?.MonthEndDate < DateTime.Now.Date)
                    q = (maxMonthInDB.MonthStartDate.Month - DateTime.Now.Month) + 1;

                for (int i = q; i < 11; i++)
                {
                    DateTime monthStartDate = new DateTime(DateTime.Now.AddMonths(i).Year, DateTime.Now.AddMonths(i).Month, monthStartDay);
                    DateTime monthEndDate = monthEndDay == 30 ? new DateTime(DateTime.Now.AddMonths(i).Year, DateTime.Now.AddMonths(i).Month,
                                    DateTime.DaysInMonth(DateTime.Now.AddMonths(i).Year, DateTime.Now.AddMonths(i).Month)) :
                                    new DateTime(DateTime.Now.AddMonths(i + 1).Year, DateTime.Now.AddMonths(i + 1).Month,
                                    monthEndDay);
                    var financialYear = _financialYears.FirstOrDefault(x => x.CompanyId == _company.Id
                    && x.FinancialYearStartDate <= monthStartDate && x.FinancialYearEndDate >= monthStartDate
                    && x.FinancialYearStartDate <= monthEndDate && x.FinancialYearEndDate >= monthEndDate);

                    var financialId = financialYear?.Id;
                    if (financialId == null || financialId == Guid.Empty)
                        continue;
                    if (_monthCycles.FirstOrDefault(x => x.FinancialYearId == financialId
                     && x.MonthStartDate == monthStartDate && x.MonthEndDate == monthEndDate) != null)
                        continue;
                    if (monthEndDate > financialYear.FinancialYearEndDate)
                        continue;

                    var newMonthCycle = new MonthCycle(Guid.NewGuid());
                    newMonthCycle.CreateMonthCycle(_company.Id, financialId.Value, monthStartDate.ToString("MMMM"),
                        monthStartDate, monthEndDate, 22, monthStartDate.Month);
                    MonthCycleList.Add(newMonthCycle);
                }
            }


        }
    }
}
