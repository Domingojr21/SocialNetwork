using Microsoft.Extensions.DependencyInjection;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Interfaces.Services.Generics;
using RedSocial.Core.Application.Services;
using RedSocial.Core.Application.Services.Generics;
using System.Reflection;

namespace RedSocial.Core.Application
{
    public static class ServicesRegistration
    {
        public static void AddCoreApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services
            services.AddScoped(typeof(IGenericServices<,,>), typeof(GenericServices<,,>));
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IPublicationServices, PublicationServices>();
            services.AddScoped<ICommentServices, CommentServices>();
            services.AddScoped<IFriendServices, FriendServices>();
            #endregion

        }

    }
}
