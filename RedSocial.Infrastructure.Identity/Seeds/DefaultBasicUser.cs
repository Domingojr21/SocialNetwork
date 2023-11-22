
using Microsoft.AspNetCore.Identity;
using RedSocial.Core.Application.Enums;
using RedSocial.Infrastructure.Identity.Entities;

namespace RedSocial.Infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task Seed(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
             Users defaultUser = new Users();
             defaultUser.UserName = "basicuser";
             defaultUser.Email = "basicuser@email.com";
             defaultUser.FirstName = "Eliott";
             defaultUser.LastName = "Reyes";
             defaultUser.EmailConfirmed = true;
             defaultUser.PhoneNumberConfirmed = true;
             
            if(userManager.Users.All(a => a.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user != null)
                {
                    await userManager.CreateAsync(defaultUser, "Mario2003-10");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }

        }
    }
}
