using DomainClass;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IFriendRepo
	{
		Task CreateRelation(Friend model);
	}
}
