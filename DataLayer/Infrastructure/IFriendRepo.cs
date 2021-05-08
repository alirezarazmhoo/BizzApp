using DomainClass.Commands;
using DomainClass.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IFriendRepo
	{
		Task CreateRelation(CreateFriendRelationCommand model);
		Task<IEnumerable<SharedUserProfileDetailQuery>> GetAll(string userName, int page = 1);
	}
}
