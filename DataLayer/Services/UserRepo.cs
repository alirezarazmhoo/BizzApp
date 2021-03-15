using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
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
			List<string> userids=  DbContext.UserRoles.Where(a => a.RoleId == roleId).Select(b => b.UserId).Distinct() .ToList();
			return  DbContext.Users.Where(x=>userids.Any(c => c == x.Id)).OrderByDescending(x=>x.CreateDate).ToList();
		}
		public async Task<List<BizAppUser>> GetAll(string roleId,string searchString)
		{
			List<string> userids= DbContext.UserRoles.Where(a => a.RoleId == roleId).Select(b => b.UserId).Distinct() .ToList();
			return await DbContext.Users.Where(x=>userids.Any(c => c == x.Id) && x.UserName.Contains(searchString) ||
												 x.Mobile.ToString().Contains(searchString) ||
												 x.Email.Contains(searchString) ||
												 x.FullName.Contains(searchString)).OrderByDescending(x=>x.CreateDate).ToListAsync();
		}
		public async Task<BizAppUser> GetById(string userId)
        {
			return  DbContext.Users.Include(x=>x.City).Where(x=>x.Id==userId).FirstOrDefault();

        }

		public async Task<BizAppUser> GetByUserName(string userName)
		{
			return await DbContext.Users.FirstOrDefaultAsync(f => f.UserName == userName);
		}
	}
}
