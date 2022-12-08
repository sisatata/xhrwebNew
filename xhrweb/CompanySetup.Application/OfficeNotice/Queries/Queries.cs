using CompanySetup.Application.OfficeNotice.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.OfficeNotice.Queries
{
    public static class Queries
    {
        public class GetOfficeNoticeList : IRequest<List<OfficeNoticeVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetOfficeNotice : IRequest<OfficeNoticeVM>
        {
            public Guid Id { get; set; }
        }

        public class GetOfficeNoticeActiveAndPublishedList : IRequest<List<OfficeNoticeVM>>
        {
            public Guid CompanyId { get; set; }
        }
    }
}
