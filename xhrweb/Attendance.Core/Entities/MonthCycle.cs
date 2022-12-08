using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Entities
{
   public  class MonthCycle: BaseEntity<Guid>
    {
        public string MonthName { get; set; }
        public DateTime MonthStartDate { get; set; }
        public DateTime MonthEndDate { get; set; }
        public Guid CompanyId { get; set; }
        public Guid FinancialYearId { set; get; }

        public MonthCycle(Guid id) : base(id) { }

        private MonthCycle() : base(Guid.NewGuid()) { }
    }
}
