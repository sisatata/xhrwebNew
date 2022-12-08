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
    public class GetAllCompanyByApprovalStatusQueryHandler : IRequestHandler<Queries.GetAllCompanyByApprovalStatus, List<CompanyVM>>
    {
        public GetAllCompanyByApprovalStatusQueryHandler(DbConnection connection, ICurrentUserContext userContext)
        {
            _connection = connection;
            _userContext = userContext;
        }

        private readonly DbConnection _connection;
        private readonly ICurrentUserContext _userContext;
        public async Task<List<CompanyVM>> Handle(Queries.GetAllCompanyByApprovalStatus request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetAllCompanyByApprovalStatus('{request.ApprovalStatus}')";

            var data = await _connection.QueryAsync<CompanyVM>(query);
            return data.ToList();
        }
    }
}
