using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.District.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateDistrict : IRequest<DistrictCommandVM>
            {

                public Guid DivisionId { get; set; }
                public string Name { get; set; }
                public string NameLocalized { get; set; }
                public float? Latitude { get; set; }
                public float? Longitude { get; set; }
                public string Website { get; set; }
            }

            public class UpdateDistrict : IRequest<DistrictCommandVM>
            {
                public Guid Id { get; set; }
                public Guid DivisionId { get; set; }
                public string Name { get; set; }
                public string NameLocalized { get; set; }
                public float? Latitude { get; set; }
                public float? Longitude { get; set; }
                public string Website { get; set; }
            }

            public class MarkAsDeleteDistrict : IRequest<DistrictCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
