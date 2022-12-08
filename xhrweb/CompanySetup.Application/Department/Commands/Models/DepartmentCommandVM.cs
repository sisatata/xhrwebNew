using System;

namespace CompanySetup.Application.Department.Commands.Models
{
    public class DepartmentCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
