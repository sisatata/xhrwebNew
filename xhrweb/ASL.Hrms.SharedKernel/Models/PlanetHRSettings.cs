using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Models
{
    public class PlanetHRSettings
    {
        public string BaseUrl { get; set; }
        public string ClientUrl { get; set; }
        public string DomainName { get; set; }
        public ContentRoot ContentRoot { get; set; }
        public PlanetHRImageFileSettings PlanetHRImageFileSettings { get; set; }
        public PlanetHRAttachedFileSettings PlanetHRAttachedFileSettings { get; set; }
        public PlanetHRUploadFileSettings PlanetHRUploadFileSettings { get; set; }
    }

    public class ContentRoot
    {
        public string RootPath { get; set; }
        //public string ExamImagePath { get; set; }
        public string EmployeeImagePath { get; set; }
        public string AttachedFilePath { get; set; }
        public string EmployeeSignaturePath { get; set; }

        public string CompanyLogoPath { get; set; }
    }

    public class PlanetHRImageFileSettings
    {
        public string ValidExtensions { get; set; }
        public double MaxFileSize { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
    }

    public class PlanetHRAttachedFileSettings
    {
        public string ValidExtensions { get; set; }
        public double MaxFileSize { get; set; }
    }

    public class PlanetHRUploadFileSettings
    {
        public string ValidExtensions { get; set; }
        public double MaxFileSize { get; set; }
        public string TemplateFilePath { get; set; }
        public string UploadFilePath { get; set; }
        public string ReportPath { get; set; }
        public string TemplateName { get; set; }

    }
}
