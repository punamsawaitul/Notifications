using Microsoft.Extensions.Options;
using Notifications.Interfaces;
using Notifications.Model;
using System;
using System.IO;
using System.Net.Mail;

namespace Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly MailSettings _mailSettings;

        public NotificationService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public NotificationResultModel SendNotification(MailRequest mailRequest)
        {
            NotificationResultModel notificationResultModel = new NotificationResultModel();

            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                if (mailRequest.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in mailRequest.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                notificationResultModel.Message = "Mail sent successfully";
                notificationResultModel.Result = true;
            }
            catch (Exception ex)
            {
                notificationResultModel.Result = false;
            }

            return notificationResultModel;
        }
    }
}