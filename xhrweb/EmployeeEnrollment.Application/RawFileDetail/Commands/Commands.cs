using ASL.Hrms.SharedKernel.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.RawFileDetail.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateRawFileDetail : IRequest<RawFileDetailCommandVM>
            {
                public string FileName { get; set; }
                public string FileType { get; set; }
                public string Comments { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class UpdateRawFileDetail : IRequest<RawFileDetailCommandVM>
            {
                public Guid? Id { get; set; }
                public string FileName { get; set; }
                public string FileType { get; set; }
                public string Comments { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class ImportEmployeeFromExcel : IRequest<ImportEmployeeFromExcelVM>
            {
                public Guid? Id { get; set; }
                public string FileName { get; set; }
                public string FileType { get; set; }
                public string Comments { get; set; }
                public Guid? CompanyId { get; set; }
                public PlanetHRSettings Settings { get; set; }
                public IFormFile EmployeeExcelFile { get; set; }
            }

            public class DownloadEmployeeFromExcelStatusReport : IRequest<DownloadEmployeeFromExcelFileStatusVM>
            {
                public Guid? Id { get; set; }
                public string FileName { get; set; }
                public Guid? CompanyId { get; set; }
                public PlanetHRSettings Settings { get; set; }
            }
            public class MarkAsDeleteRawFileDetail : IRequest<RawFileDetailCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
