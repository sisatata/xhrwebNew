using CompanySetup.Application.Grade.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.Grade.Queries
{
    public static class Queries
    {
        public class GetGradeList : IRequest<List<GradeVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetGrade : IRequest<GradeVM>
        {
            public Guid Id { get; set; }
        }
    }
}
