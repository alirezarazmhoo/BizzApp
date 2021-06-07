using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class UserRepo : RepositoryBase<BizAppUser>, IUserRepo
	{
		public UserRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}

		public async Task<List<BizAppUser>> GetAll(string roleId)
		{
			List<string> userids = await DbContext.UserRoles.Where(a => a.RoleId == roleId).Select(b => b.UserId).Distinct().ToListAsync();
			return await DbContext.Users.Where(x => userids.Any(c => c == x.Id)).OrderByDescending(x => x.CreateDate).ToListAsync();
		}
		public async Task<List<BizAppUser>> GetAll(string roleId, string searchString)
		{
			List<string> userids = DbContext.UserRoles.Where(a => a.RoleId == roleId).Select(b => b.UserId).Distinct().ToList();
			return await DbContext.Users.Where(x => userids.Any(c => c == x.Id) && x.UserName.Contains(searchString) ||
												 x.Mobile.ToString().Contains(searchString) ||
												 x.Email.Contains(searchString) ||
												 x.FullName.Contains(searchString)).OrderByDescending(x => x.CreateDate).ToListAsync();
		}
		public async Task<BizAppUser> GetById(string userId)
		{
			return await DbContext.Users.Include(x => x.City).Include(s => s.Reviews).Include(s => s.ApplicationUserMedias).Include(s=>s.City).Where(x => x.Id == userId).FirstOrDefaultAsync();
		}

		public async Task<BizAppUser> GetByUserName(string userName)
		{
			return await DbContext.Users.FirstOrDefaultAsync(f => f.UserName == userName);
		}

		public async Task<string> GetMainPhoto(string userId)
		{
			var userPhoto = await DbContext.Users
						.Where(w => w.Id == userId)
						.SelectMany(s => s.ApplicationUserMedias)
						.FirstOrDefaultAsync(f => f.IsMainImage);

			if (userPhoto == null) return string.Empty;

			return userPhoto.UploadedPhoto;
		}

		public async Task<string> GetFullName(string userId)
		{
			var Item = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(userId));
			if (Item != null)
			{
				return Item.FullName;
			}
			else
			{
				return "بدون نام";
			}
		}
		public async Task<string> GetUserName(string userId)
		{
			var Item = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(userId));
			if (Item != null)
			{
				return Item.UserName;
			}
			else
			{
				return "بدون نام";
			}
		}
		public async Task<string> UserTokenMaper(string userToken)
		{
			var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.SecurityStamp.Equals(userToken));
			if (UserItem !=null)
			{
				return UserItem.Id; 

			}
			else
			{
				return string.Empty; 
			}
		}
		public async Task<bool> CheckUserToken(string userToken)
		{
			return await DbContext.Users.AnyAsync(s => s.SecurityStamp.Equals(userToken));
		}
		public async Task UpdateProfile(EditAcountCommand command)
		{
			var user = await DbContext.Users.FirstOrDefaultAsync(f => f.Id == command.Id);

			if (user == null) throw new KeyNotFoundException();
			user.FullName = command.FullName;
			user.Gender = command.Gender;
			user.NationalCode = command.NationalCode;
			user.PostalCode = command.PostalCode;
			user.Address = command.Address;
			user.CityId = command.CityId != null && command.CityId > 0 ? command.CityId : null;
			user.BirthDate = command.BirthDate;
			await DbContext.SaveChangesAsync();
		}
		public async Task<int> GetUserFriendsCount(string Id)
		{
			var friends = await
				DbContext.Friends
					.Where(w => w.Status == DomainClass.Enums.StatusEnum.Accepted
								&& (w.ApplicatorUserId == Id || w.ReceiverUserId == Id)).CountAsync();

			return friends / 2;
		}

		public async Task<bool> CheckBusinessUserValidity(Guid BusinessId , string UserToken )
		{
			var BusinessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(BusinessId));
			if (BusinessItem != null)
			{
				var BusinessUserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(BusinessItem.OwnerId));
				var CurrentUserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.SecurityStamp.Equals(UserToken));
				if (BusinessUserItem != null && CurrentUserItem != null)
				{
					if (BusinessUserItem.SecurityStamp.Equals(CurrentUserItem.SecurityStamp))
					{
						return true;
					}
					else return false;
				}
				else return false;
			}
			else return false; 
		}
	}
}
