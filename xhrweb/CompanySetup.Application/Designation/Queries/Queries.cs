using CompanySetup.Application.Designation.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Designation.Queries
{
    public static class Queries
    {
        public class GetDesignationByEntity : IRequest<List<DesignationVM>>
        {
            public Guid EntityId { get; set; }
        }

        public class GetDesignationListByEntities : IRequest<List<DesignationVM>>
        {
            public Guid CompanyId{ get; set; }
            public List<Guid> EntityIds { get; set; }
        }

        public class GetDesignationById : IRequest<DesignationVM>
        {
            public Guid Id { get; set; }
        }
    }
}
