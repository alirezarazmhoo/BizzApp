using DomainClass;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUserActivityRepo
	{
		Task<UserActivity> GetAllActivities(string userId, int page = 1);
		Task AddAsync(string table, string tableKey, string currentUserId, string description = null);
		Task Remove(string tableKey);
	}
}
