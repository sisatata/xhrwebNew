using System;


namespace EmployeeEnrollment.Application.EmployeeFamilyMember.Commands
{
    public class EmployeeFamilyMemberCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
