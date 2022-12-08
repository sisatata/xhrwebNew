using CompanySetup.Application.Notification.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace CompanySetup.Application.Notification.Queries
{
    public class GetNotificationSummaryByOwnerQueryHandler : IRequestHandler<Queries.GetNotificationSummaryByOwner, NotificationSummary>
    {
        public GetNotificationSummaryByOwnerQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<NotificationSummary> Handle(Queries.GetNotificationSummaryByOwner request, CancellationToken cancellationToken)
        {
            NotificationSummary oModel = new NotificationSummary();

            var query = $"SELECT * from main.GetNotificationByOwner('{request.NotificationOwnerId}')"; ;

            var data = await _connection.QueryAsync<NotificationVM>(query);
            oModel.NotificationList = data.ToList();

            oModel.ReadCount = data == null ? (short)0 : (short)data.ToList().Count(x => x.IsViewed == true);
            oModel.UnReadCount = data == null ? (short)0 : (short)data.ToList().Count(x => x.IsViewed == false);
            return oModel;
        }
    }
}

