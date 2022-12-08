using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Grade.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateGrade : IRequest<GradeCommandVM>
            {

                public string GradeName { get; set; }
                public string GradeNameLocalized { get; set; }
                public Int32? Rank { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class UpdateGrade : IRequest<GradeCommandVM>
            {
                public Guid? Id { get; set; }
                public string GradeName { get; set; }
                public string GradeNameLocalized { get; set; }
                public Int32? Rank { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class MarkAsDeleteGrade : IRequest<GradeCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
