using CompanySetup.Application.Branch.Commands.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Branch.Commands
{
    public static class BranchCommands
    {
        public static class V1
        {
            public class CreateBranch : IRequest<BranchCommandVM>
            {
                public Guid CompanyId { get; set; }
                public string BranchName { get; set; }
                public string ShortName { get; set; }
                public string BranchLocalizedName { get; set; }
                public uint SortOrder { get; set; }
            }

            public class UpdateBranch : IRequest<BranchCommandVM>
            {
                public Guid Id { get; set; }
                public Guid CompanyId { get; set; }
                public string BranchName { get; set; }
                public string ShortName { get; set; }
                public string BranchLocalizedName { get; set; }
                public uint SortOrder { get; set; }
            }

            public class MarkBranchAsDelete : IRequest<BranchCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
