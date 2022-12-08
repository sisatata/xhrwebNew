using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace ASL.AccessControl.ViewModels
{
    public class AppUserAuthModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<Claim> Claims { get; set; }
        public List<string> UserRoles { get; set; }
        public string BearerToken { get; set; }

        //[JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public string LoginId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string EmployeeImageUri { get; set; }
        public string DesignationName { get; set; }
    }
}
