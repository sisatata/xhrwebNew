using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Core.Entities
{
    public class ShiftGroup : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public string ShiftGroupName { get; private set; }
        public string ShiftGroupNameLC { get; private set; }
        public Guid CompanyId { get; private set; }
        public bool IsDeleted { get; private set; }
        public int RotationDay { get; private set; }

        public int WeekendTypeId { get; private set; }
        public string WeekendNames { get; private set; }

        public ShiftGroup(Guid id) : base(id) { }
        private ShiftGroup() : base(Guid.NewGuid()) { }



        public static ShiftGroup Create(Guid companyId, string shiftGroupName,string shiftGroupNameLC)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrWhiteSpace(shiftGroupName, "ShiftGroupName");

            var ShiftGroup = new ShiftGroup(Guid.NewGuid())
            {
                CompanyId = companyId,
                ShiftGroupName = shiftGroupName,
                ShiftGroupNameLC = shiftGroupNameLC
            };

            return ShiftGroup;
        }

        public void UpdateShiftGroup(Guid companyId, string shiftGroupName, string shiftGroupNameLC)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrWhiteSpace(shiftGroupName, "shiftGroupName");

            CompanyId = companyId;
            ShiftGroupName = shiftGroupName;
            ShiftGroupNameLC = shiftGroupNameLC;
        }

        public void MarkShiftGroupAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
