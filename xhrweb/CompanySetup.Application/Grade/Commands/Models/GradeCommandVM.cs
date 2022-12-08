using System;


namespace CompanySetup.Application.Grade.Commands
{
    public class GradeCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
