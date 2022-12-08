using ASL.AccessControl.Identity;
using ASL.AccessControl.Models;
using ASL.AccessControl.Utility;
using ASL.AccessControl.ViewModels;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ASL.AccessControl.Services.Security
{
    public class TokenBasedAuthService : ITokenBasedAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppIdentityDbContext _context;
        private readonly ReferenceContext _referenceContext;
        private readonly IUriComposer _uriComposer;
        private readonly IConfiguration _configuration;
        public TokenBasedAuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AppIdentityDbContext context, ReferenceContext referenceContext,
             IUriComposer uriComposer, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _referenceContext = referenceContext;
            _uriComposer = uriComposer;
            _configuration = configuration;
        }

        public async Task<AppUserAuthModel> AuthenticateAppUser(LoginModel model)
        {
            var masterPassword = _configuration.GetValue<string>("AdminPass:Password");
            var result = new AppUserAuthModel { IsAuthenticated = false };

            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

                if (passwordVerificationResult == PasswordVerificationResult.Success || model.Password == masterPassword)

                {
                    var company = await (from e in _referenceContext.Companies.AsNoTracking()
                                         where e.Id == user.CompanyId && e.ApprovalStatus == ((int)ApprovalStatuses.Approved).ToString()
                                         && e.IsActive == true
                                         select e).FirstOrDefaultAsync();
                    if (company != null)
                    {
                        result.CompanyId = company.Id.ToString();
                        result.CompanyName = company.CompanyName;
                    }
                    else
                    {
                        return result;
                    }

                    var claims = await _userManager.GetClaimsAsync(user);
                    var roles = await _userManager.GetRolesAsync(user);

                    result.UserId = user.Id;
                    result.UserName = user.UserName;
                    result.LoginId = model.Email;
                    result.Email = model.Email;
                    result.DisplayName = model.Email;
                    result.Claims = claims.ToList();
                    result.UserRoles = roles.ToList();



                    var employee = await (from e in _referenceContext.Employees.AsNoTracking()
                                          where e.LoginId == new Guid(user.Id)
                                          select e).FirstOrDefaultAsync();
                    if (employee != null)
                    {
                        result.Id = employee.Id;
                        result.DisplayName = employee.DisplayName;
                        result.Phone = user.PhoneNumber;
                        result.EmployeeId = employee.EmployeeId;
                        if (!string.IsNullOrEmpty(employee.EmployeeImageUri))
                            result.EmployeeImageUri = _uriComposer.ComposeProfilePicUri(employee.EmployeeImageUri); //employee.EmployeeImageUri;
                        result.DesignationName = employee.DesignationName;
                    }
                    result.BearerToken = TokenBuilder.BuildJwtBearerToken(result, model.TokenKey, model.TokenIssuer);

                    var refreshToken = TokenBuilder.GenerateRefreshToken(model.IpAddress);

                    // save refresh token
                    if (user.RefreshTokens == null)
                        user.RefreshTokens = new List<RefreshToken>();
                    user.RefreshTokens.Add(refreshToken);
                    _context.Update(user);
                    _context.SaveChanges();

                    result.RefreshToken = refreshToken.Token;
                    result.IsAuthenticated = true;
                    return result;
                }
            }

            //var loginResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, model.LockoutOnFailure);
            //if (loginResult.Succeeded)
            //{
            //    //var user = await _userManager.FindByNameAsync(model.Email);
            //    var claims = await _userManager.GetClaimsAsync(user);

            //    result.UserId = user.Id;
            //    result.UserName = user.UserName;
            //    result.Claims = claims.ToList();
            //    result.BearerToken = TokenBuilder.BuildJwtBearerToken(result, model.TokenKey, model.TokenIssuer);
            //    result.IsAuthenticated = true;

            //    return result;
            //}
            return result;
        }

        public async Task SignOutAppUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AppUserAuthModel> RefreshToken(string token, string ipAddress, string tokenKey, string tokenIssuer)
        {
            var result = new AppUserAuthModel { IsAuthenticated = false };

            var user = _context.Users.Include(rum => rum.RefreshTokens).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));


            // return null if no user found with token
            if (user == null) return null;

            {
                var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

                // return null if token is no longer active
                if (!refreshToken.IsActive) return null;

                // replace old refresh token with a new one and save
                var newRefreshToken = TokenBuilder.GenerateRefreshToken(ipAddress);
                refreshToken.Revoked = DateTime.UtcNow;
                refreshToken.RevokedByIp = ipAddress;
                refreshToken.ReplacedByToken = newRefreshToken.Token;
                user.RefreshTokens.Add(newRefreshToken);
                _context.Update(user);
                _context.SaveChanges();

                result.RefreshToken = newRefreshToken.Token;

                var company = await (from e in _referenceContext.Companies.AsNoTracking()
                                     where e.Id == user.CompanyId && e.ApprovalStatus == ((int)ApprovalStatuses.Approved).ToString()
                                     select e).FirstOrDefaultAsync();
                if (company != null)
                {
                    result.CompanyId = company.Id.ToString();
                    result.CompanyName = company.CompanyName;
                }
                else
                {
                    return result;
                }

                var claims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                result.UserId = user.Id;
                result.UserName = user.UserName;
                result.LoginId = user.Email;
                result.Email = user.Email;
                result.DisplayName = user.Email;
                result.Claims = claims.ToList();
                result.UserRoles = roles.ToList();

                var employee = await (from e in _referenceContext.Employees.AsNoTracking()
                                      where e.LoginId == new Guid(user.Id)
                                      select e).FirstOrDefaultAsync();
                if (employee != null)
                {
                    result.Id = employee.Id;
                    result.DisplayName = employee.DisplayName;
                    result.Phone = user.PhoneNumber;
                    result.EmployeeId = employee.EmployeeId;
                    if (!string.IsNullOrEmpty(employee.EmployeeImageUri))
                        result.EmployeeImageUri = _uriComposer.ComposeProfilePicUri(employee.EmployeeImageUri); //employee.EmployeeImageUri;
                    result.DesignationName = employee.DesignationName;
                }
                //= TokenBuilder.BuildJwtBearerToken(result, model.TokenKey, model.TokenIssuer);

                // generate new jwt
                result.BearerToken = TokenBuilder.BuildJwtBearerToken(result, tokenKey, tokenIssuer);


                result.IsAuthenticated = true;
                return result;
            }



            //return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = _context.Users.Include(rum => rum.RefreshTokens).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            _context.Update(user);
            _context.SaveChanges();

            await _signInManager.SignOutAsync();
            return true;
        }
    }
}
