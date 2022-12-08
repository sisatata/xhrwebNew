using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class PreviousPFGratuityEarnLeave : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? EmployeeId { get; private set; }
        public decimal? PFAmount { get; private set; }
        public decimal? GratuityAmount { get; private set; }
        public decimal? EarnLeaveAmount { get; private set; }
        public DateTime? TillDate { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public string Remarks { get; private set; }


        public PreviousPFGratuityEarnLeave(Guid id) : base(id) { }
        private PreviousPFGratuityEarnLeave() : base(Guid.NewGuid()) { }

        public static PreviousPFGratuityEarnLeave Create(
         Guid? employeeId,
         decimal? pFAmount,
         decimal? gratuityAmount,
         decimal? earnLeaveAmount,
         DateTime? tillDate,
         Guid? companyId,
         Boolean isDeleted,
         string remarks
        )

        {
            var oModel = new PreviousPFGratuityEarnLeave(Guid.NewGuid());

            oModel.EmployeeId = employeeId;
            oModel.PFAmount = pFAmount;
            oModel.GratuityAmount = gratuityAmount;
            oModel.EarnLeaveAmount = earnLeaveAmount;
            oModel.TillDate = tillDate;
            oModel.CompanyId = companyId;
            oModel.IsDeleted = isDeleted;
            oModel.Remarks = remarks;

            return oModel;

        }


        public void UpdatePreviousPFGratuityEarnLeave
            (

         Guid? employeeId,
         decimal? pFAmount,
         decimal? gratuityAmount,
         decimal? earnLeaveAmount,
         DateTime? tillDate,
         Guid? companyId,
         Boolean isDeleted,
         string remarks
        )
        {
            EmployeeId = employeeId;
            PFAmount = pFAmount;
            GratuityAmount = gratuityAmount;
            EarnLeaveAmount = earnLeaveAmount;
            TillDate = tillDate;
            CompanyId = companyId;
            IsDeleted = isDeleted;
            Remarks = remarks;
        }


        public void MarkAsDeletePreviousPFGratuityEarnLeave()
        {
            IsDeleted = true;
        }


    }
}

