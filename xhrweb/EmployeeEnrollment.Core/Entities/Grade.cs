using ASL.Hrms.SharedKernel;
using System;


namespace EmployeeEnrollment.Core.Entities
{
    public class Grade : BaseEntity<Guid>
    {
        public string GradeName { get; private set; }
        public string GradeNameLocalized { get; private set; }
        public Int32? Rank { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }


        public Grade(Guid id) : base(id) { }
        private Grade() : base(Guid.NewGuid()) { }

    }
}

