using CompanySetup.Application.MonthCycle.Commands.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.MonthCycle.Commands
{
   public  class MonthCycleCommands
    {
        public class CreateMonthCycle : IRequest<MonthCycleCommandVM>
        {          
            public Guid CompanyId { get; set; }
            public Guid FinancialYearId { get; set; }
            public string MonthCycleName { get; set; }
            public string MonthCycleLocalizedName { get; set; }
            public DateTime MonthStartDate { get; set; }
            public DateTime MonthEndDate { get; set; }
            public double TotalWorkingDays { get; set; }
            public bool RunningFlag { get; set; }
            public bool IsSelected { get; set; }
            public int SortOrder { get; set; }
            public bool IsDeleted { get; set; }

        }

        public class UpdateMonthCycle : IRequest<MonthCycleCommandVM>
        {
            public Guid Id { get; set; }
            public Guid CompanyId { get; set; }
            public Guid FinancialYearId { get; set; }
            public string MonthCycleName { get; set; }
            public string MonthCycleLocalizedName { get; set; }
            public DateTime MonthStartDate { get; set; }
            public DateTime MonthEndDate { get; set; }
            public double TotalWorkingDays { get; set; }
            public bool RunningFlag { get; set; }
            public bool IsSelected { get; set; }
            public int SortOrder { get; set; }
            public bool IsDeleted { get; set; }

        }

        public class MarkMonthCycleAsDelete : IRequest<MonthCycleCommandVM>
        {
            public Guid Id { get; set; }
        }

        public class ProcessMonthCycleCreate : IRequest<MonthCycleCommandVM>
        {
            public Guid? CompanyId { get; set; }
            public DateTime ProcessDate { get; set; }
        }
    }
}
