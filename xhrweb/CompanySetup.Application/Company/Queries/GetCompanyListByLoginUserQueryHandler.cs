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
    public class GetCompanyListByLoginUserQueryHandler : IRequestHandler<Queries.GetCompanyListByLoginUser, List<CompanyVM>>
    {
        public GetCompanyListByLoginUserQueryHandler(DbConnection connection, ICurrentUserContext userContext, IUriComposer uriComposer)
        {
            _connection = connection;
            _userContext = userContext;
            _uriComposer = uriComposer;
        }

        private readonly DbConnection _connection;
        private readonly ICurrentUserContext _userContext;
        private readonly IUriComposer _uriComposer;
        public async Task<List<CompanyVM>> Handle(Queries.GetCompanyListByLoginUser request, CancellationToken cancellationToken)
        {
            string companyId = _userContext.CurrentUserCompanyId;
            var query = $"SELECT * from main.GetCompanyByLoginUser('{companyId}')";

            var data = await _connection.QueryAsync<CompanyVM>(query);
            foreach(var item in data)
            {
                if (item != null && !string.IsNullOrWhiteSpace(item.CompanyLogoUri))
                {
                    item.CompanyLogoUri = _uriComposer.ComposeCompanyLogoUri(item.CompanyLogoUri);
                }

            }
            return data.ToList();
        }
    }
}
