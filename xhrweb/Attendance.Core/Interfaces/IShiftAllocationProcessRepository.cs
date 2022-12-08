using Attendance.Core.Entities.ProcessShiftAllocationAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Core.Interfaces
{
    public interface IShiftAllocationProcessRepository
    {
        Task SaveShiftAllocation(ProcessShiftAllocationAggregate processShiftAllocation);
    }
}
