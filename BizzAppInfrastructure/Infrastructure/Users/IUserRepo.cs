using DomainClass;
using DomainClass.Commands;
using System;
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
		Task<string> GetFullName(string userId);
		Task<string> GetUserName(string userId); 
		Task<string> GetMainPhoto(string userId);
		Task<string> UserTokenMaper(string userToken);
		Task<bool> CheckUserToken(string userToken);
		Task UpdateProfile(EditAcountCommand command);
		Task<int> GetUserFriendsCount(string Id);

		Task<bool> CheckBusinessUserValidity(Guid BusinessId, string UserToken); 
	}
}
