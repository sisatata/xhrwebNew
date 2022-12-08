using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.Shift.Commands.Models
{
  public   class ShiftCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
