using CompanySetup.Application.Designation.Queries.Models;
using Dapper;
using MediatR;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Designation.Queries
{
    public class DesignationByIdQueryHandler : IRequestHandler<Queries.GetDesignationById, DesignationVM>
    {
        public DesignationByIdQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<DesignationVM> Handle(Queries.GetDesignationById request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.Getdesignationbyid('{request.Id}')";

            var data = await _connection.QueryFirstOrDefaultAsync<DesignationVM>(query);
            return data;
        }
    }
}
