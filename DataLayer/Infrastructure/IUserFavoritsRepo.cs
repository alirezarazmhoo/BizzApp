using DomainClass;
using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public  interface IUserFavoritsRepo
	{
		Task<IEnumerable<UserFavorits>> GetAll(Guid Id);
		Task<VotesAction> AddOrRemove(Guid Id, string UserId); 

	}
}
