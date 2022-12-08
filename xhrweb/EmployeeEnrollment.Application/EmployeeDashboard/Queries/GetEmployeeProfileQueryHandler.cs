using ASL.Hrms.SharedKernel.Interfaces;
using Dapper;
using EmployeeEnrollment.Application.EmployeeDashboard.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static EmployeeEnrollment.Application.EmployeeAddress.Queries.Queries;
using static EmployeeEnrollment.Application.EmployeeEmail.Queries.Queries;
using static EmployeeEnrollment.Application.EmployeeManager.Queries.Queries;
using static EmployeeEnrollment.Application.EmployeePhone.Queries.Queries;

namespace EmployeeEnrollment.Application.EmployeeDashboard.Queries
{
    public class GetEmployeeProfileQueryHandler : IRequestHandler<Queries.GetEmployeeProfileById, EmployeeProfileVM>
    {
        public GetEmployeeProfileQueryHandler(DbConnection connection, IMediator mediator, IUriComposer uriComposer)
        {
            _connection = connection;
            _mediator = mediator;
            _uriComposer = uriComposer;
        }
        private readonly DbConnection _connection;
        private readonly IMediator _mediator;
        private readonly IUriComposer _uriComposer;
        public async Task<EmployeeProfileVM> Handle(Queries.GetEmployeeProfileById request, CancellationToken cancellationToken)
        {
            // EmployeeProfileVM employeeProfileVM = new EmployeeProfileVM();
            try
            {


                var query = $"SELECT * from  employee.GetEmployeeProfileByEmployeeId('{request.EmployeeId}')";

                var employeeProfileVM = await _connection.QueryFirstOrDefaultAsync<EmployeeProfileVM>(query);

                // Combining real path
                if (employeeProfileVM != null && !string.IsNullOrWhiteSpace(employeeProfileVM.EmployeeImageUri))
                {
                    employeeProfileVM.EmployeeImageUri = _uriComposer.ComposeProfilePicUri(employeeProfileVM.EmployeeImageUri);
                }

                var addressQuery = new GetEmployeeAddressList
                {
                    EmployeeId = request.EmployeeId
                };
                var addressList = await _mediator.Send(addressQuery);
                employeeProfileVM.EmployeeAddressList = addressList;

                var managerQuery = new GetEmployeeManagerList
                {
                    EmployeeId = request.EmployeeId
                };
                var managerList = await _mediator.Send(managerQuery);
                employeeProfileVM.EmployeeManagerList = managerList;

                var emailQuery = new GetEmployeeEmailListByEmployeeId
                {
                    EmployeeId = request.EmployeeId
                };
                var emailList = await _mediator.Send(emailQuery);
                employeeProfileVM.EmployeeEmailList = emailList;

                var phoneQuery = new GetEmployeePhoneList
                {
                    EmployeeId = request.EmployeeId
                };
                var phoneList = await _mediator.Send(phoneQuery);
                employeeProfileVM.EmployeePhoneList = phoneList;

                return employeeProfileVM;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
