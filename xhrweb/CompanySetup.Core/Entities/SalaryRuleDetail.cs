using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class SalaryRuleDetail : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid? SalaryRuleId { get; private set; }
        public string SalaryHead { get; private set; }
        public decimal? Value { get; private set; }
        public string ValueType { get; private set; }
        public string PercentDependOn { get; private set; }


        public SalaryRuleDetail(Guid id) : base(id) { }
        private SalaryRuleDetail() : base(Guid.NewGuid()) { }

        public static SalaryRuleDetail Create(

         Guid? salaryRuleId,
         string salaryHead,
         decimal? value,
         string valueType,
         string percentDependOn

        )

        {
            var oModel = new SalaryRuleDetail(Guid.NewGuid());

            oModel.SalaryRuleId = salaryRuleId;
            oModel.SalaryHead = salaryHead;
            oModel.Value = value;
            oModel.ValueType = valueType;
            oModel.PercentDependOn = percentDependOn;

            return oModel;

        }


        public void UpdateSalaryRuleDetail
            (

         Guid? salaryRuleId,
         string salaryHead,
         decimal? value,
         string valueType,
         string percentDependOn

        )
        {
            SalaryRuleId = salaryRuleId;
            SalaryHead = salaryHead;
            Value = value;
            ValueType = valueType;
            PercentDependOn = percentDependOn;
        }


        public void MarkAsDeleteSalaryRuleDetail()
        {
            //IsDeleted = true;
        }


    }
}

