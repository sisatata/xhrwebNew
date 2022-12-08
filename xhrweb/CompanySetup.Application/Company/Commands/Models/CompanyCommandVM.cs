using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Company.Commands.Models
{
    public class CompanyCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class UpdateCompanyImageCommandVM
    {
        public Guid Id { get; set; }
        public string PictureUri { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
