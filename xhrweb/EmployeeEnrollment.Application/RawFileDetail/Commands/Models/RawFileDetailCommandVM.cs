using System;


namespace EmployeeEnrollment.Application.RawFileDetail.Commands
{
    public class RawFileDetailCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class ImportEmployeeFromExcelVM
    {
        public Guid Id { get; set; }
        //public string PictureUri { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
    public class DownloadEmployeeFromExcelTemplateVM
    {
        public string uri { get; set; }
    }
    public class DownloadEmployeeFromExcelFileStatusVM
    {
        public string uri { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
