using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.AccessControl.Models
{
    public class ResetPasswordResponse
    {
        public bool Succeeded { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }
}
