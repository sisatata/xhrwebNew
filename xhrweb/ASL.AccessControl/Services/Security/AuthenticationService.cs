using ASL.AccessControl.Identity;
using ASL.AccessControl.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.AccessControl.Services.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task SignIn(LoginModel login)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, login.LockoutOnFailure);
            if (loginResult.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(login.Email);
                var claims = await _userManager.GetClaimsAsync(user);
            }
            


            //return test;
        }
    }
}
