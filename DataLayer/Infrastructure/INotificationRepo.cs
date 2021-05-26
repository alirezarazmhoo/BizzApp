using DomainClass;
using DomainClass.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface INotificationRepo
	{
		Task Add(string userId, NotificationModel model, string modelId, string creatorUserId = null);
		Task Remove(string modelId);
		Task<IEnumerable<Notification>> GetTopFive(string userId);
	}
}
