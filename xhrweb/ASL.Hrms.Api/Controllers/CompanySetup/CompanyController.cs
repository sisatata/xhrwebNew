using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASL.AccessControl.Models;
using ASL.AccessControl.Services.Security;
using ASL.AccessControl.Utility;
using ASL.Hrms.SharedKernel.Models;
using ASL.Utility.EmailManager.Interfaces;
using ASL.Utility.EmailManager.Models;
using CompanySetup.Application.Company.Commands;
using CompanySetup.Application.Company.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASL.Hrms.Api.Controllers.CompanySetup
{

    public class CompanyController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        private readonly IOptions<MailServerSettings> _mailServerSettings;
        private readonly ILogger<AccountController> _logger;
        private readonly IOptions<PlanetHRSettings> _settings;
        public CompanyController(IUserService userService,
            IEmailSender emailSender,
            IOptions<MailServerSettings> mailServerSettings,
            ILogger<AccountController> logger,
            IOptions<PlanetHRSettings> settings)
        {
            _userService = userService;
            _emailSender = emailSender;
            _mailServerSettings = mailServerSettings;
            _logger = logger;
            _settings = settings;
        }
        #region Queries

        [HttpGet("company-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetCompanyById { CompanyId = id });
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await Mediator.Send(new Queries.GetCompanyListByLoginUser());
                return Ok(data);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("approval-status/{approvalStatus}")]
        public async Task<IActionResult> GetAllCompanyByApprovalStatus(string approvalStatus)
        {
            var data = await Mediator.Send(new Queries.GetAllCompanyByApprovalStatus());
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyCommands.V1.CreateCompany command)
        {
            var data = await Mediator.Send(command);
            if (data.Status)
            {
                var user = await _userService.CreateUser(new UserModel { Email = command.UserId, Password = command.Password, CompanyId = data.Id, PhoneNumber = "", FullName = command.EmployeFullName });
                if (!user.ToLower().Contains("error"))
                {
                    var role = await _userService.AssignRole(user, new string[] { AuthorizationConstants.Roles.ADMINISTRATORS });
                    //
                    if (_mailServerSettings.Value.IsEnabled)
                    {
                        SendSystemAdminEmail(command);
                        SendClientEmail(command);
                    }
                }
                else
                {
                    var deleteCompany = await Mediator.Send(new CompanyCommands.V1.MarkCompanyAsDelete { Id = data.Id });
                    data.Status = false;
                    data.Message = user;
                    data.Id = Guid.Empty;
                }
            }
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CompanyCommands.V1.UpdateCompany command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] CompanyCommands.V1.MarkCompanyAsDelete command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("deactivate-company")]
        public async Task<IActionResult> DeactivateCompany([FromBody] CompanyCommands.V1.DeactivateCompany command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("approve-company")]
        public async Task<IActionResult> ApproveCompany([FromBody] CompanyCommands.V1.ApproveCompanyCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("decline-company")]
        public async Task<IActionResult> DeclineCompany([FromBody] CompanyCommands.V1.RejectCompanyCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> Post([FromForm] CompanyCommands.V1.UpdateCompanyImage command)
        {
            if (Request.Form.Files != null && Request.Form.Files.Count > 0 && Request.Form.Files[0].Length > 0)
            {
                var companyImage = Request.Form.Files?[0];

                var settings = _settings.Value;

                command.CompanyLogo = companyImage;
                command.Settings = settings;

                var data = await Mediator.Send(command);
                return Ok(data);
            }

            return null;
        }

        private async void SendSystemAdminEmail(CompanyCommands.V1.CreateCompany command)
        {
            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine($"<html>");
            sbBody.AppendLine($"<body>");
            sbBody.AppendLine($"<p>Dear System Admin,</p>");
            sbBody.AppendLine($"<p>A new company has been registered. Please approve.<br /> Name : <b>{command.CompanyName}</b>");
            sbBody.AppendLine($"<br />Contact Person: {command.EmployeFullName}");
            sbBody.AppendLine($"<br />Contact Email: {command.UserId}");
            sbBody.AppendLine($"</p>");
            sbBody.AppendLine($"Environment: <a href= \"{_settings.Value.ClientUrl}\">{_settings.Value.ClientUrl}</a>");

            sbBody.AppendLine($"<p>Regards,</p>");
            sbBody.AppendLine("<p style=\"color:Green;\"><b>planetHR Team</b></p>");
            sbBody.AppendLine($"</body>");
            sbBody.AppendLine($"</html>");

            //mailBody = mailBody.Replace("[[NAME]]", command.CompanyName);
            await _emailSender.SendMail(new EmailModel
            {
                Subject = $"[{_settings.Value.DomainName} Site] planetHR > New company > \"{command.CompanyName}\" has been registered",
                Body = sbBody.ToString(),
                AllowHtml = true,
                ReceiverMailIds = new List<string> { "support@planethrms.com" }
            }, _mailServerSettings.Value);
        }

        private async void SendClientEmail(CompanyCommands.V1.CreateCompany command)
        {
            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine($"<html>");
            sbBody.AppendLine($"<body>");
            sbBody.AppendLine($"<p>Dear {command.EmployeFullName},</p>");
            sbBody.AppendLine($"<p>Your company \"{command.CompanyName}\" has been registerd in planetHR.");
            sbBody.AppendLine($"<br />");
            sbBody.AppendLine($"Your application is currently under review and we will respond back to you within next 3 working days for approval.</p>");
            sbBody.AppendLine($"<p>For any query please visit: <a href= \"https://planethrms.com/\"> https://planethrms.com/</a> or mail us at: <a href=\"mailto:sales@aplectrum.com\">sales@aplectrum.com</a></p>");
            sbBody.AppendLine($"<p>Regards,</p>");
            sbBody.AppendLine("<p style=\"color:Green;\"><b>planetHR Team</b></p>");
            sbBody.AppendLine($"</body>");
            sbBody.AppendLine($"</html>");

            await _emailSender.SendMail(new EmailModel
            {
                Subject = $"Welcome to planetHR",
                Body = sbBody.ToString(),
                AllowHtml = true,
                ReceiverMailIds = new List<string> { command.UserId },
                ReceiverBccs = new List<string> { "support@planethrms.com" }
            }, _mailServerSettings.Value);
        }
        #endregion
    }
}