using DomainClass;
using DomainClass.Commands;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IFriendRepo
	{
		Task CreateRelation(CreateFriendRelationCommand model);
	}
}
