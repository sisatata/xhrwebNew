using EmployeeEnrollment.Application.AdminDashBoard.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.AdminDashBoard.Queries
{
    public static class Queries
    {
        public class GetAdminDashBoard : IRequest<AdminDashBoardVM>
        {
            public Guid CompanyId { get; set; }
            public Guid ManagerId { get; set; }
            public DateTime PunchDate { get; set; }
        }

    }
}
