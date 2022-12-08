using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeCard.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeCard : IRequest<EmployeeCardCommandVM>
            {
                public Guid EmployeeId { get; set; }
                public string CardMaskingValue { get; set; }
                public DateTime? IssueDate { get; set; }
                public decimal? ChargeAmount { get; set; }
                public Boolean IsPaid { get; set; }
                public DateTime? PaymentDate { get; set; }
                public Boolean CardRevoked { get; set; }
                public DateTime? CardRevokedDate { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; private set; }
            }

            public class UpdateEmployeeCard : IRequest<EmployeeCardCommandVM>
            {
                public Guid Id { get; set; }
                public Guid EmployeeId { get; set; }
                public string CardMaskingValue { get; set; }
                public DateTime? IssueDate { get; set; }
                public decimal? ChargeAmount { get; set; }
                public Boolean IsPaid { get; set; }
                public DateTime? PaymentDate { get; set; }
                public Boolean CardRevoked { get; set; }
                public DateTime? CardRevokedDate { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; private set; }
            }

            public class MarkAsDeleteEmployeeCard : IRequest<EmployeeCardCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
