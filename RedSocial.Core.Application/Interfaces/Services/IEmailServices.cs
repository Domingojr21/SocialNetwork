

using RedSocial.Core.Application.Dtos.Email;
using RedSocial.Core.Domain.Settings;

namespace RedSocial.Core.Application.Interfaces.Services
{
  public interface IEmailServices
    {
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);

    }
}
