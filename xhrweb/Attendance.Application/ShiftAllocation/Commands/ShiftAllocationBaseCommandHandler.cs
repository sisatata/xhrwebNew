using Attendance.Core.Entities;
using Attendance.Core.Interfaces;
//using Attendance.Persistence.AttendanceContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using Attendance.Persistence;

namespace Attendance.Application.ShiftAllocation.Commands
{
   public class ShiftAllocationBaseCommandHandler
    {

        //private readonly ShiftAllocation _dataContext;

        //public ShiftAllocationBaseCommandHandler(ShiftAllocation dataContext)
        //{
        //    _dataContext = dataContext;
        //}
        //public ShiftAllocationBaseCommandHandler(IAsyncRepository<Core.Entities.ShiftAllocation, Guid> repository)
        //{
        //    _repository = repository;

        //}
        //from a in _repository.ListAsync()
        //where a.IsDeleted == false && a.EmployeeId = model.EmployeeId && a.CompanyId == model.CompanyId && a.BranchId == model.BranchId && a.DutyYear = model.DutyYear && a.DutyMonth == model.DutyMonth

        // select a
       // private readonly IAsyncRepository<Core.Entities.ShiftAllocation, Guid> _repository;

        //public async Task ValidateShiftAllocation(Core.Entities.ShiftAllocation model)
        //{
           
        //    var existingAllocatedShiftForTheEmployee = await  _repository.ListAsync(x=)

        //    if (existingAllocatedShiftForTheEmployee != null)
        //    {
        //        throw new InvalidOperationException("The Shift Allocation has been already assigned for this employee.");
        //    }
        //}
    }
}
