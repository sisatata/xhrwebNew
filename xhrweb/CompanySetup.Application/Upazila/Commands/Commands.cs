using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Upazila.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateUpazila : IRequest<UpazilaCommandVM>
            {
                public Guid DistrictId { get; set; }
                public string UpazilaName { get; set; }
                public string UpazilaNameLocalized { get; set; }
            }

            public class UpdateUpazila : IRequest<UpazilaCommandVM>
            {
                public Guid Id { get; set; }
                public Guid DistrictId { get; set; }
                public string UpazilaName { get; set; }
                public string UpazilaNameLocalized { get; set; }
            }

            public class MarkAsDeleteUpazila : IRequest<UpazilaCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
