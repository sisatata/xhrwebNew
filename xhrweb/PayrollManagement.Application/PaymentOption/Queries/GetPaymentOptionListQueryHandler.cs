using PayrollManagement.Application.PaymentOption.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.PaymentOption.Queries
{
    public class GetPaymentOptionListQueryHandler : IRequestHandler<Queries.GetPaymentOptionList, List<PaymentOptionVM>>
    {
        public GetPaymentOptionListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<PaymentOptionVM>> Handle(Queries.GetPaymentOptionList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetPaymentOptionByCompany ('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<PaymentOptionVM>(query);
            return data.ToList();
        }
    }
}
