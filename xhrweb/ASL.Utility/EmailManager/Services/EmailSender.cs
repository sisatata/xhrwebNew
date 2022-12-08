using ASL.Utility.EmailManager.Interfaces;
using ASL.Utility.EmailManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ASL.Utility.EmailManager.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendMail(EmailModel emailModel, MailServerSettings settings)
        {
            try
            {
                using var client = new SmtpClient();
                var credential = new NetworkCredential
                {
                    UserName = settings.FromEmail,
                    Password = settings.Password
                };

                client.UseDefaultCredentials = false;
                client.Credentials = credential;
                client.Host = settings.Host;
                client.Port = settings.Port;
                client.EnableSsl = true;

                using var emailMessage = new MailMessage();
                if (emailModel.ReceiverMailIds.Any())
                {
                    foreach (var email in emailModel.ReceiverMailIds)
                    {
                        emailMessage.To.Add(new MailAddress(email));
                    }
                    //if (emailModel.ReceiverCCs != null && emailModel.ReceiverCCs.Any())
                    //{
                    foreach (var cc in emailModel.ReceiverCCs ?? new List<string>())
                    {
                        emailMessage.CC.Add(new MailAddress(cc));
                    }
                    //}
                    //if (emailModel.ReceiverBccs != null && emailModel.ReceiverBccs.Any())
                    //{
                    foreach (var Bcc in emailModel.ReceiverBccs ?? new List<string>())
                    {
                        emailMessage.Bcc.Add(new MailAddress(Bcc));
                    }
                    //}
                    emailMessage.From = new MailAddress(settings.FromEmail);
                    emailMessage.Subject = emailModel.Subject;
                    emailMessage.Body = emailModel.Body;
                    emailMessage.IsBodyHtml = emailModel.AllowHtml;

                    foreach (var attachment in emailModel.Attachments ?? new List<string>())
                    {
                        emailMessage.Attachments.Add(new Attachment(attachment));
                    }
                    await client.SendMailAsync(emailMessage);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            await Task.CompletedTask;
        }
    }
}
