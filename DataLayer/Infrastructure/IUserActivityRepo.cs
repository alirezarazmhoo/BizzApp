using DataLayer.Services;
using DomainClass;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUserActivityRepo
	{
		Task Add(UserActivity userActivity, ActivityObject activityObject);
	}
}
