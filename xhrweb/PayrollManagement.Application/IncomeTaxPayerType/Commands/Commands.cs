using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.IncomeTaxPayerType.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateIncomeTaxPayerType : IRequest<IncomeTaxPayerTypeCommandVM>
            {
                
                public string PayerTypeCode { get; set; }
                public string PayerTypeName { get; set; }
                public string Remarks { get; set; }
                public Boolean IsActive { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class UpdateIncomeTaxPayerType : IRequest<IncomeTaxPayerTypeCommandVM>
            {
                public Guid? Id { get; set; }
                public string PayerTypeCode { get; set; }
                public string PayerTypeName { get; set; }
                public string Remarks { get; set; }
                public Boolean IsActive { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class MarkAsDeleteIncomeTaxPayerType : IRequest<IncomeTaxPayerTypeCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
