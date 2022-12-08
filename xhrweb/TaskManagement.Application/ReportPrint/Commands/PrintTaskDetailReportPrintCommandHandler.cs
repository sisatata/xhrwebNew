using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using ASL.Hrms.SharedKernel.Services;
using ASL.Utility.ExtensionMethods;
using Dapper;
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
using TaskManagement.Application.ReportPrint.Model;
using TaskManagement.Application.TaskDetail.Queries.Models;

namespace TaskManagement.Application.ReportPrint.Commands
{/// <summary>
/// /
/// </summary>
    public class PrintTaskDetailReportPrintCommandHandler : IRequestHandler<PrintTaskDetailReportPrintCommand, string>
    {
        #region ctor
        public PrintTaskDetailReportPrintCommandHandler(DbConnection connection, IConfiguration configuration, IUriComposer uriComposer, ICurrentUserContext currentUserContext)
        {
            _connection = connection;
            _configuration = configuration;
            _uriComposer = uriComposer;
            _currentUserContext = currentUserContext;
        }
        #endregion

        #region properties
        private readonly DbConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly IUriComposer _uriComposer;
        private readonly ICurrentUserContext _currentUserContext;

        #endregion

        #region methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> Handle(PrintTaskDetailReportPrintCommand request, CancellationToken cancellationToken)
        {
            var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
            var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");

            var query = "";
            query = $"SELECT * FROM employee.GetUserIdbyEmployeeId('{request.EmployeeId}')";
            var user = await _connection.QueryAsync<UserVM>(query).ConfigureAwait(false);


            // var userId = _currentUserContext.CurrentUserId==null?request.UserId: _currentUserContext.CurrentUserId;
            DateTime format;
            string endDate = "", startDate = "";

            format = (DateTime)request.EndDate;
            endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartDate;
            startDate = format.ToString("yyyy-MM-dd");


            query = $"SELECT * from task. RPTtaskDetail ('{request.CompanyId}', '{request.EmployeeId}','{user.FirstOrDefault().UserId}','{startDate}','{endDate}')";

            var data = await _connection.QueryAsync<ReportTaskDetailPdfVM>(query);
            var dataList = data.OrderBy(x=>x.BranchName).ThenBy(x=>x.DepartmentName).ThenBy(x=>x.SortOrder).ThenBy(x=>x.FullName);
            foreach (var item in dataList)
            {

                if (item.CreatedBy != null)
                {
                    query = $"SELECT * from employee.GetEmployeeInformationbyUserId ('{item.CreatedBy}')";
                    var assignedBy = await _connection.QueryFirstAsync<UserVM>(query);
                    item.AssignedBy = assignedBy.EmployeeName;
                    item.AssignedById = assignedBy.EmployeeId;


                }
                else if (item.TaskCreator != null || item.TaskCreator != Guid.Empty)
                {
                    var taskCreator = item.TaskCreator.ToString();
                    query = $"SELECT * from employee.GetEmployeeInformationbyUserId ('{taskCreator}')";
                    var assignedBy = await _connection.QueryFirstAsync<UserVM>(query);
                    item.AssignedBy = assignedBy.EmployeeName;
                    item.AssignedById = assignedBy.EmployeeId;
                }


            }

            var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
            var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery);

            var logoQuery = $"SELECT * from main.GetCompanyLogoUri('{request.CompanyId}')";
            var companyLogoUri = await _connection.QueryAsync<ASL.Hrms.SharedKernel.Models.CompanyLogoUriVM>(logoQuery);
            var companyLogoUriData = companyLogoUri?.FirstOrDefault();
            var companyLogo = "";
            if (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && dataList != null && dataList.Count() > 0)
            {

                companyLogo = _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri.ToString());
            }
            var consolidatedChildren = from c in dataList
                                       group c by new { c.CompanyId } into gcs
                                       select new GroupedTaskDetailPdfVM()
                                       {
                                           CompanyName = companyData.FirstOrDefault().CompanyName,
                                           Address = companyData.Count() == 0 ? "" : companyData.FirstOrDefault().CompanyAddress,
                                           DataList = gcs.ToList(),
                                           CompanyLogoUri = companyLogo,
                                           StartDate = request.StartDate.Value.ToString("dd/MM/yyy"),
                                           EndDate = request.EndDate.Value.ToString("dd/MM/yyy"),
                                       };

            //consolidatedChildren.FirstOrDefault().DataList.OrderBy(x => x.BranchName).ThenBy(x => x.DepartmentName).ThenBy(x => x.SortOrder).ThenBy(x => x.FullName);
            var docName = $"Task_Detail_Report_{DateTime.Now.ConvertToUnixEpochInSecond()}.pdf";// DateTime.Now.ToString("yyyyMMdd_HHmmss")

            var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintTaskDetailReport.cshtml", consolidatedChildren.FirstOrDefault());
            ReportService reportService = new ReportService()
            {
                OutPath = rootPath + dataFilePath + docName,
                StylePath = Path.Combine(Directory.GetCurrentDirectory(), "utility\\assets\\css\\", "reportstyles.css"),
                HtmlContent = htmlView,

            };
            request.Converter.Convert(reportService.PdfDocument);
            string printPdfPath = $"{dataFilePath}{docName}";
            return printPdfPath;

        }
        #endregion
    }
}
