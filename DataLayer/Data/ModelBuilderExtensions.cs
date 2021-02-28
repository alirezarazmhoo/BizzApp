using DomainClass;
using DomainClass.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataLayer.Data
{
	public static class ModelBuilderExtensions
	{
		public static void SeedMainAdmin(this ModelBuilder builder)
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
			var memberRoleId = "447ffd0e-d5f1-4301-b9c1-bf08f8d351d2";

			builder.Entity<IdentityRole>().HasData(
				new IdentityRole { Name = "admin", NormalizedName = "admin", Id = adminRoleId, ConcurrencyStamp = adminRoleId },
				new IdentityRole { Name = "operator", NormalizedName = "operator", Id = operatorRoleId, ConcurrencyStamp = operatorRoleId },
				new IdentityRole { Name = "member", NormalizedName = "member", Id = memberRoleId, ConcurrencyStamp = memberRoleId }
			);

			builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = adminRoleId,
				UserId = userId
			});

	//		builder.Entity<Slider>().HasData(
 //new Slider
 //{
	// Id = 1,
	// Title = "Title1",
	// Image = "/Upload/Slider/Files/1.jpg",
	// Status = DomainClass.Enums.SlideStatusEnum.Publish,
	// Text = "Text1"
 //},
 // new Slider
 // {
	//  Id = 2,
	//  Title = "Title2",
	//  Image = "/Upload/Slider/Files/2.jpg",
	//  Status = DomainClass.Enums.SlideStatusEnum.Publish,
	//  Text = "Text2"
 // },
 //  new Slider
 //  {
	//   Id = 3,
	//   Title = "Title3",
	//   Image = "/Upload/Slider/Files/3.jpg",
	//   Status = DomainClass.Enums.SlideStatusEnum.Publish,
	//   Text = "Text3"
 //  }
 //);

		}

		public static void SeedOwnerRole(this ModelBuilder builder)
		{
			var roleId = UserConfiguration.OwnerRoleId;

			// create owner role entity
			var ownerRole = new IdentityRole
			{
				Name = UserConfiguration.OwnerRoleName,
				Id = roleId,
				ConcurrencyStamp = roleId
			};

			ownerRole.NormalizedName = ownerRole.Name.Normalize().ToUpper();

			builder.Entity<IdentityRole>().HasData(ownerRole);
		}
	}
}
