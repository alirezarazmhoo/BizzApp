using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Enums;
using Microsoft.EntityFrameworkCore;
using System;
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

		public async Task Add(string userId, NotificationModel model) 
		{
			var notification = new Notification
			{
				UserId = userId,
				Model = model
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

		public async Task Remove(Guid id)
		{
			var notification = await DbContext.Notifications.FirstOrDefaultAsync(f => f.Id == id);

			if (notification == null) return;

			DbContext.Notifications.Remove(notification);
			await DbContext.SaveChangesAsync();
		}
		public async Task Remove(NotificationModel model, string creatorUserId, string receiverUserId)
		{
			var notification =
				await DbContext.Notifications
						.FirstOrDefaultAsync(f => f.Model == model && f.CreatorUserId == creatorUserId && f.UserId == receiverUserId);

			if (notification == null) return;

			DbContext.Notifications.Remove(notification);
			await DbContext.SaveChangesAsync();
		}
	}
}
