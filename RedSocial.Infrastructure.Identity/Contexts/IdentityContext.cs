using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RedSocial.Infrastructure.Identity.Entities;
 

namespace RedSocial.Infrastructure.Identity.Contexts
{
    public class IdentityContext : IdentityDbContext<Users>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
        


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Fluent Api

            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");

            builder.Entity<Users>(a =>
            {
                a.ToTable(name: "Users");
            });

            builder.Entity<IdentityRole>(a =>
            {
                a.ToTable(name: "Roles");
            });

            builder.Entity<IdentityUserRole<string>>(a =>
            {
                a.ToTable(name: "UserRoles");
            });

            builder.Entity < IdentityUserLogin<string>>(a =>
            {
                a.ToTable(name: "UserLogins");
            });



        }

    }
}
