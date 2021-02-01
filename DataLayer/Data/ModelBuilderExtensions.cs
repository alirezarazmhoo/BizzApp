using DomainClass;
using DomainClass.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Data
{
	public static class ModelBuilderExtensions
	{
		public static void Seed(this ModelBuilder builder)
		{
			var userId = UserConfiguration.MainAdminId;
			var adminUser = new BizAppUser
			{
				Id = userId,
				UserName = "mainadmin",
				NormalizedUserName = "mainadmin",
				Email = "mainadmin@email.com",
				NormalizedEmail = "mainadmin@email.com",
				EmailConfirmed = true,
				LockoutEnabled = false,
				SecurityStamp = Guid.NewGuid().ToString(),
			};

			var ph = new PasswordHasher<BizAppUser>();
			adminUser.PasswordHash = ph.HashPassword(adminUser, "123456");

			builder.Entity<BizAppUser>().HasData(adminUser);
			
			var adminRoleId = UserConfiguration.AdminRoleId;
			var operatorRoleId = "467ffd0e-d5f1-4301-b9c1-bf08f8d351d2";

			builder.Entity<IdentityRole>().HasData(
				new IdentityRole { Name = "admin", NormalizedName = "admin", Id = adminRoleId, ConcurrencyStamp = adminRoleId},
				new IdentityRole { Name = "operator", NormalizedName = "operator", Id = operatorRoleId, ConcurrencyStamp = operatorRoleId }
			);

			builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = adminRoleId,
				UserId = userId
			});
		}
	}
}
