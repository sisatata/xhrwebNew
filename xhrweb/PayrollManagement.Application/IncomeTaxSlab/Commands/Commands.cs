using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.IncomeTaxSlab.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateIncomeTaxSlab : IRequest<IncomeTaxSlabCommandVM>
            {
                
                public string SlabName { get; set; }
                public string Description { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public Boolean IsRunningFlag { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
                public decimal? SlabAmount { get; set; }
                public decimal? TaxablePercent { get; set; }
                public string TaxPayerTypeCode { get; set; }
                public Int32? SlabOrder { get; set; }
            }

            public class UpdateIncomeTaxSlab : IRequest<IncomeTaxSlabCommandVM>
            {
                public Guid? Id { get; set; }
                public string SlabName { get; set; }
                public string Description { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public Boolean IsRunningFlag { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
                public decimal? SlabAmount { get; set; }
                public decimal? TaxablePercent { get; set; }
                public string TaxPayerTypeCode { get; set; }
                public Int32? SlabOrder { get; set; }
            }

            public class MarkAsDeleteIncomeTaxSlab : IRequest<IncomeTaxSlabCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
