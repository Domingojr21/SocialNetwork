
using Microsoft.AspNetCore.Identity;

namespace RedSocial.Infrastructure.Identity.Entities
{
   public class Users : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Photo { get; set; } = null!;

    }
}
