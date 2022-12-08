using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BankBranch.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateBankBranch : IRequest<BankBranchCommandVM>
            {
                public string BranchName { get; set; }
                public string BranchNameLC { get; set; }
                public string BranchAddress { get; set; }
                public string ContactPerson { get; set; }
                public string ContactNumber { get; set; }
                public string ContactEmailId { get; set; }
                public Int32? SortOrder { get; set; }
                public Guid? CompanyId { get; set; }
                public Guid BankId { get; set; }
            }

            public class UpdateBankBranch : IRequest<BankBranchCommandVM>
            {
                public Guid? Id { get; set; }
                public string BranchName { get; set; }
                public string BranchNameLC { get; set; }
                public string BranchAddress { get; set; }
                public string ContactPerson { get; set; }
                public string ContactNumber { get; set; }
                public string ContactEmailId { get; set; }
                public Int32? SortOrder { get; set; }
                public Guid? CompanyId { get; set; }
                public Guid BankId { get; set; }
            }

            public class MarkAsDeleteBankBranch : IRequest<BankBranchCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
