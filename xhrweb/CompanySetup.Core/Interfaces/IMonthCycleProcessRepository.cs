using CompanySetup.Core.Entities.ProcessMonthCycleAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanySetup.Core.Interfaces
{
    public interface IMonthCycleProcessRepository
    {
        Task SaveMonthCycle(ProcessMonthCycleAggregate processMonthCycle);
    }
}
