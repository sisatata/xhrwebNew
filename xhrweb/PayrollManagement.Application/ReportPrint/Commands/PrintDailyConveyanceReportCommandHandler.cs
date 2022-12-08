using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using ASL.Hrms.SharedKernel.Services;
using ASL.Utility.ExtensionMethods;
using Attendance.Application.ReportPrint.Model;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Application.ReportPrint.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompanyNameAndAddressVM = Attendance.Application.ReportPrint.Model.CompanyNameAndAddressVM;

namespace PayrollManagement.Application.ReportPrint.Commands
{
    public class PrintDailyConveyanceReportCommandHandler : IRequestHandler<PrintDailyConveyanceReportCommand, string>
    {
        public PrintDailyConveyanceReportCommandHandler(IConfiguration configuration, DbConnection connection, IUriComposer uriComposer)
        {
            _connection = connection;
            _configuration = configuration;
            _uriComposer = uriComposer;
            
        }
        private readonly DbConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly IUriComposer _uriComposer;
        public async Task<string> Handle(PrintDailyConveyanceReportCommand request, CancellationToken cancellationToken)
        {
            var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
            var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
            DateTime format = (DateTime)request.EndTime;
            string endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartTime;
            string startDate = format.ToString("yyyy-MM-dd");
            var query = "";
            if (request.EmployeeId == null || request.EmployeeId == Guid.Empty)
            {
                query = $"SELECT * from payroll.RPTGetBillClaimData('{request.CompanyId}',null,'{startDate}','{endDate}')";
            }
            else
            {
                query = $"SELECT * from payroll.RPTGetBillClaimData('{request.CompanyId}','{request.EmployeeId}','{startDate}','{endDate}')";
            }

            var data = await _connection.QueryAsync<ReportBenefitBillClaimPdfVM>(query);

            var oData = data.Where(x => x.ApprovalStatus == "Approved").OrderBy(x => x.Branch).ThenBy(x=>x.Department).ThenBy(x=>x.DesignationOrder).ThenBy(x => x.ApplicantName).ThenBy(x=>x.BillDate).ToList();
           
            foreach (var item in oData)
            {
                if (item != null && !string.IsNullOrWhiteSpace(item.EmployeeSignature))
                {
                    item.EmployeeSignature = _uriComposer.ComposeEmployeeSignatureUri(item.EmployeeSignature);
                }
                if (item != null && !string.IsNullOrWhiteSpace(item.ManagerSignature) && item.ApprovalStatus =="Approved")
                {
                    item.ManagerSignature = _uriComposer.ComposeEmployeeSignatureUri(item.ManagerSignature);
                }
                if(item.ApprovalStatus != "Approved")
                {
                    item.ManagerSignature = "";
                }
            }
            var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
            var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery);
            string logoUri = "";
            query = $"SELECT * from main.GetCompanyLogoUri('{request.CompanyId}')";
            var companyLogoUri = await _connection.QueryAsync<CompanyLogoUriVM>(query);
            var companyLogoUriData = companyLogoUri?.FirstOrDefault();
            if (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && oData.Count() > 0)
            {

                logoUri = _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri.ToString());
            }
            var consolidatedChildren = from c in oData
                                       group c by new { c.ApplicantName, c.BillDate, c.ManagerName } into gcs
                                       select new GroupedBillClaimVM()
                                       {
                                           CompanyAddress = companyData.Count() == 0 ? "" : companyData.FirstOrDefault().CompanyAddress,
                                           ApplicantName = gcs.Key.ApplicantName,
                                           ManagerName = gcs.Key.ManagerName,
                                           BillDate = gcs.Key.BillDate,
                                           CompanyLogoUri = logoUri,
                                           DataList = gcs.ToList(),
                                           TotalAmountInWord = InWordWithinBangla.ConvertNumbertoEnglishWords(Convert.ToInt32(gcs.ToList().Sum(x => x.ApprovedAmount.Value))),
                                       };
            
            var docName = string.Format("Con_Bill_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss"));

          
            var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintDailyConveyanceReport.cshtml", consolidatedChildren);
           

            ReportService reportService = new ReportService()
            {
                OutPath = rootPath + dataFilePath + docName,
                StylePath = Path.Combine(Directory.GetCurrentDirectory(), "utility\\assets\\css\\", "reportstyles.css"),
                HtmlContent = htmlView

            };
                       
            request.Converter.Convert(reportService.PdfDocument);

            string printGiftPath = $"{dataFilePath}{docName}";
            return printGiftPath;
        }
    }
}
