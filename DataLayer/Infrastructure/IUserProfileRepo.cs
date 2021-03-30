using DomainClass.Queries;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUserProfileRepo
	{
		Task<UserProfileDetailQuery> GetUserDetail(string userName);
		Task<SharedUserProfileDetailQuery> GetSharedUserDetail(string userName);
	}
}
