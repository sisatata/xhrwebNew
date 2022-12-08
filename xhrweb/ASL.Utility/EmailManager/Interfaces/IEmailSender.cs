using ASL.Utility.EmailManager.Models;
using System.Threading.Tasks;

namespace ASL.Utility.EmailManager.Interfaces
{
    public interface IEmailSender
    {
        Task SendMail(EmailModel emailModel, MailServerSettings settings);
    }
}
