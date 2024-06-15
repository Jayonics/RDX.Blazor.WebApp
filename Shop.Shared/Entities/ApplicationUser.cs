using Microsoft.AspNetCore.Identity;

namespace Shop.Shared.Entities
{
    // Add profile data for users by adding proeprties to the User class
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
