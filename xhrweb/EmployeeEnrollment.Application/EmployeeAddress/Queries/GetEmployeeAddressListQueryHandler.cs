using EmployeeEnrollment.Application.EmployeeAddress.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeAddress.Queries
{
    public class GetEmployeeAddressListQueryHandler : IRequestHandler<Queries.GetEmployeeAddressList, List<EmployeeAddressVM>>
    {
        public GetEmployeeAddressListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeAddressVM>> Handle(Queries.GetEmployeeAddressList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.getemployeeaddressbyemployee('{request.EmployeeId}')"; ;
            try
            {
                var data = await _connection.QueryAsync<EmployeeAddressVM>(query);
                return data.ToList();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
