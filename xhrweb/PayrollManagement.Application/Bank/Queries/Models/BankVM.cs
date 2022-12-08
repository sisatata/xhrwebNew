using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.Bank.Queries.Models
{
    public class BankVM
    {
        public Guid? Id { get; set; }
        public string BankName { get; set; }
        public string BankNameLC { get; set; }
        public Int32? SortOrder { get; set; }
        public Guid? CompanyId { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int16? PaymentOptionId { get; set; }
    }
}
