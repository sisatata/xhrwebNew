using ASL.AccessControl.Identity;
using ASL.AccessControl.Models;
using ASL.AccessControl.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Collections;

namespace ASL.AccessControl.Services.Security
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppIdentityDbContext _context;
        private readonly DbConnection _connection;
        public UserService(UserManager<ApplicationUser> userManager, AppIdentityDbContext context, DbConnection connection)
        {
            _userManager = userManager;
            _context = context;
            _connection = connection;
        }

        public async Task<List<UserViewModel>> GetUsers(string[] excludeRoles = null)
        {
            if (excludeRoles == null)
            {
                excludeRoles = new string[] { };
            }
            var query = await (from u in _context.Users.AsNoTracking()
                               join ur in _context.UserRoles.AsNoTracking() on u.Id equals ur.UserId
                               join r in _context.Roles.AsNoTracking() on ur.RoleId equals r.Id
                               where !excludeRoles.Contains(r.Name)
                               select new UserViewModel
                               {
                                   Id = u.Id,
                                   Email = u.Email,
                                   PhoneNumber = u.PhoneNumber,
                                   Roles = r.Name
                               }).ToListAsync();

            var data = query.GroupBy(x => new { x.Id, x.Email, x.PhoneNumber })
                .Select(c => new UserViewModel
                {
                    Id = c.Key.Id,
                    Email = c.Key.Email,
                    PhoneNumber = c.Key.PhoneNumber,
                    Roles = string.Join(",", c.Select(g => g.Roles))
                }).ToList();


            return data;
        }

        public async Task<string> CreateUser(UserModel model)
        {
            try
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, CompanyId = model.CompanyId, FullName = model.FullName };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return user.Id;
                }
                else
                {
                    StringBuilder errorMessage = new StringBuilder("ERROR: ");
                    foreach (var error in result.Errors)
                    {
                        errorMessage.AppendLine(error.Description);
                    }
                    return errorMessage.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> AssignRole(string userId, string[] roles)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) throw new ArgumentException("User not found");
                if (user != null)
                    await DeleteUserRoles(user);
                await _userManager.AddToRolesAsync(user, roles);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task DeleteUserRoles(ApplicationUser user)
        {

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles.ToArray());
        }
        #region change password

        public async Task<string> ChangePassword(string email, string currentPassword, string newPassword)
        {
            var result = "Change password failed";

            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    result = $"User not found for the email {email}";
                    return result;
                }
                var o = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (o.Succeeded)
                {
                    result = "Change password successfully";
                }
                else
                {
                    result = "";
                    foreach (var item in o.Errors)
                    {
                        result = $"{result} { item.Code} : { item.Description}";
                    }
                }
                if (result.Contains("Incorrect"))
                {
                    result = "Current Password you have entered is incorrect.";
                }
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return result;
            }

        }

        #endregion
        #region Forgot Password
        public async Task<ForgotPasswordResponse> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ArgumentException($"User not found for the email {email}");

            var response = new ForgotPasswordResponse
            {
                Email = email,
                PasswordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user)
            };

            return response;
        }

        public async Task<ResetPasswordResponse> ResetPassword(string email, string token, string newPassword)
        {
            var result = new ResetPasswordResponse
            {
                Succeeded = false
            };

            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    result.Succeeded = false;
                    result.Messages.Add($"User not found for the email {email}");
                    return result;
                }

                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, newPassword);
                result.Succeeded = true;
                if (!resetPasswordResult.Succeeded)
                {
                    result.Succeeded = false;
                    foreach (var item in resetPasswordResult.Errors)
                    {
                        result.Messages.Add(item.Description);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
                return result;
            }

        }

        public async Task<bool> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            await _userManager.DeleteAsync(user);
            return true;
        }
        public async Task<bool> DeleteUserByUserId(Guid userId)
        {
            var user = await _userManager.Users.Where(x => x.Id == userId.ToString()).Include(x => x.RefreshTokens).FirstOrDefaultAsync().ConfigureAwait(false);
            if (user == null) return false;

            await _userManager.DeleteAsync(user);
            return true;
        }
        public async Task<IEnumerable<UserRoles>> GetRolesById(string employeeId)
        {
            var query = $"SELECT * from access.GetRolesByUser('{employeeId}')";
            var data = await _connection.QueryAsync<UserRoles>(query);

            return data;

        }


        #endregion
    }
}
