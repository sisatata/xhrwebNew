using ASL.Hrms.SharedKernel.Interfaces;
using CompanySetup.Core.Events;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanySetup.Core.Handlers
{
    public class ApproveCompanyEventHandler : ICommandHandler<ApproveCompanyEvent>
    {
        public ApproveCompanyEventHandler(IConfiguration configuration,
            ICompanyApprovePostService companyApprovePostService)
        {
            _configuration = configuration;
            _companyApprovePostService = companyApprovePostService;
        }
        private readonly IConfiguration _configuration;
        private readonly ICompanyApprovePostService _companyApprovePostService;

        public async Task Handle(ApproveCompanyEvent command)
        {
            //await _companyApprovePostService.AddDefaultEmployeeStatuses(command.CompanyId);
            //await _companyApprovePostService.AddDefaultGrades(command.CompanyId);
            //await _companyApprovePostService.AddDefaultEmployeeConfirmationRules(command.CompanyId);
            await _companyApprovePostService.AddDefaultValues(command.CompanyId);
        }
    }
}