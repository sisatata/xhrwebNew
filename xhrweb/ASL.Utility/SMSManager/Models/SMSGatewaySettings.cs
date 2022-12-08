namespace ASL.Utility.SMSManager.Models
{
    public class SMSGatewaySettings
    {
        public bool SendSMS { get; set; }
        public string GatewayURL { get; set; }
        public string SidBn { get; set; }
        public string SidEng { get; set; }
        public string SidIn { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
