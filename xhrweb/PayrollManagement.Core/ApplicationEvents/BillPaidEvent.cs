using System;
using ASL.Hrms.SharedKernel.Interfaces;

namespace PayrollManagement.Core.ApplicationEvents
{
   public class BillPaidEvent : ICommand
    {
        public Guid AccountManagerId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal PaidAmount { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTime CommandPublished { get; set; }
        public string UserId { get; set; }
    }
}
