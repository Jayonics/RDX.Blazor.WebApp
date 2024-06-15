using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shop.Shared.Entities;

namespace Shop.WebApp.Services
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser applicationUser)
        {
            var identity = await base.GenerateClaimsAsync(applicationUser);
            var roles = await UserManager.GetRolesAsync(applicationUser);

            foreach (var role in roles) identity.AddClaim(new Claim(ClaimTypes.Role, role));

            return identity;
        }
    }
}
