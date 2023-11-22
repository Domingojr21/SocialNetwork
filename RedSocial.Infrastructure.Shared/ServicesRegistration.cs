
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Domain.Settings;
using RedSocial.Infrastructure.Shared.Services;

namespace RedSocial.Infrastructure.Shared
{
    public static class ServicesRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddScoped<IEmailServices, EmailServices>();
        }
    }
}
