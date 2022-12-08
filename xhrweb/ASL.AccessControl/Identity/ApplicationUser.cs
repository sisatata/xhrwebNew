using ASL.AccessControl.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ASL.AccessControl.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? CompanyId { get; set; }
        public string FullName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
