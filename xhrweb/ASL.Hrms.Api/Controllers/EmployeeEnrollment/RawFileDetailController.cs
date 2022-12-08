using System;
using System.IO;
using System.Threading.Tasks;
using ASL.Hrms.SharedKernel.Models;
using EmployeeEnrollment.Application.RawFileDetail.Commands;
using EmployeeEnrollment.Application.RawFileDetail.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASL.Hrms.Api.Controllers
{
    public class RawFileDetailController : BaseController
    {
        public RawFileDetailController(IOptions<PlanetHRSettings> settings)
        {
            _settings = settings;
        }
        private readonly IOptions<PlanetHRSettings> _settings;
        #region Queries

        [HttpGet("companyId/{companyId}")]
        public async Task<IActionResult> Get(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetRawFileDetailList{ CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetRawFileDetail { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost("employee-import-excel")]
        public async Task<IActionResult> Post([FromForm] Commands.V1.ImportEmployeeFromExcel command)
        {
            if (Request.Form.Files != null && Request.Form.Files.Count > 0 && Request.Form.Files[0].Length > 0)
            {
                
                var employeeExcel = Request.Form.Files?[0];

                var settings = _settings.Value;

                command.EmployeeExcelFile = employeeExcel;
                command.Settings = settings;

                var data = await Mediator.Send(command);
                return Ok(data);
            }

            return null;
        }
        [HttpPost("download-employee-template")]
        public async Task<IActionResult> Post()
        {
            //var data = await Mediator.Send(command);

            var reportFolder = _settings.Value.ContentRoot.RootPath + _settings.Value.PlanetHRUploadFileSettings.TemplateFilePath;
            var fileName = _settings.Value.PlanetHRUploadFileSettings.TemplateName;
            var downloadUrl = Path.Combine(reportFolder, fileName);

            return ReturnExcelFileFromFilePath(downloadUrl, fileName);
        }

        [HttpPost("download-employee-file-status")]
        public async Task<IActionResult> Post([FromBody] Commands.V1.DownloadEmployeeFromExcelStatusReport command)
        {
            var settings = _settings.Value;
            command.Settings = settings;
            var data = await Mediator.Send(command);

            var reportFolder = _settings.Value.ContentRoot.RootPath + _settings.Value.PlanetHRUploadFileSettings.ReportPath;
            //var fileName = _settings.Value.PlanetHRUploadFileSettings.TemplateName;
            var downloadUrl = Path.Combine(reportFolder, data.uri);

            return ReturnExcelFileFromFilePath(downloadUrl, data.uri);
        }

        private IActionResult ReturnExcelFileFromFilePath(string excelFile, string fileName)
        {
            var stream = new MemoryStream();

            using (FileStream fileStream = System.IO.File.OpenRead(excelFile))
            {
                stream.SetLength(fileStream.Length);
                fileStream.Read(stream.GetBuffer(), 0, (int)fileStream.Length);
            }

            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        #endregion
    }
}