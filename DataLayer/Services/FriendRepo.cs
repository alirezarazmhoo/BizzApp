using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Commands;
using DomainClass.Enums;
using DomainClass.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

		public async Task<IEnumerable<SharedUserProfileDetailQuery>> GetAll(string userName, int page = 1)
		{
			var user = await DbContext.Users.FirstOrDefaultAsync(w => w.UserName == userName);
			if (user == null) throw new KeyNotFoundException();

			var result = 
				await DbContext.Friends
						.Where(w => w.ApplicatorUserId == user.Id)
						.Select(s => new SharedUserProfileDetailQuery
						{
							Id = s.ReceiverUserId,
							UserName = s.Receiver.UserName,
							FullName = s.Receiver.FullName,
							MainPhotoPath = 
								s.Receiver.ApplicationUserMedias.FirstOrDefault(f => f.IsMainImage && f.BizAppUserId == s.ReceiverUserId).UploadedPhoto,
							ReviewCount = s.Receiver.Reviews.Count,
							FriendsNumber = DbContext.Friends.Where(w => w.ApplicatorUserId == user.Id && w.Status == StatusEnum.Accepted).Count()
						})
						.Paginate(page, 48)
						.ToListAsync();

			return result;
		}
	}
}
