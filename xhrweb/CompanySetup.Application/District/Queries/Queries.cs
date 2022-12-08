using CompanySetup.Application.District .Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.District .Queries
{
    public static class Queries
    {
        public class GetDistrictList : IRequest< List< DistrictVM>>
        {
            //public Guid CompanyId { get; set; }
        }

        public class GetDistrict : IRequest< DistrictVM>
        {
            public Guid Id { get; set; }
        }
    }
}
