using CompanySetup.Application.CommonLookUpType.Commands.Models;
using MediatR;
using System;

namespace CompanySetup.Application.CommonLookUpType.Commands
{
    public static class LookUpCommands
    {
        public static class V1
        {
            public class CreateCommonLookUpType : IRequest<CommonLookUpTypeCommandVM>
            {
                public string ShortCode { get; set; }
                public string LookUpTypeName { get; set; }
                public string LookUpTypeNameLC { get; set; }
                public string Remarks { get; set; }
                public Guid? CompanyId { get; set; }
                public Guid? ParentId { get; set; }
                public Int16 SortOrder { get; set; }
            }

            public class UpdateCommonLookUpType : IRequest<CommonLookUpTypeCommandVM>
            {
                public Guid Id { get; set; }
                public string ShortCode { get; set; }
                public string LookUpTypeName { get; set; }
                public string LookUpTypeNameLC { get; set; }
                public string Remarks { get; set; }
                public Guid? CompanyId { get; set; }
                public Guid? ParentId { get; set; }
                public Int16 SortOrder { get; set; }
            }

            public class MarkAsDeleteCommonLookUpType : IRequest<CommonLookUpTypeCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
