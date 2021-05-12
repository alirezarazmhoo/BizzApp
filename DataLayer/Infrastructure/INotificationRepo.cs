using DomainClass;
using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface INotificationRepo
	{
		Task Add(string userId, NotificationModel model);
		Task Remove(Guid id);
		Task Remove(NotificationModel model, string creatorUserId, string receiverUserId);
		Task<IEnumerable<Notification>> GetTopFive(string userId);
	}
}
