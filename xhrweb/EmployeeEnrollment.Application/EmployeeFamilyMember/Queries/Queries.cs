using EmployeeEnrollment.Application.EmployeeFamilyMember.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeFamilyMember.Queries
{
    public static class Queries
    {
        public class GetEmployeeFamilyMemberList : IRequest<List<EmployeeFamilyMemberVM>>
        {
            //public Guid CompanyId { get; set; }
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeeFamilyMember : IRequest<EmployeeFamilyMemberVM>
        {
            public Guid Id { get; set; }
        }
    }
}
