using DomainClass.Queries;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUserProfileRepo
	{
		UserProfileDetailQuery GetUserDetail(string userId);
	}
}
