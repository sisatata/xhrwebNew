using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeEducation : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid EmployeeId { get; private set; }
        public Guid? EducationalDegreeId { get; private set; }
        public Guid? EducationalInstituteId { get; private set; }
        public string Session { get; private set; }
        public string PassingYear { get; private set; }
        public string BoardorUniversity { get; private set; }
        public string Result { get; private set; }
        public string ResultType { get; private set; }
        public decimal? OutOf { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid CompanyId { get; private set; }


        public EmployeeEducation(Guid id) : base(id) { }
        private EmployeeEducation() : base(Guid.NewGuid()) { }

        public static EmployeeEducation Create(

         Guid employeeId,
         Guid? educationalDegreeId,
         Guid? educationalInstituteId,
         string session,
         string passingYear,
         string boardorUniversity,
         string result,
         string resultType,
         decimal? outOf,
         Boolean isDeleted,
         Guid companyId

        )

        {
            Guard.Against.NullOrEmptyGuid(employeeId, "EmployeeId");

            Guard.Against.NullOrEmptyGuid(educationalDegreeId.Value, "EducationalDegreeId");
            Guard.Against.NullOrWhiteSpace(boardorUniversity, "BoardorUniversity");

            var oModel = new EmployeeEducation(Guid.NewGuid());

            oModel.EmployeeId = employeeId;
            oModel.EducationalDegreeId = educationalDegreeId;
            oModel.EducationalInstituteId = educationalInstituteId;
            oModel.Session = session;
            oModel.PassingYear = passingYear;
            oModel.BoardorUniversity = boardorUniversity;
            oModel.Result = result;
            oModel.ResultType = resultType;
            oModel.OutOf = outOf;
            oModel.IsDeleted = isDeleted;
            oModel.CompanyId = companyId;

            return oModel;

        }


        public void UpdateEmployeeEducation
            (

         Guid employeeId,
         Guid? educationalDegreeId,
         Guid? educationalInstituteId,
         string session,
         string passingYear,
         string boardorUniversity,
         string result,
         string resultType,
         decimal? outOf,
         Boolean isDeleted,
         Guid companyId

        )
        {
            Guard.Against.NullOrEmptyGuid(employeeId, "EmployeeId");

            Guard.Against.NullOrEmptyGuid(educationalDegreeId.Value, "EducationalDegreeId");
            Guard.Against.NullOrWhiteSpace(boardorUniversity, "BoardorUniversity");

            EmployeeId = employeeId;
            EducationalDegreeId = educationalDegreeId;
            EducationalInstituteId = educationalInstituteId;
            Session = session;
            PassingYear = passingYear;
            BoardorUniversity = boardorUniversity;
            Result = result;
            ResultType = resultType;
            OutOf = outOf;
            IsDeleted = isDeleted;
            CompanyId = companyId;
        }


        public void MarkAsDeleteEmployeeEducation()
        {
            IsDeleted = true;
        }


    }
}

