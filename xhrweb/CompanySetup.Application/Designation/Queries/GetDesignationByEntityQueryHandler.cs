using CompanySetup.Application.Designation.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Designation.Queries
{
    public class GetDesignationByEntityQueryHandler : IRequestHandler<Queries.GetDesignationByEntity, List<DesignationVM>>
    {
        public GetDesignationByEntityQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<List<DesignationVM>> Handle(Queries.GetDesignationByEntity request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.Getdesignationbyentity('{request.EntityId}')";

            var data = await _connection.QueryAsync<DesignationVM>(query);
            return data.OrderBy(x => x.SortOrder).ToList();
        }
    }
}
