using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUserActivityRepo
	{
		Task AddAsync(string table, string tableKey, string currentUserId, string description = null);
		Task Remove(string tableKey);
	}
}
