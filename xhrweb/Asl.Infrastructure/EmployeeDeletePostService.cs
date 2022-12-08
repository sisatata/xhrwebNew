using ASL.Hrms.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeStatus = EmployeeEnrollment.Application.EmployeeStatus.Commands;
using Microsoft.Extensions.Configuration;
using EmployeeGrade = CompanySetup.Application.Grade.Commands;
using ConfirmationRule = CompanySetup.Application.ConfirmationRule.Commands;
//using ASL.AccessControl.Services.Security;

namespace Asl.Infrastructure
{
    public class EmployeeDeletePostService //: IEmployeeDeletePostService
    {
        //private readonly IUserService _userService;
        //public EmployeeDeletePostService(IUserService userService, IConfiguration configuration, IMediator mediator
        //    , ILogger<CompanyApprovePostService> logger
        //    )
        //{
        //    _userService = userService;
        //    _configuration = configuration;
        //    _mediator = mediator;
        //    _logger = logger;
        //}
        //private readonly IConfiguration _configuration;

        //private readonly IMediator _mediator;
        //private readonly ILogger<CompanyApprovePostService> _logger;
        //public async Task<bool> DeleteLoginUserByEmployee(Guid logInId)
        //{
        //    try
        //    {

        //        //await _mediator.Send(processingCommand).ConfigureAwait(false);
        //        await _userService.DeleteUserByUserId(logInId);

        //        _logger.LogInformation($"User deleted for {logInId}");

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception while user deleting {logInId}");
        //    }
        //    return true;
        //}


    }
}

