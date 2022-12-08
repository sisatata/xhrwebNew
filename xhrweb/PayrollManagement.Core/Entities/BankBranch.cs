using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class BankBranch : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public string BranchName { get; private set; }
        public string BranchNameLC { get; private set; }
        public string BranchAddress { get; private set; }
        public string ContactPerson { get; private set; }
        public string ContactNumber { get; private set; }
        public string ContactEmailId { get; private set; }
        public Int32? SortOrder { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }

        public Guid BankId { get; set; }

        public BankBranch(Guid id) : base(id) { }
        private BankBranch() : base(Guid.NewGuid()) { }

        public static BankBranch Create(

         string branchName,
         string branchNameLC,
         string branchAddress,
         string contactPerson,
         string contactNumber,
         string contactEmailId,
         Int32? sortOrder,
         Guid? companyId,
         Guid bankId

        )

        {
            var oModel = new BankBranch(Guid.NewGuid());

            oModel.BranchName = branchName;
            oModel.BranchNameLC = branchNameLC;
            oModel.BranchAddress = branchAddress;
            oModel.ContactPerson = contactPerson;
            oModel.ContactNumber = contactNumber;
            oModel.ContactEmailId = contactEmailId;
            oModel.SortOrder = sortOrder;
            oModel.CompanyId = companyId;
            oModel.IsDeleted = false;
            oModel.BankId = bankId;
            return oModel;

        }


        public void UpdateBankBranch
            (

         string branchName,
         string branchNameLC,
         string branchAddress,
         string contactPerson,
         string contactNumber,
         string contactEmailId,
         Int32? sortOrder,
         Guid? companyId,
         Guid bankId
        )
        {
            BranchName = branchName;
            BranchNameLC = branchNameLC;
            BranchAddress = branchAddress;
            ContactPerson = contactPerson;
            ContactNumber = contactNumber;
            ContactEmailId = contactEmailId;
            SortOrder = sortOrder;
            CompanyId = companyId;
            IsDeleted = false;
            BankId = bankId;
        }


        public void MarkAsDeleteBankBranch()
        {
            IsDeleted = true;
        }


    }
}

