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
    public class GetNotificationListByOwnerQueryHandler : IRequestHandler<Queries.GetNotificationListByOwner, List<NotificationVM>>
    {
        public GetNotificationListByOwnerQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<NotificationVM>> Handle(Queries.GetNotificationListByOwner request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetNotificationByOwner('{request.NotificationOwnerId}')"; ;

            var data = await _connection.QueryAsync<NotificationVM>(query);
            return data.ToList();
        }
    }
}
