using CompanySetup.Application.Notification.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.Notification.Queries
{
    public class GetNotificationQueryHandler : IRequestHandler<Queries.GetNotification, NotificationVM>
    {
        public GetNotificationQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<NotificationVM>
            Handle(Queries.GetNotification request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetNotificationById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<NotificationVM>
                (query);
            return data;
        }
    }
}

