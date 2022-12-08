using CompanySetup.Application.Notification.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Notification.Queries
{
    public class GetPendingNotificationListToPushQueryHandler : IRequestHandler<Queries.GetPendingNotificationListToPush, List<NotificationVM>>
    {
        public GetPendingNotificationListToPushQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<NotificationVM>> Handle(Queries.GetPendingNotificationListToPush request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetPendingNotificationListToPush()"; ;

            var data = await _connection.QueryAsync<NotificationVM>(query);
            return data.ToList();
        }
    }
}
