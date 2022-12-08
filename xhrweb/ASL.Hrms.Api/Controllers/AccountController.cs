using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASL.AccessControl.Models;
using ASL.AccessControl.Services.Security;
using ASL.Hrms.Api.ApiModels.AppAuth;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Utility.EmailManager.Interfaces;
using ASL.Utility.EmailManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASL.Hrms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IPermissionProvider _permissionProvider;
        private readonly IEmailSender _emailSender;
        private readonly IOptions<MailServerSettings> _mailServerSettings;
        private readonly ILogger<AccountController> _logger;
        private readonly IUriComposer _uriComposer;
        public AccountController(IUserService userService, IRoleService roleService, IPermissionProvider permissionProvider,
            IEmailSender emailSender, IOptions<MailServerSettings> mailServerSettings, ILogger<AccountController> logger, IUriComposer uriComposer)
        {
            _userService = userService;
            _roleService = roleService;
            _permissionProvider = permissionProvider;
            _mailServerSettings = mailServerSettings;
            _emailSender = emailSender;
            _logger = logger;
            _uriComposer = uriComposer;
        }

        [HttpGet("users")]
        public async Task<IActionResult> List()
        {
            var data = await _userService.GetUsers(new string[] { "Candidate" });//new string[] { "Administrators" }
            return Ok(data);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> Roles()
        {
            var data = await _roleService.GetRoles();
            return Ok(data);
        }

        [HttpGet("roles/{employeeId}")]
        public async Task<IActionResult> RoleById(string employeeId)
        {
            var data = await _userService.GetRolesById(employeeId);
            return Ok(data);
        }

        [HttpGet("permissions")]
        public IActionResult Permissions()
        {
            return Ok(_permissionProvider.GetPermissions());
        }

        [HttpPost("users/create")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
        {
            var data = await _userService.CreateUser(new UserModel { Email = user.Email, Password = user.Password, PhoneNumber = user.PhoneNumber });
            return Ok(data);
        }

        [HttpPost("roles/create")]
        public async Task<IActionResult> CreateRole([FromBody] string role)
        {
            var data = await _roleService.CreateRole(role);
            return Ok(data);
        }

        [HttpPost("users/assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRoleDTO userRole)
        {
            var data = await _userService.AssignRole(userRole.UserId, userRole.Roles);
            return Ok(data);
        }

        [HttpPost("users/create-role-permission")]
        public async Task<IActionResult> RolePermission([FromBody] RolePermissionDTO rolePermission)
        {
            var data = await _roleService.AssignRolePermission(rolePermission.Roles, rolePermission.Permissions);
            return Ok(data);
        }

        #region Change Password
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel changePassword)
        {
            try
            {
                var result = await _userService.ChangePassword(changePassword.Email, changePassword.CurrentPassword, changePassword.NewPassword);

                if (result.Contains("successfully"))
                {
                    var mailBody = $@"<html>
                                <body>
                                    <p>Dear Concern,</p>
                                    <p>Your planetHR account password has been changed successfully.</p>
                                    
                                    <p>Regards,</p>
                                    <p>planetHR Team</p>
                                </body>
                                </html>
                            ";

                    await _emailSender.SendMail(new EmailModel
                    {
                        Subject = $"planetHR password changed successfully",
                        Body = mailBody,
                        AllowHtml = true,
                        ReceiverMailIds = new List<string> { changePassword.Email }
                    }, _mailServerSettings.Value);

                    return Ok(new { success = true, message = result });
                }

                return Ok(new { success = false, message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Change password, user retrieval error: {ex.Message}");
                return Ok(new { success = false, message = ex.Message });
            }
        }
        #endregion
        #region forgot password
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel forgotPassword)
        {
            try
            {
                var result = await _userService.ForgotPassword(forgotPassword.Email);

                if (!string.IsNullOrWhiteSpace(result.PasswordResetToken))
                {
                    var encodedToken = System.Web.HttpUtility.UrlEncode(result.PasswordResetToken);
                    var confirmationLink = _uriComposer.ComposeResetPasswordUri(encodedToken, result.Email);
                    //    Url.Action("ResetPassword",
                    //"Account", new { token = result.PasswordResetToken, email = result.Email },
                    //protocol: HttpContext.Request.Scheme);

                    var mailBody = $@"<html>
                                <body>
                                    <p>Dear Concern,</p>
                                    <p>Please click the link below to reset your planetHR account password.</p>
                                    <a href={confirmationLink}>{confirmationLink}</a><br />
                                    <p>If direct click doesn’t work, then copy and paste the link into your browser.</p>
                                    
                                    <p>Regards,</p>
                                    <p>planetHR Team</p>
                                </body>
                                </html>
                            ";

                    await _emailSender.SendMail(new EmailModel
                    {
                        Subject = $"planetHR reset password link",
                        Body = mailBody,
                        AllowHtml = true,
                        ReceiverMailIds = new List<string> { result.Email }
                    }, _mailServerSettings.Value);
                }

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Forgot password, user retrieval error: {ex.Message}");
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("ResetPassword")]
        public IActionResult ResetPassword(string token, string email)
        {
            try
            {
                var encodedToken = System.Web.HttpUtility.UrlEncode(token);
                var redirectUri = $"{Request.Scheme}://{Request.Host}/reset-password?token={encodedToken}&email={email}";
                return Redirect(redirectUri);
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordPost([FromBody] ResetPasswordModel resetPassword)
        {
            try
            {
                var resetResult = await _userService.ResetPassword(resetPassword.Email, resetPassword.Token, resetPassword.Password);
                return Ok(resetResult);
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        #endregion
    }
}
