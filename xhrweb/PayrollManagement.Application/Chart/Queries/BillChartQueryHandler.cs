using Dapper;
using MediatR;
using PayrollManagement.Application.Chart.Model;
using PayrollManagement.Application.Chart.Queries;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.Chart.Queries
{
   public class BillChartQueryHandler : IRequestHandler<BillChart, ChartBillVM>
    {
        public BillChartQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<ChartBillVM> Handle(BillChart request, CancellationToken cancellationToken)
        {
            
            DateTime format = (DateTime)request.EndDate;
            string endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartDate;
            string startDate = format.ToString("yyyy-MM-dd");
            var query = "";
            query = $"SELECT * from payroll.chartgetbillamount('{request.CompanyId}','{startDate}','{endDate}')";
            var data = await _connection.QueryAsync<ChartBillVM>(query);
            return data.FirstOrDefault() ;
        }
    }
}
