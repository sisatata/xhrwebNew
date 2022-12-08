using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Entities
{
  public  class FinancialYear: BaseEntity<Guid>
    {

        public string Year { get; set; }
        public DateTime YearStartDate { get; set; }
        public DateTime YearEndDate { get; set; }
        public Guid CompanyId { get; set; }
        public FinancialYear(Guid id) : base(id) { }

        private FinancialYear() : base(Guid.NewGuid()) { }

    }


}
