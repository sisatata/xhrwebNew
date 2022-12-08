using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.ShiftAllocation.Commands.Models
{
  public   class ShiftAllocationCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }

    }
}
