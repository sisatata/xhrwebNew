using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class BenefitDeductionEmployeeAssigned : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {

        public Guid? BenefitDeductionId { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public string Remarks { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public decimal? Amount { get; private set; }


        public BenefitDeductionEmployeeAssigned(Guid id) : base(id) { }
        private BenefitDeductionEmployeeAssigned() : base(Guid.NewGuid()) { }

        public static BenefitDeductionEmployeeAssigned Create(


         Guid? benefitDeductionId,
         Guid? employeeId,
         string remarks,
         Boolean isDeleted,
         DateTime? startDate,
         DateTime? endDate,
         decimal? amount

        )

        {
            var oModel = new BenefitDeductionEmployeeAssigned(Guid.NewGuid());


            oModel.BenefitDeductionId = benefitDeductionId;
            oModel.EmployeeId = employeeId;
            oModel.Remarks = remarks;
            oModel.IsDeleted = isDeleted;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.Amount = amount;

            return oModel;

        }


        public void UpdateBenefitDeductionEmployeeAssigned
            (


         Guid? benefitDeductionId,
         Guid? employeeId,
         string remarks,
         Boolean isDeleted,
         DateTime? startDate,
         DateTime? endDate,
         decimal? amount

        )
        {

            BenefitDeductionId = benefitDeductionId;
            EmployeeId = employeeId;
            Remarks = remarks;
            IsDeleted = isDeleted;
            StartDate = startDate;
            EndDate = endDate;
            Amount = amount;
        }


        public void MarkAsDeleteBenefitDeductionEmployeeAssigned()
        {
            IsDeleted = true;
        }


    }
}

