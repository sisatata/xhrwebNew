using EmployeeEnrollment.Core.Interfaces;
using System;
using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;

namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeExperience : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid EmployeeId { get; private set; }
        public string CompanyName { get; private set; }
        public string Designation { get; private set; }
        public string FunctionalDesignation { get; private set; }
        public string Department { get; private set; }
        public string Responsibilities { get; private set; }
        public string CompanyAddress { get; private set; }
        public string CompanyContactNo { get; private set; }
        public string CompanyMobile { get; private set; }
        public string CompanyEmail { get; private set; }
        public string CompanyWeb { get; private set; }
        public DateTime FromDate { get; private set; }
        public DateTime? ToDate { get; private set; }
        public Boolean TilDate { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid CompanyId { get; private set; }


        public EmployeeExperience(Guid id) : base(id) { }
        private EmployeeExperience() : base(Guid.NewGuid()) { }

        public static EmployeeExperience Create(

         //Guid? id,
         Guid employeeId,
         string companyName,
         string designation,
         string functionalDesignation,
         string department,
         string responsibilities,
         string companyAddress,
         string companyContactNo,
         string companyMobile,
         string companyEmail,
         string companyWeb,
         DateTime fromDate,
         DateTime? toDate,
         Boolean tilDate,
         Boolean isDeleted,
         Guid companyId

        )

        {
            Guard.Against.NullOrEmptyGuid(employeeId, "EmployeeId");
            Guard.Against.NullOrWhiteSpace(companyName, "CompanyName");
            Guard.Against.NullOrWhiteSpace(designation, "Designation");
            Guard.Against.NullOrWhiteSpace(responsibilities, "Responsibilities");
            Guard.Against.OutOfSQLDateRange(fromDate, "ToDate");

            var oModel = new EmployeeExperience(Guid.NewGuid());

            //oModel.Id = id;
            oModel.EmployeeId = employeeId;
            oModel.CompanyName = companyName;
            oModel.Designation = designation;
            oModel.FunctionalDesignation = functionalDesignation;
            oModel.Department = department;
            oModel.Responsibilities = responsibilities;
            oModel.CompanyAddress = companyAddress;
            oModel.CompanyContactNo = companyContactNo;
            oModel.CompanyMobile = companyMobile;
            oModel.CompanyEmail = companyEmail;
            oModel.CompanyWeb = companyWeb;
            oModel.FromDate = fromDate;
            oModel.ToDate = toDate;
            oModel.TilDate = tilDate;
            oModel.IsDeleted = isDeleted;
            oModel.CompanyId = companyId;

            return oModel;

        }


        public void UpdateEmployeeExperience
            (

         //Guid? id,
         Guid employeeId,
         string companyName,
         string designation,
         string functionalDesignation,
         string department,
         string responsibilities,
         string companyAddress,
         string companyContactNo,
         string companyMobile,
         string companyEmail,
         string companyWeb,
         DateTime fromDate,
         DateTime? toDate,
         Boolean tilDate,
         Boolean isDeleted,
         Guid companyId

        )
        {
            Guard.Against.NullOrEmptyGuid(employeeId, "EmployeeId");
            Guard.Against.NullOrWhiteSpace(companyName, "CompanyName");
            Guard.Against.NullOrWhiteSpace(designation, "Designation");
            Guard.Against.NullOrWhiteSpace(responsibilities, "Responsibilities");
            Guard.Against.OutOfSQLDateRange(fromDate, "ToDate");
            //Id = id;
            EmployeeId = employeeId;
            CompanyName = companyName;
            Designation = designation;
            FunctionalDesignation = functionalDesignation;
            Department = department;
            Responsibilities = responsibilities;
            CompanyAddress = companyAddress;
            CompanyContactNo = companyContactNo;
            CompanyMobile = companyMobile;
            CompanyEmail = companyEmail;
            CompanyWeb = companyWeb;
            FromDate = fromDate;
            ToDate = toDate;
            TilDate = tilDate;
            IsDeleted = isDeleted;
            CompanyId = companyId;
        }


        public void MarkAsDeleteEmployeeExperience()
        {
            IsDeleted = true;
        }


    }
}

