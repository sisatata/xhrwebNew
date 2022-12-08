using PayrollManagement.Application.BenefitBillClaim.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace PayrollManagement.Application.BenefitBillClaim.Queries
{
    public class GetBenefitBillClaimReportDataByCompanyQueryHandler : IRequestHandler<Queries.GetBenefitBillClaimReportDataByCompany, List<ReportBenefitBillClaimVM>>
    {
        public GetBenefitBillClaimReportDataByCompanyQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<ReportBenefitBillClaimVM>> Handle(Queries.GetBenefitBillClaimReportDataByCompany request, CancellationToken cancellationToken)
        {
            DateTime format = (DateTime)request.EndTime;
              string endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartTime;
            string startDate = format.ToString("yyyy-MM-dd");

            var query = "";
            if (request.EmployeeId == Guid.Empty || request.EmployeeId== null)
            {
                query = $"SELECT * from payroll.RPTGetBillClaimData('{request.CompanyId}',null,'{startDate}','{endDate}')";
            }
            else
            {
                query = $"SELECT * from payroll.RPTGetBillClaimData('{request.CompanyId}','{request.EmployeeId}','{startDate}','{endDate}')";
            }
            //var query = $"SELECT * from payroll.RPTGetBillClaimData('{request.CompanyId}','{startDate}','{endDate}')";

            var data = await _connection.QueryAsync<ReportBenefitBillClaimVM>(query);
            return data.ToList();
        }
    }
}
