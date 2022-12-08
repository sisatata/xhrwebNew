using ASL.AccessControl.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.AccessControl.Services.Security
{
    public interface IAuthenticationService
    {
        Task SignIn(LoginModel login);
    }
}
