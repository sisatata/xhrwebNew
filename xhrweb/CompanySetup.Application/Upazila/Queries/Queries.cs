using CompanySetup.Application.Upazila.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.Upazila.Queries
{
    public static class Queries
    {
        public class GetUpazilaList : IRequest<List<UpazilaVM>>
        {
            public Guid DistrictId { get; set; }
        }

        public class GetUpazila : IRequest<UpazilaVM>
        {
            public Guid Id { get; set; }
        }
    }
}
