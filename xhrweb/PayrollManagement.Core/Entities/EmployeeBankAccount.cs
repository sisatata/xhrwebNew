using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class EmployeeBankAccount : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? BankId { get; private set; }
        public Guid? BankBranchId { get; private set; }
        public string AccountNo { get; private set; }
        public string AccountTitle { get; private set; }
        public Boolean IsPrimary { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }
        //public Int16? PaymentOptionId { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string Remarks { get; private set; }
        public Guid EmployeeId { get; private set; }

        public EmployeeBankAccount(Guid id) : base(id) { }
        private EmployeeBankAccount() : base(Guid.NewGuid()) { }

        public static EmployeeBankAccount Create(

         Guid? bankId,
         Guid? bankBranchId,
         string accountNo,
         string accountTitle,
         Boolean isPrimary,
         Guid? companyId,
         Int16? paymentOptionId,
         DateTime? startDate,
         DateTime? endDate,
         string remarks,
          Guid employeeId
        )

        {
            var oModel = new EmployeeBankAccount(Guid.NewGuid());

            oModel.BankId = bankId;
            oModel.BankBranchId = bankBranchId;
            oModel.AccountNo = accountNo;
            oModel.AccountTitle = accountTitle;
            oModel.IsPrimary = isPrimary;
            oModel.CompanyId = companyId;
            oModel.IsDeleted = false;
            //oModel.PaymentOptionId = paymentOptionId;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.Remarks = remarks;
            oModel.EmployeeId = employeeId;
            return oModel;

        }


        public void UpdateEmployeeBankAccount
            (

         Guid? bankId,
         Guid? bankBranchId,
         string accountNo,
         string accountTitle,
         Boolean isPrimary,
         Guid? companyId,
         Int16? paymentOptionId,
         DateTime? startDate,
         DateTime? endDate,
         string remarks,
          Guid employeeId

        )
        {
            BankId = bankId;
            BankBranchId = bankBranchId;
            AccountNo = accountNo;
            AccountTitle = accountTitle;
            IsPrimary = isPrimary;
            CompanyId = companyId;
            IsDeleted = false;
            //PaymentOptionId = paymentOptionId;
            StartDate = startDate;
            EndDate = endDate;
            Remarks = remarks;
            EmployeeId = employeeId;
        }


        public void MarkAsDeleteEmployeeBankAccount()
        {
            IsDeleted = true;
        }


    }
}

