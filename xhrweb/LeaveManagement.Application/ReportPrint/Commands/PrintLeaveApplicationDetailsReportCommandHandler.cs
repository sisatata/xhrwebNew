using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using ASL.Hrms.SharedKernel.Services;
using ASL.Utility.ExtensionMethods;
using Dapper;
using EmployeeEnrollment.Application.Employee.Queries.Models;
using LeaveManagement.Application.ReportPrint.Model;
using LeaveManagement.Core.Interfaces;
using LeaveManagement.Core.Specifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace LeaveManagement.Application.ReportPrint.Commands
{
    public class PrintLeaveApplicationDetailsReportCommandHandler : IRequestHandler<PrintLeaveApplicationDetailsReportCommand, string>
    {
        #region ctor
        public PrintLeaveApplicationDetailsReportCommandHandler(DbConnection connection, IConfiguration configuration, IReferenceRepository<Core.Entities.Department, Guid> repositoryDept,
            IReferenceRepository<Core.Entities.Designation, Guid> repositoryDesig,
            IUriComposer uriComposer,
            IReferenceRepository<Core.Entities.Branch, Guid> repositoryBranch
            )
        {
            _connection = connection;
            _configuration = configuration;
            _repositoryBranch = repositoryBranch;
            _uriComposer = uriComposer;
            _repositoryDept = repositoryDept;
            _repositoryDesig = repositoryDesig;
        }
        #endregion
        #region properties
        private readonly DbConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly IUriComposer _uriComposer;
        private readonly IReferenceRepository<Core.Entities.Department, Guid> _repositoryDept;
        private readonly IReferenceRepository<Core.Entities.Designation, Guid> _repositoryDesig;
        private readonly IReferenceRepository<Core.Entities.Branch, Guid> _repositoryBranch;
        #endregion
        #region methods
        public async Task<string> Handle(PrintLeaveApplicationDetailsReportCommand request, CancellationToken cancellationToken)
        {
            var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
            var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
            var query = "";


            var format = (DateTime)request?.EndDate;

            var endDate = format.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            format = (DateTime)request.StartDate;
            var startDate = format.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            if (request.EmployeeIds == null || request.EmployeeIds.Count == 0)
            {
                query = $"SELECT * from employee.GetEmployeeListByCompany('{request.CompanyId}')";
                var data = await _connection.QueryAsync<EmployeeVM>(query).ConfigureAwait(false);
                if (request.BranchIds != null && request.BranchIds.Count > 0)
                {
                    data = data.Where(r => request.BranchIds.Contains(r.BranchId.Value));
                }
                if (request.DepartmentIds != null && request.DepartmentIds.Count > 0)
                {
                    data = data.Where(r => request.DepartmentIds.Contains(r.DepartmentId.Value));
                }
                if (request.DesignationIds != null && request.DesignationIds.Count > 0)
                {
                    data = data.Where(r => request.DesignationIds.Contains(r.PositionId.Value));
                }
                request.EmployeeIds = data.Select(x => x.Id).ToList();
            }
            var queryDept = new DepartmentFilterSpecification(request.CompanyId);
            var departments = await _repositoryDept.ListAsync(queryDept).ConfigureAwait(false);
            var queryDesig = new DesignationFilterSpecification();
            var designations = await _repositoryDesig.ListAsync(queryDesig).ConfigureAwait(false);
            var queryBranch = new BranchFilterSpecification(request.CompanyId);
            var branches = await _repositoryBranch.ListAsync(queryBranch).ConfigureAwait(false);
            query = $"SELECT * from leave.RPTGetLeaveApplicationDetailsByCompany('{request.CompanyId}','{startDate}','{endDate}','{request.ApprovalStatusText}')";

            query = query.Replace("'null'", "null", StringComparison.InvariantCultureIgnoreCase);
            query = query.Replace("''", "null", StringComparison.InvariantCultureIgnoreCase);
            var queryResult = await _connection.QueryAsync<ReportLeaveApplicationDetailsPdfVM>(query).ConfigureAwait(false);
            var res = (from q in queryResult
                       join emp in request.EmployeeIds on q.EID equals emp
                       join dep in departments on q.DepartmentId equals dep.Id
                       join des in designations on q.DesignationId equals des.Id
                       select new ReportLeaveApplicationDetailsPdfVM
                       {
                           EmployeeId = q.EmployeeId,
                           CompanyName = q.CompanyName,
                           FullName = q.FullName,
                           BranchName = q.BranchName,
                           DepartmentName = dep.DepartmentName,
                           DesignationName = des.DesignationName,
                           JoiningDateString = q.JoiningDateString,
                           StartDate = q.StartDate,
                           EndDate = q.EndDate,
                           LeaveDays = q.LeaveDays,
                           Reason = q.Reason,
                           ApplyDate = q.ApplyDate,
                           LeaveTypeName = q.LeaveTypeName,
                           Notes = q.Notes,
                           Balance = q.Balance,
                           RemainingBalance = q.RemainingBalance,
                           ApprovalStatusText = q.ApprovalStatusText,
                           ApprovalStatus = q.ApprovalStatus,
                           AddressOnLeave = q.AddressOnLeave,
                           DesignationOrder = q.DesignationOrder,
                           EID = q.EID
                       }
                ).OrderBy(x => x.BranchName).ThenBy(x => x.DepartmentName).ThenBy(x => x.DesignationOrder).ThenBy(x => x.FullName);
            var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
            var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery).ConfigureAwait(false);
            request.ApprovalStatusText ??= ((int)ApprovalStatuses.All).ToString(CultureInfo.InvariantCulture);
            var logoQuery = $"SELECT * from main.getcompanylogouri('{request.CompanyId}')";
            var companyLogoUri = await _connection.QueryAsync<ASL.Hrms.SharedKernel.Models.CompanyLogoUriVM>(logoQuery).ConfigureAwait(false);
            var companyLogoUriData = companyLogoUri?.FirstOrDefault();
            var consolidatedChildren = from c in res
                                       group c by new { c.CompanyName } into gcs
                                       select new GroupedReportLeaveApplicationDetailsPdfVM()
                                       {

                                           CompanyName = !companyData.Any() ? gcs.Key.CompanyName : companyData.FirstOrDefault().CompanyName,
                                           CompanyAddress = !companyData.Any() ? "" : companyData.FirstOrDefault().CompanyAddress,
                                           CompanyLogoUri = (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && res != null) == true ? _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri) : null,
                                           DataList = gcs.ToList(),
                                           StartDate = request.StartDate.Value.ToString("dd/MM/yyy", CultureInfo.InvariantCulture),
                                           ReportTitle = (int)ApprovalStatuses.Approved == int.Parse(request.ApprovalStatusText, CultureInfo.InvariantCulture) ? "Approved" : (int)ApprovalStatuses.InProgress == int.Parse(request.ApprovalStatusText, CultureInfo.InvariantCulture) ? "In Progress" :
                                                        (int)ApprovalStatuses.Declined == int.Parse(request.ApprovalStatusText, CultureInfo.InvariantCulture) ? "Declined" : (int)ApprovalStatuses.Pending == int.Parse(request.ApprovalStatusText, CultureInfo.InvariantCulture) ? "Pending" : "All",
                                           EndDate = request.EndDate.Value.ToString("dd/MM/yyy", CultureInfo.InvariantCulture),
                                       };

            if (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && consolidatedChildren.Any())
            {

                consolidatedChildren.FirstOrDefault().DataList[0].CompanyLogoUri = _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri);
            }
            var docName = $"Leave_Details_Report_{DateTime.Now.ConvertToUnixEpochInSecond()}.pdf";// DateTime.Now.ToString("yyyyMMdd_HHmmss")
            var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintLeaveApplicationDetailsReport.cshtml", consolidatedChildren).ConfigureAwait(false);
            ReportService reportService = new ReportService()
            {
                OutPath = $"{rootPath}{dataFilePath}{docName}", //rootPath + dataFilePath + docName,
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
