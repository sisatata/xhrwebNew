using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using ASL.Hrms.SharedKernel.Services;
using Dapper;
using EmployeeEnrollment.Application.ReportPrint.Model;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace EmployeeEnrollment.Application.ReportPrint.Commands
{
    public class PrintEmployeeEnrollmentReportCommandHandler : IRequestHandler<Commands.V1.PrintEmployeeEnrollmentReport, string>
    {
        #region ctor
        public PrintEmployeeEnrollmentReportCommandHandler(DbConnection connection, IConfiguration configuration, IUriComposer uriComposer)
        {
            _connection = connection;
            _configuration = configuration;
            _uriComposer = uriComposer;
        }
        #endregion
        #region properties
        private readonly DbConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly IUriComposer _uriComposer;
        #endregion
        #region methods
        public async Task<string> Handle(Commands.V1.PrintEmployeeEnrollmentReport request, CancellationToken cancellationToken)
        {
            try
            {
                var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
                var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
                var query = "";
                DateTime format = (DateTime)request.EndDate;
                string endDate = format.ToString("yyyy-MM-dd");
                format = (DateTime)request.StartDate;
                string startDate = format.ToString("yyyy-MM-dd");
                if (request.Type == (int?)EnrollementTypes.Join)
                {
                    query = $"SELECT * from employee.RPTEmployeeJoinByCompany('{request.CompanyId}','{startDate}','{endDate}')";
                }
                else
                {
                    query = $"SELECT * from employee.RPTEmployeeQuitByCompany('{request.CompanyId}','{startDate}','{endDate}')";
                }
                var data = await _connection.QueryAsync<ReportEmployeeEnrollmentPdfVM>(query);
                var oData = data.OrderBy(x => x.BranchName).ThenBy(x => x.DepartmentName).ThenBy(x => x.DesigantionOrder).ThenBy(x => x.FullName);
                // add photo
                foreach (var item in oData)
                {
                    if (item != null && !string.IsNullOrWhiteSpace(item.EmployeeImageUri))
                    {
                        item.EmployeeImageUri = _uriComposer.ComposeProfilePicUri(item.EmployeeImageUri);
                    }
                    else if (item != null)
                    {
                        if (item.LookUpTypeName == "Male")
                            item.EmployeeImageUri = _uriComposer.ComposeProfilePicUri("placeholder-male.jpg");
                        else
                            item.EmployeeImageUri = _uriComposer.ComposeProfilePicUri("placeholder-female.jpg");
                    }
                }
                var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
                var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery);

                var logoQuery = $"SELECT * from main.getcompanylogouri('{request.CompanyId}')";
                var companyLogoUri = await _connection.QueryAsync<ASL.Hrms.SharedKernel.Models.CompanyLogoUriVM>(logoQuery);
                var companyLogoUriData = companyLogoUri?.FirstOrDefault();
                var consolidatedChildren = from c in oData
                                       group c by new { c.CompanyName } into gcs
                                       select new GroupedReportEmployeeEnrollmentPdfVM()
                                       {
                                           CompanyName = companyData.Count() == 0 ? gcs.Key.CompanyName : companyData.FirstOrDefault().CompanyName,
                                           CompanyLogoUri = (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && oData != null) == true ? _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri) : null,
                                           Address = companyData.Count() == 0 ? "" : companyData.FirstOrDefault().CompanyAddress,
                                           ReportTitle = request.Type == (int?) EnrollementTypes.Join ?"New Joining Employees From ":"Employees Resign From ",
                                           DataList = gcs.ToList(),
                                           StartDate = request.StartDate.Value.ToString("dd/MM/yyy"),
                                           EndDate = request.EndDate.Value.ToString("dd/MM/yyy"),
                                           };
                var employeeEnrollmentSheet = consolidatedChildren.ToList().FirstOrDefault();
                var docName = string.Format("Join_Rep_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintEmployeeEnrollmentReport.cshtml", employeeEnrollmentSheet);
                ReportService reportService = new ReportService()
                {
                    OutPath = rootPath + dataFilePath + docName,
                    StylePath = Path.Combine(Directory.GetCurrentDirectory(), "utility\\assets\\css\\", "reportstyles.css"),
                    HtmlContent = htmlView
                };
                request.Converter.Convert(reportService.PdfDocument);
                string printPath = $"{dataFilePath}{docName}";
                return printPath;
            }
            catch( Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
