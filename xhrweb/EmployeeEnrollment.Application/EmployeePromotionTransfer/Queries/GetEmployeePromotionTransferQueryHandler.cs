using EmployeeEnrollment.Application.EmployeePromotionTransfer.Queries.Models;
using MediatR;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeePromotionTransfer.Queries
{
    public class GetEmployeePromotionTransferQueryHandler : IRequestHandler<Queries.GetEmployeePromotionTransfer, EmployeePromotionTransferVM>
    {
        public GetEmployeePromotionTransferQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeePromotionTransferVM>
            Handle(Queries.GetEmployeePromotionTransfer request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee. GetEmployeePromotionTransferById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<EmployeePromotionTransferVM>
                (query);
            return data;
        }
    }
}

