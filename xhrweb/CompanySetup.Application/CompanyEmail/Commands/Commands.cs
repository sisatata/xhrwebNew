using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.CompanyEmail.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateCompanyEmail : IRequest<CompanyEmailCommandVM>
            {
                public Guid? CompanyId { get; set; }
                public string EmailAddress { get; set; }
                public string Remarks { get; set; }
                public Boolean IsPrimary { get; set; }
            }

            public class UpdateCompanyEmail : IRequest<CompanyEmailCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? CompanyId { get; set; }
                public string EmailAddress { get; set; }
                public string Remarks { get; set; }
                public Boolean IsPrimary { get; set; }
            }

            public class MarkAsDeleteCompanyEmail : IRequest<CompanyEmailCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
