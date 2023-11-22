using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RedSocial.Core.Application.Dtos.Email;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Domain.Settings;

namespace RedSocial.Infrastructure.Shared.Services
{
    public class EmailServices : IEmailServices
    {
        public MailSettings MailSettings { get; }

        public EmailServices(IOptions<MailSettings> mailSettings)
        {
            MailSettings = mailSettings.Value;
        }
        public async Task SendAsync(EmailRequest request)
        {

            try
            {
             
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(request.From ?? MailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(MailSettings.SmtpHost, MailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(MailSettings.SmtpUser, MailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
