using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeBankAccount.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeBankAccount : IRequest<EmployeeBankAccountCommandVM>
            {
                public Guid? BankId { get; set; }
                public Guid? BankBranchId { get; set; }
                public string AccountNo { get; set; }
                public string AccountTitle { get; set; }
                public Boolean IsPrimary { get; set; }
                public Guid? CompanyId { get; set; }
                public Int16? PaymentOptionId { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public string Remarks { get; set; }

                public Guid EmployeeId { get; set; }
            }

            public class UpdateEmployeeBankAccount : IRequest<EmployeeBankAccountCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? BankId { get; set; }
                public Guid? BankBranchId { get; set; }
                public string AccountNo { get; set; }
                public string AccountTitle { get; set; }
                public Boolean IsPrimary { get; set; }
                public Guid? CompanyId { get; set; }
                public Int16? PaymentOptionId { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public string Remarks { get; set; }
                public Guid EmployeeId { get; set; }
            }

            public class MarkAsDeleteEmployeeBankAccount : IRequest<EmployeeBankAccountCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
