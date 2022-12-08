namespace ASL.Utility.EmailManager.Models
{
    public class MailServerSettings
    {
        public string FromEmail { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public string QueryMail { get; set; }
        public bool IsEnabled { get; set; }
    }
}
