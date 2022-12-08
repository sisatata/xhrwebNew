using PayrollManagement.Application.EmployeeBankAccount.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.EmployeeBankAccount.Queries
{
    public static class Queries
    {
        public class GetEmployeeBankAccountList : IRequest<List<EmployeeBankAccountVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeeBankAccount : IRequest<EmployeeBankAccountVM>
        {
            public Guid Id { get; set; }
        }
        public class GetEmployeeBankAccountListByEmployee : IRequest<List<EmployeeBankAccountVM>>
        {
            public Guid CompanyId { get; set; }
            public Guid EmployeeId { get; set; }
        }
    }
}
