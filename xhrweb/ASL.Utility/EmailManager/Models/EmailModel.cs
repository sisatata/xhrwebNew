using System.Collections.Generic;

namespace ASL.Utility.EmailManager.Models
{
    public class EmailModel
    {
        public List<string> ReceiverMailIds { get; set; }
        public List<string> ReceiverCCs { get; set; }
        public List<string> ReceiverBccs { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool AllowHtml { get; set; }
        public List<string> Attachments { get; set; }
    }
}
