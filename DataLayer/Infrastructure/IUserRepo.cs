using DomainClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUserRepo
	{
		Task<List<BizAppUser>> GetAll(string roleId);
		 Task<List<BizAppUser>> GetAll(string roleId,string searchString);
		Task<BizAppUser> GetById(string userId);
		

	}
}
