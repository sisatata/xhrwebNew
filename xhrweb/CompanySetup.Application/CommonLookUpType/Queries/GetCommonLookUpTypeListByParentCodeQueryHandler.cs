using ASL.Hrms.SharedKernel.Interfaces;
using CompanySetup.Application.CommonLookUpType.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.CommonLookUpType.Queries
{
    public class GetCommonLookUpTypeListByParentCodeQueryHandler : IRequestHandler<Queries.GetCommonLookUpTypeListByParentCode, List<CommonLookUpTypeVM>>
    {
        public GetCommonLookUpTypeListByParentCodeQueryHandler(DbConnection connection,
            ICurrentUserContext userContext)
        {
            _connection = connection;
            _userContext = userContext;
        }
        private readonly DbConnection _connection;
        private readonly ICurrentUserContext _userContext;

        public async Task<List<CommonLookUpTypeVM>> Handle(Queries.GetCommonLookUpTypeListByParentCode request, CancellationToken cancellationToken)
        {
            request.CompanyId = new Guid(_userContext.CurrentUserCompanyId);
            var query = $"SELECT * from main.getcommonlookuptypebyparentcode ('{request.CompanyId}','{request.ParentCode}')";

            var data = await _connection.QueryAsync<CommonLookUpTypeVM>(query);
            return data.ToList();
        }
    }
}

