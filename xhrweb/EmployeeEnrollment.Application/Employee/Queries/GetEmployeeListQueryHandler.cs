using EmployeeEnrollment.Application.Employee.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ASL.Hrms.SharedKernel.Interfaces;

namespace EmployeeEnrollment.Application.Employee.Queries
{
    public class GetEmployeeListQueryHandler : IRequestHandler<Queries.GetEmployeeList, List<EmployeeVM>>
    {
        public GetEmployeeListQueryHandler(DbConnection connection,
            ICurrentUserContext userContext)
        {
            _connection = connection;
            _userContext = userContext;
        }
        private readonly DbConnection _connection;
        private readonly ICurrentUserContext _userContext;
        public async Task<List<EmployeeVM>> Handle(Queries.GetEmployeeList request, CancellationToken cancellationToken)
        {
            // need to extract only full name and id from db
            string companyId = _userContext.CurrentUserCompanyId;
            var query = $"SELECT * from employee.GetEmployeeListByCompany('{companyId}')"; ;
            try
            {
                var data = await _connection.QueryAsync<EmployeeVM>(query);
                return data.OrderBy(x=>x.FullName).ToList();
            }
            catch (System.Exception ex)
            {  

                throw ex;
            }
            
        }
    }
}






