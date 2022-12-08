using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeCard : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid EmployeeId { get; private set; }
        public string CardMaskingValue { get; private set; }
        public DateTime? IssueDate { get; private set; }
        public decimal? ChargeAmount { get; private set; }
        public Boolean IsPaid { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public Boolean CardRevoked { get; private set; }
        public DateTime? CardRevokedDate { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }


        public EmployeeCard(Guid id) : base(id) { }
        private EmployeeCard() : base(Guid.NewGuid()) { }

        public static EmployeeCard Create(

         Guid employeeId,
         string cardMaskingValue,
         DateTime? issueDate,
         decimal? chargeAmount,
         Boolean isPaid,
         DateTime? paymentDate,
         Boolean cardRevoked,
         DateTime? cardRevokedDate,
         Guid? companyId,
         Boolean isDeleted

        )

        {
            var oModel = new EmployeeCard(Guid.NewGuid());

            oModel.EmployeeId = employeeId;
            oModel.CardMaskingValue = cardMaskingValue;
            oModel.IssueDate = issueDate;
            oModel.ChargeAmount = chargeAmount;
            oModel.IsPaid = isPaid;
            oModel.PaymentDate = paymentDate;
            oModel.CardRevoked = cardRevoked;
            oModel.CardRevokedDate = cardRevokedDate;
            oModel.CompanyId = companyId;
            oModel.IsDeleted = isDeleted;

            return oModel;

        }


        public void UpdateEmployeeCard
            (

         Guid employeeId,
         string cardMaskingValue,
         DateTime? issueDate,
         decimal? chargeAmount,
         Boolean isPaid,
         DateTime? paymentDate,
         Boolean cardRevoked,
         DateTime? cardRevokedDate,
         Guid? companyId,
         Boolean isDeleted


        )
        {
            EmployeeId = employeeId;
            CardMaskingValue = cardMaskingValue;
            IssueDate = issueDate;
            ChargeAmount = chargeAmount;
            IsPaid = isPaid;
            PaymentDate = paymentDate;
            CardRevoked = cardRevoked;
            CardRevokedDate = cardRevokedDate;
            CompanyId = companyId;
            IsDeleted = isDeleted;
        }


        public void MarkAsDeleteEmployeeCard()
        {
            IsDeleted = true;
        }


    }
}

