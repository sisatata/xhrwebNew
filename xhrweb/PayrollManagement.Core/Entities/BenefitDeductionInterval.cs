using ASL.Hrms.SharedKernel;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class BenefitDeductionInterval : BaseEntity<Guid>, IAggregateRoot
    {

        public Int32? IntervalId { get; private set; }
        public string IntervalName { get; private set; }
        public Boolean IsDeleted { get; private set; }

        public BenefitDeductionInterval(Guid id) : base(id) { }
        private BenefitDeductionInterval() : base(Guid.NewGuid()) { }

        public static BenefitDeductionInterval Create(

         Int32? intervalId,
         string intervalName
        )

        {
            var oModel = new BenefitDeductionInterval(Guid.NewGuid());


            oModel.IntervalId = intervalId;
            oModel.IntervalName = intervalName;

            return oModel;

        }


        public void UpdateBenefitDeductionInterval
            (

         
         Int32? intervalId,
         string intervalName
        )
        {

            IntervalId = intervalId;
            IntervalName = intervalName;
        }


        public void MarkAsDeleteBenefitDeductionInterval()
        {
            IsDeleted = true;
        }


    }
}

