using PayrollManagement.Application.PaymentOption.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.PaymentOption.Queries
{
    public class GetPaymentOptionQueryHandler : IRequestHandler<Queries.GetPaymentOption, PaymentOptionVM>
    {
        public GetPaymentOptionQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<PaymentOptionVM>
            Handle(Queries.GetPaymentOption request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetPaymentOptionById ({request.Id})";

            var data = await _connection.QueryFirstAsync<PaymentOptionVM>
                (query);
            return data;
        }
    }
}

