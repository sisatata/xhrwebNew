using PayrollManagement.Application.EmployeeBankAccount.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.EmployeeBankAccount.Queries
{
    public class GetEmployeeBankAccountListQueryHandler : IRequestHandler<Queries.GetEmployeeBankAccountList, List<EmployeeBankAccountVM>>
    {
        public GetEmployeeBankAccountListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeBankAccountVM>> Handle(Queries.GetEmployeeBankAccountList request, CancellationToken cancellationToken)
        {
            try
            {
                var query = $"SELECT * from payroll.GetEmployeeBankAccountByCompany('{request.CompanyId}')"; ;

                var data = await _connection.QueryAsync<EmployeeBankAccountVM>(query);
                return data.ToList();
            }
            catch (System.Exception ex)
            {

                throw ex;  

            }
            
        }
    }
}
