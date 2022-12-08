using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class IncomeTaxParameter : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public string ParameterName { get; private set; }
        public decimal? LimitAmount { get; private set; }
        public decimal? LimitPercentageOfBasic { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string Remarks { get; private set; }
        public Boolean IsActive { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }
        public string PayerTypeCode { get; private set; }


        public IncomeTaxParameter(Guid id) : base(id) { }
        private IncomeTaxParameter() : base(Guid.NewGuid()) { }

        public static IncomeTaxParameter Create(
         string parameterName,
         decimal? limitAmount,
         decimal? limitPercentageOfBasic,
         DateTime? startDate,
         DateTime? endDate,
         string remarks,
         Boolean isActive,
         Boolean isDeleted,
         Guid? companyId,
         string payerTypeCode

        )

        {
            var oModel = new IncomeTaxParameter(Guid.NewGuid());


            oModel.ParameterName = parameterName.Trim();
            oModel.LimitAmount = limitAmount;
            oModel.LimitPercentageOfBasic = limitPercentageOfBasic;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.Remarks = remarks;
            oModel.IsActive = isActive;
            oModel.IsDeleted = isDeleted;
            oModel.CompanyId = companyId;
            oModel.PayerTypeCode = payerTypeCode;

            return oModel;

        }


        public void UpdateIncomeTaxParameter
            (

        string parameterName,
         decimal? limitAmount,
         decimal? limitPercentageOfBasic,
         DateTime? startDate,
         DateTime? endDate,
         string remarks,
         Boolean isActive,
         Boolean isDeleted,
         Guid? companyId,
         string payerTypeCode

        )
        {
            ParameterName = parameterName.Trim();
            LimitAmount = limitAmount;
            LimitPercentageOfBasic = limitPercentageOfBasic;
            StartDate = startDate;
            EndDate = endDate;
            Remarks = remarks;
            IsActive = isActive;
            IsDeleted = isDeleted;
            CompanyId = companyId;
            PayerTypeCode = payerTypeCode;
        }


        public void MarkAsDeleteIncomeTaxParameter()
        {
            IsDeleted = true;
        }


    }
}

