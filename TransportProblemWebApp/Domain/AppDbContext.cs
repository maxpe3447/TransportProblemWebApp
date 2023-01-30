using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TransportProblemWebApp.Domain.Entities;
using TransportProblemWebApp.Service;

namespace TransportProblemWebApp.Domain
{
	public class AppDbContext :IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options) { }
		public DbSet<TextField> TextFields { get; set; }
        public DbSet<InformationField> InformationFields { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "e217e9d7-ec23-4868-acc9-81737b460ee7",
                Name = AdminAreaConfig.Name,
                NormalizedName = AdminAreaConfig.Name.ToUpper(),
            });

            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "6edae83a-f414-41f5-8a48-9c1f77c776b7",
                UserName = AdminAreaConfig.Name,
                NormalizedUserName = AdminAreaConfig.Name.ToUpper(),
                Email = AdminAreaConfig.Email,
                NormalizedEmail = AdminAreaConfig.Email.ToUpper(),
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, AdminAreaConfig.Password),
                SecurityStamp = string.Empty

            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "e217e9d7-ec23-4868-acc9-81737b460ee7",
                UserId = "6edae83a-f414-41f5-8a48-9c1f77c776b7"
            });

            builder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("c55c5de5-966d-40a9-8476-cfd6851d0d57"),
                CodeWord = "PageIndex",
                Title = "MAIN",

                //MetaDescription = "...",
                //MetaTitle = "..."
            });

            builder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("60467613-3d6a-4521-b8de-2735b98b23c6"),
                CodeWord = "PageInformations",
                Title = "Information",

                //MetaDescription = "..."

            });

            builder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("9d02c058-a1e3-4111-af64-d1aba6b2e268"),
                CodeWord = "PageAbout",
                Title = "About",

                //MetaDescription = "..."

            });
		}
    }
}
