using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.CompanyPhone.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateCompanyPhone : IRequest<CompanyPhoneCommandVM>
            {
                public Guid? CompanyId { get; set; }
                public string PhoneNumber { get; set; }
                public Guid? PhoneTypeId { get; set; }
            }

            public class UpdateCompanyPhone : IRequest<CompanyPhoneCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? CompanyId { get; set; }
                public string PhoneNumber { get; set; }
                public Guid? PhoneTypeId { get; set; }
            }

            public class MarkAsDeleteCompanyPhone : IRequest<CompanyPhoneCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
