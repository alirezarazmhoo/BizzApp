using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Commands;
using Microsoft.EntityFrameworkCore;
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
			return await DbContext.Users.Include(x => x.City).Where(x => x.Id == userId).FirstOrDefaultAsync();

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

		public async Task<string> GetUserName(string userId)
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

		public Task UpdateUserInformation(EditAcountCommand command)
		{
			throw new System.NotImplementedException();
		}
	}
}
