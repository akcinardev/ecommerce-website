using EcommerceApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Data
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>
	{
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Product>()
				.HasMany(p => p.Comments)
				.WithOne(c => c.Product)
				.HasForeignKey(c => c.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

            var ownerRole = new IdentityRole()
			{
				Id = "d115ce88-ad66-4019-95d5-3a2bb7c81343",
				Name = "Owner",
				NormalizedName = "OWNER"
			};

			var ownerInitialPassword = "Owner!23";

			var ownerUser = new AppUser()
			{
				Id = "d179b4a4-a9cf-42e9-b6d0-4bfe95825f53",
				UserName = ownerRole.Name,
				NormalizedUserName = ownerRole.NormalizedName,
				Email = "owner@akc.com",
				NormalizedEmail = "OWNER@AKC.COM",
				PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, ownerInitialPassword)
			};

			var ownerUserRole = new IdentityUserRole<string>()
			{
				RoleId = ownerRole.Id,
				UserId = ownerUser.Id,
			};

			modelBuilder.Entity<IdentityRole>().HasData(ownerRole);
			modelBuilder.Entity<AppUser>().HasData(ownerUser);
			modelBuilder.Entity<IdentityUserRole<string>>().HasData(ownerUserRole);
        }
	}
}
