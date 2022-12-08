using System;


namespace EmployeeEnrollment.Application.EmployeeEnrollment.Commands
{
    public class EmployeeEnrollmentCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }


    public class UpdateEmployeeImageCommandVM
    {
        public Guid Id { get; set; }
        public string PictureUri { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
    public class UpdateEmployeeSignatureCommandVM
    {
        public Guid Id { get; set; }
        public string SignatureUri { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
