using PayrollManagement.Application.EmployeePFTransaction.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.EmployeePFTransaction.Queries
{
    public static class Queries
    {
        public class GetEmployeePFTransactionList : IRequest<List<EmployeePFTransactionVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeePFTransaction : IRequest<EmployeePFTransactionVM>
        {
            public Guid Id { get; set; }
        }
    }
}
