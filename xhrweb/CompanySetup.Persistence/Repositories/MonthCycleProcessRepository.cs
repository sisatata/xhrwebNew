using ASL.Hrms.SharedKernel.Enums;
using CompanySetup.Core.Entities.ProcessMonthCycleAggregate;
using CompanySetup.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanySetup.Persistence
{
    public class MonthCycleProcessRepository : IMonthCycleProcessRepository
    {
        private readonly CompanyContext _companyContext;

        public MonthCycleProcessRepository(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }
        public async Task SaveMonthCycle(ProcessMonthCycleAggregate processMonthCycle)
        {
            foreach (var Item in processMonthCycle.MonthCycleList)
            {
                if (Item.State == TrackingState.Added)
                {
                    _companyContext.Entry(Item).State = EntityState.Added;
                }
                if (Item.State == TrackingState.Modified)
                {
                    _companyContext.Entry(Item).State = EntityState.Modified;
                }
                if (Item.State == TrackingState.Deleted)
                {
                    _companyContext.Entry(Item).State = EntityState.Deleted;
                }
            }
            await _companyContext.SaveChangesAsync();
        }
    }
}
