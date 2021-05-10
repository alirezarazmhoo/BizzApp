using DomainClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUserActivityRepo
	{
		Task<IList<UserActivity>> GetAllActivities(string userId, int page = 1);
		Task AddAsync(TableName table, string tableKey, string currentUserId, string description = null);
		Task Remove(string tableKey);
	}
}
