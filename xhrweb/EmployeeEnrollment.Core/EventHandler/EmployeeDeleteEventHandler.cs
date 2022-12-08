using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.Events;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Core.EventHandler
{
    public class EmployeeDeleteEventHandler : ICommandHandler<EmployeeDeleteEvent>
    {
        public EmployeeDeleteEventHandler(IConfiguration configuration,
           IEmployeeDeletePostService employeeDeletePostService)
        {
            _configuration = configuration;
            _employeeDeletePostService = employeeDeletePostService;


        }
        private readonly IConfiguration _configuration;
        private readonly IEmployeeDeletePostService _employeeDeletePostService;
        public async Task Handle(EmployeeDeleteEvent command)
        {
            await _employeeDeletePostService.DeleteLoginUserByEmployee(command.LoginId);
        }
    }
}
