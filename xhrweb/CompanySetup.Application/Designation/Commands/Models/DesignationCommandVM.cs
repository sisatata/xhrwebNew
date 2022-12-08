using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Designation.Commands.Models
{
    public class DesignationCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
