using DomainClass;
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
			var userId = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
			var adminUser = new BizAppUser
			{
				Id = userId,
				UserName = "mianadmin",
				NormalizedUserName = "mainadmin",
				Email = "mainadmin@email.com",
				NormalizedEmail = "mainadmin@email.com",
				EmailConfirmed = true,
				LockoutEnabled = false,
				SecurityStamp = Guid.NewGuid().ToString(),
			};

			PasswordHasher<BizAppUser> ph = new PasswordHasher<BizAppUser>();
			adminUser.PasswordHash = ph.HashPassword(adminUser, "123456");

			builder.Entity<BizAppUser>().HasData(adminUser);


			var adminRoleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";
			var userRoleId = Guid.NewGuid().ToString();

			builder.Entity<IdentityRole>().HasData(
				new IdentityRole { Name = "admin", NormalizedName = "admin", Id = adminRoleId, ConcurrencyStamp = adminRoleId},
				new IdentityRole { Name = "user", NormalizedName = "user", Id = userRoleId, ConcurrencyStamp = userRoleId }
			);

			builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = adminRoleId,
				UserId = userId
			});
		}
	}
}
