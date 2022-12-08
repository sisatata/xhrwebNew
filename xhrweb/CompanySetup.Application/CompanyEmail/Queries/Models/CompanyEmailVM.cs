using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.CompanyEmail.Queries.Models
{
    public class CompanyEmailVM
    {
        public Guid? Id { get; set; }
        public Guid? CompanyId { get; set; }
        public string EmailAddress { get; set; }
        public string Remarks { get; set; }
        public Boolean IsPrimary { get; set; }
    }
}
