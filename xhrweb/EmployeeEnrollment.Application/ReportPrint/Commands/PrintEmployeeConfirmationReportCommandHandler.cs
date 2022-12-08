using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using ASL.Hrms.SharedKernel.Services;
using Dapper;
using EmployeeEnrollment.Application.ReportPrint.Model;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.ReportPrint.Commands
{
    public class PrintEmployeeConfirmationReportCommandHandler : IRequestHandler<Commands.V1.PrintEmployeeConfirmationReport, string>
    {
        #region ctor
        public PrintEmployeeConfirmationReportCommandHandler(DbConnection connection, IConfiguration configuration, IUriComposer uriComposer)
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
        public async Task<string> Handle(Commands.V1.PrintEmployeeConfirmationReport request, CancellationToken cancellationToken)
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
                if (request.EmployeeId == null || request.EmployeeId == Guid.Empty)
                    query = $"SELECT * from employee.RPTEmployeeConfirmationByCompany('{request.CompanyId}','{startDate}','{endDate}',null)";
                else
                    query = $"SELECT * from employee.RPTEmployeeConfirmationByCompany('{request.CompanyId}','{startDate}','{endDate}','{request.EmployeeId}')";
                var data = await _connection.QueryAsync<ReportEmployeeConfirmation>(query);
                var oData = data.OrderBy(x => x.BranchName).ThenBy(x => x.DepartmentName).ThenBy(x => x.DesignationOrder).ThenBy(x => x.FullName);
                var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
                var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery);
                var logoQuery = $"SELECT * from main.getcompanylogouri('{request.CompanyId}')";
                var companyLogoUri = await _connection.QueryAsync<ASL.Hrms.SharedKernel.Models.CompanyLogoUriVM>(logoQuery);
                var companyLogoUriData = companyLogoUri?.FirstOrDefault();

                var consolidatedChildren = from c in oData
                                           group c by new { c.CompanyName } into gcs
                                           select new GroupedReportEmployeeConfirmation()
                                           {
                                               CompanyName = companyData.Count() == 0 ? gcs.Key.CompanyName : companyData.FirstOrDefault().CompanyName,
                                               Address = companyData.Count() == 0 ? "" : companyData.FirstOrDefault().CompanyAddress,
                                               DataList = gcs.ToList(),
                                               StartDate = request.StartDate.Value.ToString("dd/MM/yyy"),
                                               EndDate = request.EndDate.Value.ToString("dd/MM/yyy"),
                                               CompanyLogoUri = (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && oData != null) == true ? _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri) : null

                                           };

                var employeeConfirmationmentSheet = consolidatedChildren.ToList().FirstOrDefault();
                var docName = string.Format("Confirmation_Rep_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss"));


                var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintEmployeeConfirmationReport.cshtml", employeeConfirmationmentSheet);

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
            catch(Exception ex)
            {
                throw new  ArgumentException(ex.ToString());
            }
        }
    }
}
