using ASL.AccessControl.Models;
using ASL.AccessControl.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASL.AccessControl.Services.Security
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetUsers(string[] excludeRoles = null);
        Task<string> CreateUser(UserModel model);
        Task<bool> AssignRole(string userId, string[] roles);
        Task<string> ChangePassword(string email, string currentPassword, string newPassword);
        Task<ForgotPasswordResponse> ForgotPassword(string email);
        Task<ResetPasswordResponse> ResetPassword(string email, string token, string newPassword);
        Task<bool> DeleteUser(string email);
        Task<bool> DeleteUserByUserId(Guid userId);
        Task<IEnumerable<UserRoles>> GetRolesById(string employeeId);
        // confrim email
        // reset password
        // forget password
    }
}
