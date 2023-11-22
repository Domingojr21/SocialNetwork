using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Infrastructure.Identity.Contexts;
using RedSocial.Infrastructure.Identity.Entities;
using RedSocial.Infrastructure.Identity.Services;

namespace RedSocial.Infrastructure.Identity
{
    public static class ServicesRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts

            services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
            m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));

            #endregion

            #region Identity

            services.AddIdentity<Users, IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
            services.AddAuthentication();
            #endregion


            #region Services
            services.AddScoped<IAccountServices, AccountServices>();    
            #endregion
        }
    }
}
