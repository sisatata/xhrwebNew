using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.DefaultConfiguration.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateDefaultConfiguration : IRequest<DefaultConfigurationCommandVM>
            {
                
                public string Code { get; set; }
                public string DefaultValue { get; set; }
                public string Description { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class UpdateDefaultConfiguration : IRequest<DefaultConfigurationCommandVM>
            {
                public Guid Id { get; set; }
                public string Code { get; set; }
                public string DefaultValue { get; set; }
                public string Description { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class MarkAsDeleteDefaultConfiguration : IRequest<DefaultConfigurationCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
