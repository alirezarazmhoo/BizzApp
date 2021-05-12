using DomainClass;
using DomainClass.Commands;
using DomainClass.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IFriendRepo
	{
		Task CreateRelation(CreateFriendRelationCommand model);
		Task<IEnumerable<SharedUserProfileDetailQuery>> GetAll(string userName, int page = 1);
		Task RemoveRelation(RemoveFriendRelationCommand model);
		Task AcceptedRelation(string receiverUserId, string applicatorUserId);
		Task RejectRelation(string receiverUserId, string applicatorUserId);
		Task<bool> CheckRelation(Guid Id);
		Task<Friend> GetById(Guid Id); 
	}
}
