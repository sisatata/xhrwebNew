using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.AccessControl.Models
{
    public class ForgotPasswordResponse
    {
        public string Email { get; set; }
        public string PasswordResetToken { get; set; }
    }
}
