using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.CustomConfiguration.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateCustomConfiguration : IRequest<CustomConfigurationCommandVM>
            {

                public string Code { get; set; }
                public string CustomValue { get; set; }
                public string Description { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid CompanyId { get; set; }
            }

            public class UpdateCustomConfiguration : IRequest<CustomConfigurationCommandVM>
            {
                public Guid Id { get; set; }
                public string Code { get; set; }
                public string CustomValue { get; set; }
                public string Description { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid CompanyId { get; set; }
            }

            public class MarkAsDeleteCustomConfiguration : IRequest<CustomConfigurationCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
