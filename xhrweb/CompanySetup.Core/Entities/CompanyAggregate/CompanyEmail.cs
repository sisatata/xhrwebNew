using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class CompanyEmail : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid? CompanyId { get; private set; }
        public string EmailAddress { get; private set; }
        public string Remarks { get; private set; }
        public Boolean IsPrimary { get; private set; }
        public Boolean IsDeleted { get; private set; }

        public CompanyEmail(Guid id) : base(id) { }
        private CompanyEmail() : base(Guid.NewGuid()) { }

        public static CompanyEmail Create(

         Guid? companyId,
         string emailAddress,
         string remarks,
         Boolean isPrimary

        )

        {
            var oModel = new CompanyEmail(Guid.NewGuid());

            oModel.CompanyId = companyId;
            oModel.EmailAddress = emailAddress;
            oModel.Remarks = remarks;
            oModel.IsPrimary = isPrimary;
            oModel.IsDeleted = false;
            return oModel;

        }


        public void UpdateCompanyEmail
            (

         Guid? companyId,
         string emailAddress,
         string remarks,
         Boolean isPrimary

        )
        {
            CompanyId = companyId;
            EmailAddress = emailAddress;
            Remarks = remarks;
            IsPrimary = isPrimary;
        }


        public void MarkAsDeleteCompanyEmail()
        {
            IsDeleted = true;
        }


    }
}

