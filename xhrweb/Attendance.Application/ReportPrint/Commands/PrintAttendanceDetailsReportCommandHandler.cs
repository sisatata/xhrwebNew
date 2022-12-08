using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Services;
using ASL.Utility.ExtensionMethods;
using Attendance.Application.ReportPrint.Model;
using Attendance.Core.Entities;
using Attendance.Core.Interfaces;
using Attendance.Core.Specifications;
using Dapper;
using EmployeeEnrollment.Application.Employee.Queries.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Attendance.Application.ReportPrint.Commands
{
    public class PrintAttendanceDetailsReportCommandHandler : IRequestHandler<PrintAttendanceDetailsReportCommand, string>
    {
        #region ctor
        public PrintAttendanceDetailsReportCommandHandler(DbConnection connection, IConfiguration configuration,
            IReferenceRepository<Core.Entities.Department, Guid> repositoryDept, IReferenceRepository<Core.Entities.Designation, Guid> repositoryDesig,
            IUriComposer uriComposer,
            IReferenceRepository<Core.Entities.Branch, Guid> repositoryBranch
            )
        {
            _connection = connection;
            _configuration = configuration;
            _repositoryDesig = repositoryDesig;
            _repositoryDept = repositoryDept;
            _repositoryBranch = repositoryBranch;
            _uriComposer = uriComposer;
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
        public async Task<string> Handle(PrintAttendanceDetailsReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
                var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
                var query = "";
                var format = (DateTime)request.EndDate;
                var endDate = format.ToString("yyyy-MM-dd");
                format = (DateTime)request.StartDate;
                var startDate = format.ToString("yyyy-MM-dd");
                if (request.EmployeeIds == null || request.EmployeeIds.Count == 0)
                {
                    query = $"SELECT * from employee.GetEmployeeListByCompany('{request.CompanyId}')";
                    var data = await _connection.QueryAsync<EmployeeVM>(query);
                    // data = data.OrderByDescending(x => x.DepartmentId).ThenByDescending(x => x.EmployeeId);
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
                var departments = await _repositoryDept.ListAsync(queryDept);
                var queryDesig = new DesignationFilterSpecification();
                var designations = await _repositoryDesig.ListAsync(queryDesig);
                var queryBranch = new BranchFilterSpecification(request.CompanyId);
                var branches = await _repositoryBranch.ListAsync(queryBranch);
                query = $"SELECT * from attendance.RPTAttendanceDetailsByCompanyAndDateRange('{request.CompanyId}','{startDate}','{endDate}',{request.Type})";
                var queryResult = await _connection.QueryAsync<ReportAttendaceDetailsPdfVM>(query);
                var res = (from q in queryResult
                           join emp in request.EmployeeIds on q.Id equals emp
                           join dep in departments on q.DepartmentId equals dep.Id
                           join des in designations on q.DesignationId equals des.Id
                           join br in branches on q.BranchId equals br.Id
                           select new ReportAttendaceDetailsPdfVM
                           {
                               EmployeeId = q.EmployeeId,
                               DateString = q.DateString,
                               InTime = q.InTime,
                               OutTime = q.OutTime,
                               OfficeInTime = q.OfficeInTime,
                               OfficeOutTime = q.OfficeOutTime,
                               Late = q.Late,
                               ShiftCode = q.ShiftCode,
                               Status = q.Status,
                               CompanyName = q.CompanyName,
                               Branch = br.BranchName,
                               FullName = q.FullName,
                               Department = dep.DepartmentName,
                               Designation = des.DesignationName,
                               JoiningDateString = q.JoiningDateString,
                               Remarks = q.Remarks,
                               ReportTitle = q.ReportTitle,
                               DesignationOrder = q.DesignationOrder
                           }
                    ).OrderBy(x => x.Branch).ThenBy(x => x.Department).ThenBy(x => x.DesignationOrder).ThenBy(x => x.FullName);
                var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
                var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery);

                var logoQuery = $"SELECT * from main.getcompanylogouri('{request.CompanyId}')";
                var companyLogoUri = await _connection.QueryAsync<ASL.Hrms.SharedKernel.Models.CompanyLogoUriVM>(logoQuery);
                var companyLogoUriData = companyLogoUri?.FirstOrDefault();

                var consolidatedChildren = from c in res
                                           group c by new { c.CompanyName, c.ReportTitle } into gcs
                                           select new GroupedAttendanceDetailsPdfVM()
                                           {
                                               CompanyName = companyData.Count() == 0 ? gcs.Key.CompanyName : companyData.FirstOrDefault().CompanyName,
                                               Address = companyData.Count() == 0 ? "" : companyData.FirstOrDefault().CompanyAddress,
                                               DataList = gcs.ToList(),
                                               ReportTitle = gcs.Key.ReportTitle,
                                               StartDate = request.StartDate.Value.ToString("dd/MM/yyy"),
                                               EndDate = request.EndDate.Value.ToString("dd/MM/yyy"),
                                           };
                var consolidatedChildrenList = consolidatedChildren.FirstOrDefault();

                if (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && res != null)
                {

                    consolidatedChildrenList.DataList[0].CompanyLogoUri = _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri.ToString());
                }
                var docName = $"Attendance_Detail_Report_{DateTime.Now.ConvertToUnixEpochInSecond()}.pdf";// DateTime.Now.ToString("yyyyMMdd_HHmmss")
                var nn = consolidatedChildrenList;
                var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintAttendanceDetailsReport.cshtml", consolidatedChildrenList);
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
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }
        }
        #endregion
    }
}
