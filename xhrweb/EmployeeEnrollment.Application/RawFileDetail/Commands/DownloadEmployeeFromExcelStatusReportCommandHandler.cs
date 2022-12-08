using ASL.Hrms.SharedKernel.Models;
using Dapper;
using EmployeeEnrollment.Application.EmployeeRawDataPrep.Queries.Models;
using EmployeeEnrollment.Application.RawFileDetail.Queries.Models;
using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using Npgsql;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.RawFileDetail.Commands
{
    public class DownloadEmployeeFromExcelStatusReportCommandHandler : IRequestHandler<Commands.V1.DownloadEmployeeFromExcelStatusReport, DownloadEmployeeFromExcelFileStatusVM>
    {
        public DownloadEmployeeFromExcelStatusReportCommandHandler(IAsyncRepository<EmployeeEntities.RawFileDetail, Guid>
        repository, DbConnection connection)
        {
            _repository = repository;
            _connection = connection;
        }

        private readonly IAsyncRepository<EmployeeEntities.RawFileDetail, Guid>
        _repository;
        private readonly DbConnection _connection;

        public async Task<DownloadEmployeeFromExcelFileStatusVM> Handle(Commands.V1.DownloadEmployeeFromExcelStatusReport request, CancellationToken cancellationToken)
        {
            var response = new DownloadEmployeeFromExcelFileStatusVM
            {
                uri = ""
            };
            DataTable dt = new DataTable();
            try
            {
                var query = $"SELECT * from employee.GetEployeeRawDataPrepByFileId('{request.Id}')";

                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = _connection.ConnectionString;
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.Dispose();
                    connection.Close();
                }

                var _settings = request.Settings;

                var filename = $"REPORT_{request.FileName}_{DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}.xlsx";
                Export(dt, filename, _settings);

                response.Status = true;
                response.Message = "report created successfully.";
                response.uri = filename;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public void Export(DataTable dt, string reportFileName, PlanetHRSettings settings)
        {
            var strFilePath = settings.ContentRoot.RootPath + settings.PlanetHRUploadFileSettings.TemplateFilePath + settings.PlanetHRUploadFileSettings.TemplateName; //"~\\Files\\EmployeeBulkUploadFormat.xlsx";
            var destinationDirectory = settings.ContentRoot.RootPath + settings.PlanetHRUploadFileSettings.ReportPath;
            if (!Directory.Exists(destinationDirectory))
            { Directory.CreateDirectory(destinationDirectory); }


            FileInfo file = new FileInfo(strFilePath);
            using (var excel = new ExcelPackage(file))
            {
                if (excel.Workbook.Worksheets.Count > 1)
                {
                    var Mich = excel.Workbook.Worksheets.SingleOrDefault(x => x.Name == "Mich");
                    if (Mich != null) { excel.Workbook.Worksheets.Delete(Mich); }
                    var dist = excel.Workbook.Worksheets.SingleOrDefault(x => x.Name == "District & Upozila");
                    if (dist != null) { excel.Workbook.Worksheets.Delete(dist); }

                }
                ExcelWorksheet worksheet = excel.Workbook.Worksheets["Sheet1"];
                //You could also use [line, column] notation:
                worksheet.InsertColumn(1, 3);
                worksheet.Cells["A2"].Value = "Status";
                worksheet.Cells["B2"].Value = "Error Description";
                worksheet.Cells["C2"].Value = "Planet ID";
                //worksheet.Cells["D2"].Value = "File Name";

                worksheet.Cells[1, 1, 2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1, 2, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[1, 1, 2, 3].Style.Font.Bold = true;
                //worksheet.Cells[1, 1, 2, 4].Style.Fill.BackgroundColor.SetColor(Color.GreenYellow);
                worksheet.Cells[1, 1, 1, 3].Merge = true;
                worksheet.Cells["A1"].Value = "Records Status";

                worksheet.Cells["A3"].LoadFromDataTable(dt, false);
                worksheet.Cells.AutoFitColumns();

                var formatExpressionCompleted = worksheet.ConditionalFormatting.AddEqual(new ExcelAddress(string.Format("A3:A{0}", 3 + dt.Rows.Count)));
                //var formatExpressionCompleted = worksheet.ConditionalFormatting.AddEqual(new ExcelAddress(3, 1, dt.Rows.Count, dt.Columns.Count));// string.Format("A3:A{0}", 3 + dt.Rows.Count)));
                formatExpressionCompleted.Formula = "\"E\"";
                formatExpressionCompleted.Style.Fill.PatternType = ExcelFillStyle.Solid;
                formatExpressionCompleted.Style.Fill.BackgroundColor.Color = Color.Red;
                formatExpressionCompleted.Style.Font.Bold = true;

                //string excelName = string.Format("Status report {0}", SrcFileName);

                string path = $"{destinationDirectory}{reportFileName}";
                Stream stream = File.Create(path);
                excel.SaveAs(stream);
                stream.Close();
            }
        }
    }
}
