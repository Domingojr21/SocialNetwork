using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Repositories.Generics;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Infrastructure.Persistence.Contexts;
using RedSocial.Infrastructure.Persistence.Repositories;
using RedSocial.Infrastructure.Persistence.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infrastructure.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts

            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            #endregion

            #region Repositories

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPublicationRepository, PublicationRepository>();
            services.AddScoped<IFriendRepository, FriendRepository>();
           
            #endregion

        }


    }
}
