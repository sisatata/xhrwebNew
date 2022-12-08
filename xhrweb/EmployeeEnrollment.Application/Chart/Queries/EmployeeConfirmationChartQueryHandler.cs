using Dapper;
using EmployeeEnrollment.Application.Chart.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.Chart.Queries
{
    public class EmployeeConfirmationChartQueryHandler : IRequestHandler<Queries.EmployeeConfiramtionChart, List<EmployeeConfirmationChartVM>>
    {
        public EmployeeConfirmationChartQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;
        public async Task<List<EmployeeConfirmationChartVM>> Handle(Queries.EmployeeConfiramtionChart request, CancellationToken cancellationToken)
        {
            DateTime format = (DateTime)request.EndDate;
            string endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartDate;
            string startDate = format.ToString("yyyy-MM-dd");
            var query = $"SELECT * from employee. ChartEmployeeConfirmationByCompany ('{request.CompanyId}','{startDate}','{endDate}')";
            var data = await _connection.QueryAsync<EmployeeConfirmationChartVM>(query);
            return data.ToList();


        }
    }
}
