using Dapper;
using MediatR;
using PayrollManagement.Application.EmployeeBankAccount.Queries.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.EmployeeBankAccount.Queries
{
   public class GetEmployeeBankAccountByEmployee : IRequestHandler<Queries.GetEmployeeBankAccountListByEmployee, List<EmployeeBankAccountVM>>
    {
        public GetEmployeeBankAccountByEmployee(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;
        public async Task<List<EmployeeBankAccountVM>> Handle(Queries.GetEmployeeBankAccountListByEmployee request, CancellationToken cancellationToken)
        {
            try
            {
                var query = $"SELECT * from payroll.GetEmployeeBankAccountByEmployee('{request.CompanyId}','{request.EmployeeId}')"; ;

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
