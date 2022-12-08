using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeBankAccount.Queries.Models
{
    public class EmployeeBankAccountVM
    {
        public Guid? Id { get; set; }
        public Guid? BankId { get; set; }
        public Guid? BankBranchId { get; set; }
        public string AccountNo { get; set; }
        public string AccountTitle { get; set; }
        public Boolean IsPrimary { get; set; }
        public Guid? CompanyId { get; set; }
        public Boolean IsDeleted { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Remarks { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string FullName { get; set; }
    }
}
