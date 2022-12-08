using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.MonthCycle.Commands.Models
{
   public class MonthCycleCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
