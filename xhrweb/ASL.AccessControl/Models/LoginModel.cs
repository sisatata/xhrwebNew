namespace ASL.AccessControl.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool LockoutOnFailure { get; set; }

        // for token based authentication
        public string TokenKey { get; set; }
        public string TokenIssuer { get; set; }
        public string IpAddress { get; set; }
    }
}
