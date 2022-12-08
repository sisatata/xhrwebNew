using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Entities
{
   public class Holiday : BaseEntity<Guid>
    {
        public DateTime HolidayDate { get; private set; }
        public string Reason { get; private set; }
        public string ReasonLocalized { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }

        public Holiday(Guid id) : base(id) { }
        private Holiday() : base(Guid.NewGuid()) { }
    }
}
