using Microsoft.AspNetCore.Identity;
using RedSocial.Core.Application.Enums;
using RedSocial.Infrastructure.Identity.Entities;

namespace RedSocial.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task Seed(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));

        }
    }
}
