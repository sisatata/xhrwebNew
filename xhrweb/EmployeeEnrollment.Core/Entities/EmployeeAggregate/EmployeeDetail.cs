using EmployeeEnrollment.Core.Interfaces;
using System;
using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using ASL.Hrms.SharedKernel.Interfaces;

namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeDetail : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? EmployeeId { get; private set; }
        public string FathersName { get; private set; }
        public string MothersName { get; private set; }
        public string SpouseName { get; private set; }
        public Guid? MaritalStatusId { get; private set; }
        public Guid? ReligionId { get; private set; }
        public string NID { get; private set; }
        public string BID { get; private set; }
        public Guid? BloodGroupId { get; private set; }
        public bool IsDeleted { get; private set; }

        public EmployeeDetail(Guid id) : base(id) { }
        private EmployeeDetail() : base(Guid.NewGuid()) { }

        public static EmployeeDetail Create(

         Guid? employeeId,
         string fathersName,
         string mothersName,
         string spouseName,
         Guid? maritalStatusId,
         Guid? religionId,
         string nID,
         string bID,
         Guid? bloodGroupId
        )

        {
            Guard.Against.NullOrEmptyGuid(employeeId.Value, "EmployeeId");
            

            var oModel = new EmployeeDetail(Guid.NewGuid());

            oModel.EmployeeId = employeeId;
            oModel.FathersName = fathersName;
            oModel.MothersName = mothersName;
            oModel.SpouseName = spouseName;
            oModel.MaritalStatusId = maritalStatusId;
            oModel.ReligionId = religionId;
            oModel.NID = nID;
            oModel.BID = bID;
            oModel.BloodGroupId = bloodGroupId;
            return oModel;
        }


        public void UpdateEmployeeDetail
            (

         Guid? employeeId,
         string fathersName,
         string mothersName,
         string spouseName,
         Guid? maritalStatusId,
         Guid? religionId,
         string nID,
         string bID,
         Guid? bloodGroupId

        )
        {
            Guard.Against.NullOrEmptyGuid(employeeId.Value, "EmployeeId");
            EmployeeId = employeeId;
            FathersName = fathersName;
            MothersName = mothersName;
            SpouseName = spouseName;
            MaritalStatusId = maritalStatusId;
            ReligionId = religionId;
            NID = nID;
            BID = bID;
            BloodGroupId = bloodGroupId;
        }

        public void MarkAsDeleteEmployeeDetail()
        {
            IsDeleted = true;
        }


    }
}

