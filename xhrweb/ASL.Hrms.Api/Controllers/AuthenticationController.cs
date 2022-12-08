using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASL.AccessControl.Models;
using ASL.AccessControl.Services.Security;
using ASL.Hrms.Api.ApiModels.AppAuth;
//using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace ASL.Hrms.Api.Controllers
{
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {


        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] LoginCommand model)
        //{
        //    try
        //    {
        //        //var userInfo = new LoginVm { Id= Guid.NewGuid(),LoginId = model.LoginId, DisplayName = model.LoginId, Email= "test@gmail.com", Phone= "01711567890"};
        //        var userInfo = new LoginVm
        //        {
        //            Id = new Guid("9f341a89-1805-4afd-9c35-8c765c954d78"),
        //            EmployeeId = "ASL-001",
        //            LoginId = model.LoginId,
        //            DisplayName = model.LoginId,
        //            Email = "test@gmail.com",
        //            Phone = "01711567890",
        //            CompanyId = "ab5aeca2-7a4a-4a20-bb96-383e72e839dc",
        //            CompanyName = "Aplectrum Solutions Limited"
        //        };
        //        IActionResult response = Unauthorized();

        //        if (userInfo != null)
        //        {
        //            var tokenString = "tokenstringforhardcoded";
        //            response = Ok(new { status = true, token = tokenString, userInfo });
        //        }

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new { status = false, message = ex.Message });
        //    }
        //}

        private readonly IConfiguration _configuration;
        private readonly ITokenBasedAuthService _authService;

        public AuthenticationController(IConfiguration configuration, ITokenBasedAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }



        // POST: api/auth
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] LoginDTO item)
        {
            var loginModel = new LoginModel
            {
                Email = item.Email,
                Password = item.Password,
                RememberMe = item.RememberMe,
                LockoutOnFailure = false,
                TokenKey = _configuration.GetValue<string>("Jwt:Key"),
                TokenIssuer = _configuration.GetValue<string>("Jwt:Issuer"),
                IpAddress = ipAddress()
            };

            var result = await _authService.AuthenticateAppUser(loginModel);
            if (!result.IsAuthenticated) return Unauthorized();
            setTokenCookie(result.RefreshToken);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO item)
        {
            //var refreshToken = Request.Cookies["refreshToken"];
            if(string.IsNullOrEmpty(item.RefreshToken))
                return Unauthorized(new { message = "Empty token" });

            string tokenKey = _configuration.GetValue<string>("Jwt:Key");
            string tokenIssuer = _configuration.GetValue<string>("Jwt:Issuer");
            var response = await _authService.RefreshToken(item.RefreshToken, ipAddress(), tokenKey, tokenIssuer);

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });
            setTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = await _authService.RevokeToken(token, ipAddress());

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }
        [HttpPost("signout")]
        [AllowAnonymous]
        public async Task<IActionResult> SignOut()
        {
            await _authService.SignOutAppUser();
            return Ok();
        }

        // helper methods

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
    public class LoginCommand
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
    }
    public class LoginVm
    {
        public Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public string LoginId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}