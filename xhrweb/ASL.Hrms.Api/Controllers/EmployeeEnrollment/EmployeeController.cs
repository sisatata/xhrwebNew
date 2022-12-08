using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ASL.AccessControl.Models;
using ASL.AccessControl.Services.Security;
using ASL.AccessControl.Utility;
using ASL.Hrms.Api.PermissionProvider;
using ASL.Utility.EmailManager.Interfaces;
using ASL.Utility.EmailManager.Models;
using EmployeeEnrollment.Application.Employee.Commands;
using EmployeeEnrollment.Application.Employee.Queries;
using CompanyQuery = CompanySetup.Application.Company.Queries;
using EmployeeEnrollment.Application.Employee.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ASL.Hrms.SharedKernel.Models;
using Asl.Infrastructure.ExcelReports;
using System.IO;
using ASL.Hrms.Api.Reports;

namespace ASL.Hrms.Api.Controllers
{
    //[Authorize]
    public class EmployeeController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IOptions<MailServerSettings> _mailServerSettings;
        private readonly IOptions<PlanetHRSettings> _settings;
        private readonly IExcelReportService _excelReportService;
        public EmployeeController(IUserService userService, ILogger<AccountController> logger, IEmailSender emailSender,
            IOptions<MailServerSettings> mailServerSettings,
            IExcelReportService excelReportService,
            IOptions<PlanetHRSettings> settings)
        {
            _userService = userService;
            _logger = logger;
            _emailSender = emailSender;
            _mailServerSettings = mailServerSettings;
            _excelReportService = excelReportService;
            _settings = settings;
        }
        #region Queries

        [HttpGet]
        //[Authorize(StandardPermissionProvider.ViewEmployeePermission)]
        public async Task<IActionResult> Get()
        {
            
            var data = await Mediator.Send(new Queries.GetEmployeeList());
            return Ok(data);
        }

        [HttpPost]
        [Route("paged-list", Name = "GetEmployeePagedList")]
        [Authorize(StandardPermissionProvider.ViewEmployeePermission)]
        public async Task<IActionResult> GetEmployeePagedList([FromBody] Queries.GetEmployeePagedList input)
        {
            //var query = new Queries.GetEmployeePagedList { PageNumber = input.PageNumber, PageSize = input.PageSize, GetAll = input.GetAll };
            var data = await Mediator.Send(input);
            return Ok(data);
        }
       

        [HttpPost]
        [Route("filtered-paged-list", Name = "GetEmployeeFilteredListWithoutPagination")]
        [Authorize(StandardPermissionProvider.ViewEmployeePermission)]
        public async Task<IActionResult> GetEmployeeFilteredList([FromBody] Queries.GetEmployeeFilteredList input)
        {
            //var query = new Queries.GetEmployeePagedList { PageNumber = input.PageNumber, PageSize = input.PageSize, GetAll = input.GetAll };
            var data = await Mediator.Send(input);
            return Ok(data);
        }
        [HttpPost]
        [Route("filtered-list", Name = "GetEmployeeFilteredList")]
        [Authorize(StandardPermissionProvider.ViewEmployeePermission)]
        public async Task<IActionResult> GetEmployeeFilteredListWithoutPagination([FromBody] Queries.GetEmployeeFilteredListWithoutPagination input)
        {
            //var query = new Queries.GetEmployeePagedList { PageNumber = input.PageNumber, PageSize = input.PageSize, GetAll = input.GetAll };
            var data = await Mediator.Send(input);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployee { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployee command)
        {
            var data = await Mediator.Send(command);
            if (data.Status)
            {
                var user = await _userService.CreateUser(new UserModel { Email = command.UserId, Password = command.Password, CompanyId = command.CompanyId, PhoneNumber = "" });
                if (!user.ToLower().Contains("error"))
                {
                    var companyDetail = await Mediator.Send(new CompanyQuery.Queries.GetCompanyById { CompanyId = command.CompanyId }).ConfigureAwait(false);

                    var role = await _userService.AssignRole(user, new string[] { AuthorizationConstants.Roles.EMPLOYEEROLE });
                    var dataLogin = await Mediator.Send(new Commands.V1.UpdateLoginIdEmployee { Id = data.Id, LoginId = new Guid(user) });
                    SendNewEmployeeWelcomeEmail(command, companyDetail.CompanyName);
                    _logger.LogError(user);
                }
                else
                {
                    var deleteEmployee = await Mediator.Send(new Commands.V1.MarkAsDeleteEmployee { Id = data.Id });
                    data.Status = false;
                    data.Message = user;
                    data.Id = Guid.Empty;
                }
            }
            _logger.LogInformation($"{data.Message} : {command.FullName}");
            return Ok(data);
        }

        [HttpPost("employee-list-export")]
        public async Task<IActionResult> EmployeeExport([FromBody] Commands.V1.EmployeeListExport input)
        {
            var data = await Mediator.Send(input);
            string excelName = $"EmployeeList.xlsx";
            var report = await _excelReportService.Export(data);
            return ReturnExcelFileFromFilePath(report, excelName);
        }

        private async void SendNewEmployeeWelcomeEmail(Commands.V1.CreateEmployee command, string _companyName)
        {
            //var companyDetail = await Mediator.Send(new CompanyQuery.Queries.GetCompanyById { CompanyId = command.CompanyId }).ConfigureAwait(false);
            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine($"<html> <body>");
            sbBody.AppendLine($"<p>Dear {command.FullName},</p>");

            sbBody.AppendLine($"<p>");

            sbBody.AppendLine($"Welcome to <b>{_companyName}</b>. We are excited to have you and look forward to see you into our organization.");
            sbBody.AppendLine($"</p>");
            sbBody.AppendLine($"<p>");
            sbBody.AppendLine($"From now on, please record your every HR activity into the planetHR app. Here are your login credentials to access the application");
            sbBody.AppendLine($"</p>");
            sbBody.AppendLine($"<p>");
            sbBody.AppendLine($"Password: {command.Password}");
            sbBody.AppendLine($"</p>");
            sbBody.AppendLine($"<p>For App download:");
            sbBody.AppendLine($"<br />Android: <a href= \"https://play.google.com/store/apps/details?id=com.aplectrum.planethr\">https://play.google.com/store/apps/details?id=com.aplectrum.planethr</a>");
            sbBody.AppendLine($"<br />iOS: <a href= \"https://apps.apple.com/us/app/planethr/id1521201088\">https://apps.apple.com/us/app/planethr/id1521201088</a>");
            sbBody.AppendLine($"</p>");
            sbBody.AppendLine($"<br />Please use <b>{_settings.Value.DomainName}</b> as the domain name when entering into the mobile apps.");

            sbBody.AppendLine($"<br />planetHR Web Portal URL: <a href= \"{_settings.Value.ClientUrl}\">{_settings.Value.ClientUrl}</a>");
            sbBody.AppendLine($"<br />");

            sbBody.AppendLine($"Regards,");
            sbBody.AppendLine($"<br />");
            sbBody.AppendLine($"<br />");
            sbBody.AppendLine("<p style=\"color:Green;\"><b>planetHR Team</b></p>");
            sbBody.AppendLine($"</body>");
            sbBody.AppendLine($"</html>");

            await _emailSender.SendMail(new EmailModel
            {
                Subject = $"Congratulations, your account is created successfully in planetHR by your company.",
                Body = sbBody.ToString(),
                AllowHtml = true,
                ReceiverMailIds = new List<string> { command.UserId },
                ReceiverBccs = new List<string> { "azhar@aplectrum.com" }
            }, _mailServerSettings.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployee command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployee command)
        {
            //var oEmoployee = await Mediator.Send(new Queries.GetEmployee { Id = command.Id });
            var data = await Mediator.Send(command);
            if (data.Status)
            {
                await _userService.DeleteUserByUserId(data.LoginId);
            }
            return Ok(data);
        }
        private IActionResult ReturnExcelFileFromFilePath(ExcelResponse<string> excelFile, string fileName)
        {
            var stream = new MemoryStream();

            using (FileStream fileStream = System.IO.File.OpenRead(excelFile.Data))
            {
                stream.SetLength(fileStream.Length);
                fileStream.Read(stream.GetBuffer(), 0, (int)fileStream.Length);
            }

            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        #endregion
    }
}






