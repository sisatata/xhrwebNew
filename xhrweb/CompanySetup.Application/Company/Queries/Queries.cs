using CompanySetup.Application.Company.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.Company.Queries
{
    public static class Queries
    {
        public class GetAllCompanyByApprovalStatus : IRequest<List<CompanyVM>>
        {
            public string ApprovalStatus { get; set; }
        }

        public class GetCompanyListByLoginUser : IRequest<List<CompanyVM>>
        {
        }

        public class GetAllCompany : IRequest<List<CompanyVM>>
        {
        }
        public class GetCompanyById : IRequest<CompanyVOA>
        {
            public Guid CompanyId { get; set; }
        }
    }
}
