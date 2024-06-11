using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Shared.Entities;

namespace Shop.Shared.Data
{
    public class UserDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Roles
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "Admin"
            });
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = "2",
                Name = "Staff",
                NormalizedName = "Staff"
            });
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = "3",
                Name = "Customer",
                NormalizedName = "Customer"
            });

            modelBuilder.Entity<ApplicationUser>(b => {
                b.ToTable("AspNetUsers");
                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b => {
                b.ToTable("AspNetRoles");
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            });

            modelBuilder.Entity<ApplicationUserRole>(b => {
                b.ToTable("AspNetUserRoles");
                b.HasKey(ur => new { ur.UserId, ur.RoleId });
            });
        }
    }
}
