using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class NotificationRepo : RepositoryBase<Notification>, INotificationRepo
	{
		public NotificationRepo(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task Add(string userId, NotificationModel model, string modelId, string creatorUserId = null) 
		{
			var notification = new Notification
			{
				UserId = userId,
				Model = model,
				ModelId = modelId,
				CreatorUserId = creatorUserId
			};

			await DbContext.Notifications.AddAsync(notification);
			await DbContext.SaveChangesAsync();
		}
		public async Task<IEnumerable<Notification>> GetTopFive(string userId)
		{
			var result =
				await DbContext.Notifications
						.Where(w => w.UserId == userId)
						.OrderBy(p => p.CreatedAt)
						.Take(5)
						.ToListAsync();

			return result;
		}
		public async Task Remove(string modelId)
		{
			var notification =
				await DbContext.Notifications
						.FirstOrDefaultAsync(f => f.ModelId == modelId);

			if (notification == null) return;

			DbContext.Notifications.Remove(notification);
			await DbContext.SaveChangesAsync();
		}
	}
}
