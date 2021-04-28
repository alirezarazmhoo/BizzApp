using DomainClass;
using DomainClass.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUserRepo
	{
		Task<List<BizAppUser>> GetAll(string roleId);
		Task<List<BizAppUser>> GetAll(string roleId, string searchString);
		Task<BizAppUser> GetById(string userId);
		Task<BizAppUser> GetByUserName(string userName);
		Task<string> GetUserName(string userId);
		Task<string> GetMainPhoto(string userId);
		Task UpdateProfile(EditAcountCommand command);
	}
}
