using CompanySetup.Application.Designation.Commands.Models;
using MediatR;
using System;

namespace CompanySetup.Application.Designation.Commands
{
    public static class DesignationCommands
    {
        public static class V1
        {
            public class CreateDesignation : IRequest<DesignationCommandVM>
            {
                public Guid LinkedEntityId { get; set; }
                public string LinkedEntityType { get; set; }
                public string DesignationName { get; set; }
                public string DesignationLocalizedName { get; set; }
                public string ShortName { get; set; }
                public uint SortOrder { get; set; }
            }

            public class UpdateDesignation : IRequest<DesignationCommandVM>
            {
                public Guid Id { get; set; }
                public Guid LinkedEntityId { get; set; }
                public string LinkedEntityType { get; set; }
                public string DesignationName { get; set; }
                public string DesignationLocalizedName { get; set; }
                public string ShortName { get; set; }
                public uint SortOrder { get; set; }
            }

            public class MarkDesignationAsDelete : IRequest<DesignationCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
