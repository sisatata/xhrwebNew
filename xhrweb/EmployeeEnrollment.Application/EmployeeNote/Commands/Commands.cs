using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeNote.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeNote : IRequest<EmployeeNoteCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? EmployeeId { get; set; }
                public string Note { get; set; }
                public Guid? DownloadId { get; set; }
                public Boolean DisplayToEmployee { get; set; }
            }

            public class UpdateEmployeeNote : IRequest<EmployeeNoteCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? EmployeeId { get; set; }
                public string Note { get; set; }
                public Guid? DownloadId { get; set; }
                public Boolean DisplayToEmployee { get; set; }
            }

            //public class MarkAsDeleteEmployeeNote : IRequest<EmployeeNoteCommandVM>
            //{
            //    public Guid Id { get; set; }
            //}
        }
    }
}
