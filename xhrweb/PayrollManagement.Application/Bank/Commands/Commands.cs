using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.Bank.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateBank : IRequest<BankCommandVM>
            {
                public string BankName { get; set; }
                public string BankNameLC { get; set; }
                public Int32? SortOrder { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; set; }
                public Int16? PaymentOptionId { get; set; }
            }

            public class UpdateBank : IRequest<BankCommandVM>
            {
                public Guid? Id { get; set; }
                public string BankName { get; set; }
                public string BankNameLC { get; set; }
                public Int32? SortOrder { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; set; }
                public Int16? PaymentOptionId { get; set; }
            }

            public class MarkAsDeleteBank : IRequest<BankCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
