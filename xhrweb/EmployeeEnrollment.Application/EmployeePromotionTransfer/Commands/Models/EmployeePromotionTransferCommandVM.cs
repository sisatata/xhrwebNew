using System;


namespace EmployeeEnrollment.Application.EmployeePromotionTransfer.Commands
{
    public class EmployeePromotionTransferCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
