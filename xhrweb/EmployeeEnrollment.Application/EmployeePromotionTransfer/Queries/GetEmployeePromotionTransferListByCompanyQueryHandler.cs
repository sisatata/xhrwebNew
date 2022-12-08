using EmployeeEnrollment.Application.EmployeePromotionTransfer .Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeePromotionTransfer .Queries
{
    public class GetEmployeePromotionTransferListByCompanyQueryHandler : IRequestHandler< Queries. GetEmployeePromotionTransferListByCompany, List< EmployeePromotionTransferVM>>
    {
        public GetEmployeePromotionTransferListByCompanyQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task< List< EmployeePromotionTransferVM>> Handle(Queries.GetEmployeePromotionTransferListByCompany request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeePromotionTransferByCompanyId ('{request.CompanyId}')";
            var data = await _connection.QueryAsync< EmployeePromotionTransferVM>(query);
            return data.ToList();
        }
    }
}
