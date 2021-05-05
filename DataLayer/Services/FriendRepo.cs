using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Commands;
using DomainClass.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace DataLayer.Services
{
	public class FriendRepo : RepositoryBase<Friend>, IFriendRepo
	{
		public FriendRepo(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task CreateRelation(CreateFriendRelationCommand model)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				// check request is excists or not 
				var sentRequest =
					await DbContext.Friends
							.AnyAsync(f => f.ApplicatorUserId == model.ApplicatorUserId && f.ReceiverUserId == model.ReceiverUserId);

				if (sentRequest) throw new DuplicateNameException();

				// check send request for myself
				var sentForMyself = model.ReceiverUserId == model.ApplicatorUserId;
				if (sentForMyself) throw new ApplicationException();

				// add friend for user
				var friend = new Friend
				{
					ApplicatorUserId = model.ApplicatorUserId,
					ReceiverUserId = model.ReceiverUserId,
					Status = StatusEnum.Waiting
				};
				DbContext.Friends.Add(friend);

				// add notfification
				var notification = new Notification
				{
					UserId = model.ReceiverUserId,
					Description = model.Description,
					Status = NotificationStatus.Unread
				};
				DbContext.Notifications.Add(notification);

				// save changes
				await DbContext.SaveChangesAsync();

				scope.Complete();
			}

		}
	}
}
