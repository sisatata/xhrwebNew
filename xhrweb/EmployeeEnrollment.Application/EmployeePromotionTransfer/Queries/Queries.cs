using EmployeeEnrollment.Application.EmployeePromotionTransfer.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeePromotionTransfer.Queries
{
    public static class Queries
    {
        public class GetEmployeePromotionTransferListByCompany : IRequest<List<EmployeePromotionTransferVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeePromotionTransferListByEmployee : IRequest<List<EmployeePromotionTransferVM>>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeePromotionTransfer : IRequest<EmployeePromotionTransferVM>
        {
            public Guid Id { get; set; }
        }
    }
}
