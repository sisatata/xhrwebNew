using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class Upazila : BaseEntity<Guid>
    {
        public Guid DistrictId { get; private set; }
        public string UpazilaName { get; private set; }
        public string UpazilaNameLocalized { get; private set; }
        public bool IsDeleted { get; private set; }

        public Upazila(Guid id) : base(id) { }
        private Upazila() : base(Guid.NewGuid()) { }


    }
}

