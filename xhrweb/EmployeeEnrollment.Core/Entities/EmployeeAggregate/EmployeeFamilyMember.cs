using ASL.Hrms.SharedKernel;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeFamilyMember : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid EmployeeId { get; private set; }
        public string FamiliyMemberName { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public string EducationClass { get; private set; }
        public string EducationalInstitute { get; private set; }
        public string EducationYear { get; private set; }
        public Guid RelationTypeId { get; private set; }
        public Boolean IsDependant { get; private set; }
        public Boolean IsEligibleForMedical { get; private set; }
        public Boolean IsEligibleForEducation { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid CompanyId { get; private set; }


        public EmployeeFamilyMember(Guid id) : base(id) { }
        private EmployeeFamilyMember() : base(Guid.NewGuid()) { }

        public static EmployeeFamilyMember Create(

         Guid employeeId,
         string familiyMemberName,
         DateTime? dateOfBirth,
         string educationClass,
         string educationalInstitute,
         string educationYear,
         Guid relationTypeId,
         Boolean isDependant,
         Boolean isEligibleForMedical,
         Boolean isEligibleForEducation,
         Boolean isDeleted,
         Guid companyId

        )

        {
            var oModel = new EmployeeFamilyMember(Guid.NewGuid());

            oModel.EmployeeId = employeeId;
            oModel.FamiliyMemberName = familiyMemberName;
            oModel.DateOfBirth = dateOfBirth;
            oModel.EducationClass = educationClass;
            oModel.EducationalInstitute = educationalInstitute;
            oModel.EducationYear = educationYear;
            oModel.RelationTypeId = relationTypeId;
            oModel.IsDependant = isDependant;
            oModel.IsEligibleForMedical = isEligibleForMedical;
            oModel.IsEligibleForEducation = isEligibleForEducation;
            oModel.IsDeleted = isDeleted;
            oModel.CompanyId = companyId;

            return oModel;

        }


        public void UpdateEmployeeFamilyMember
            (

         Guid employeeId,
         string familiyMemberName,
         DateTime? dateOfBirth,
         string educationClass,
         string educationalInstitute,
         string educationYear,
         Guid relationTypeId,
         Boolean isDependant,
         Boolean isEligibleForMedical,
         Boolean isEligibleForEducation,
         Boolean isDeleted,
         Guid companyId

        )
        {
            EmployeeId = employeeId;
            FamiliyMemberName = familiyMemberName;
            DateOfBirth = dateOfBirth;
            EducationClass = educationClass;
            EducationalInstitute = educationalInstitute;
            EducationYear = educationYear;
            RelationTypeId = relationTypeId;
            IsDependant = isDependant;
            IsEligibleForMedical = isEligibleForMedical;
            IsEligibleForEducation = isEligibleForEducation;
            IsDeleted = isDeleted;
            CompanyId = companyId;
        }


        public void MarkAsDeleteEmployeeFamilyMember()
        {
            IsDeleted = true;
        }


    }
}

