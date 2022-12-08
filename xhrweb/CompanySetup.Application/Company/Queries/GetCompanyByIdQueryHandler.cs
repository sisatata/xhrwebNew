using ASL.Hrms.SharedKernel.Interfaces;
using CompanySetup.Application.Company.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Company.Queries
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<Queries.GetCompanyById, CompanyVOA>
    {
        public GetCompanyByIdQueryHandler(DbConnection connection, IUriComposer uriComposer)
        {
            _connection = connection;
            _uriComposer = uriComposer;
        }

        private readonly DbConnection _connection;
        private readonly IUriComposer _uriComposer;
        public async Task<CompanyVOA> Handle(Queries.GetCompanyById request, CancellationToken cancellationToken)
        {
            try
            {
                var query = $"SELECT * from main.GetCompanyById('{request.CompanyId}')";

                var data = await _connection.QueryFirstOrDefaultAsync<CompanyVOA>(query);
                if (data != null && !string.IsNullOrWhiteSpace(data.CompanyLogoUri))
                {
                    data.CompanyLogoUri = _uriComposer.ComposeCompanyLogoUri(data.CompanyLogoUri);
                }
                return data;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}
