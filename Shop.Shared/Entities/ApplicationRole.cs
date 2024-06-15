using Microsoft.AspNetCore.Identity;

namespace Shop.Shared.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
