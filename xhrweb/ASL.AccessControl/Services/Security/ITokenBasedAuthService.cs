using ASL.AccessControl.Models;
using ASL.AccessControl.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.AccessControl.Services.Security
{
    public interface ITokenBasedAuthService
    {
        Task<AppUserAuthModel> AuthenticateAppUser(LoginModel model);
        Task<AppUserAuthModel> RefreshToken(string token, string ipAddress, string tokenKey, string tokenIssuer);
        Task<bool> RevokeToken(string token, string ipAddress);
        Task SignOutAppUser();
    }
}
