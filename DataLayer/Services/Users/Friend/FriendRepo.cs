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
		private readonly IUserActivityRepo _userActivity;
		private readonly INotificationRepo _notification;

		public FriendRepo(ApplicationDbContext dbContext, IUserActivityRepo userActivity, INotificationRepo notificationRepo) : base(dbContext)
		{
			_userActivity = userActivity;
			_notification = notificationRepo;
		}

		public async Task CreateRelation(CreateFriendRelationCommand model)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				// check send request for myself
				var sentForMyself = model.ReceiverUserId == model.ApplicatorUserId;
				if (sentForMyself) throw new ApplicationException();

				// check request is excists or not 
				var sentRequest =
					await DbContext.Friends
							.AnyAsync(f => f.ApplicatorUserId == model.ApplicatorUserId && f.ReceiverUserId == model.ReceiverUserId);

				if (sentRequest) throw new DuplicateNameException();

				// check if reciever add connection before it
				var sentFromReceiver =
					await DbContext.Friends.FirstOrDefaultAsync(f => f.ApplicatorUserId == model.ReceiverUserId
												&& f.ReceiverUserId == model.ApplicatorUserId);
				if (sentFromReceiver != null) 
				{
					// if realtion exist from friend user make relation
					await AcceptedRelation(sentFromReceiver.ReceiverUserId, sentFromReceiver.ApplicatorUserId);
					scope.Complete();

					return;
				}

				// add friend for user
				var friend = new Friend
				{
					ApplicatorUserId = model.ApplicatorUserId,
					ReceiverUserId = model.ReceiverUserId,
					Status = StatusEnum.Waiting,
					Description = model.Description
				};
				DbContext.Friends.Add(friend);

				// add notfification
				await _notification.Add(model.ReceiverUserId, NotificationModel.Friend, friend.Id.ToString(), friend.ApplicatorUserId);

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
						.Where(w => w.ApplicatorUserId == user.Id && w.Status == StatusEnum.Accepted)
						.Select(s => new SharedUserProfileDetailQuery
						{
							Id = s.ReceiverUserId,
							UserName = s.Receiver.UserName,
							FullName = s.Receiver.FullName,
							City = s.Receiver.City.Name,
							MainPhotoPath =
								s.Receiver.ApplicationUserMedias.FirstOrDefault(f => f.IsMainImage && f.BizAppUserId == s.ReceiverUserId).UploadedPhoto,
							ReviewCount = s.Receiver.Reviews.Count,
							FriendsNumber = DbContext.Friends.Where(w => w.ApplicatorUserId == user.Id && w.Status == StatusEnum.Accepted).Count(),
							Address = s.Receiver.Address
						})
						.Paginate(page, 48)
						.ToListAsync();

			return result;
		}

		public async Task<UserProfileDetailQuery> FindFriend(Guid id, string currentUserId)
		{
			var friend = await DbContext.Friends.FirstOrDefaultAsync(f => f.Id == id);
			if (friend == null) return null;

			var friendId = friend.ApplicatorUserId != currentUserId ? friend.ApplicatorUserId : friend.ReceiverUserId;

			var result =
				await DbContext.Friends
						.Where(w => w.ApplicatorUserId == friendId)
						.Select(s => new UserProfileDetailQuery
						{
							Id = s.ApplicatorUserId,
							FullName = s.Applicator.FullName,
							UserName = s.Applicator.UserName,
							ProvinceName = s.Applicator.City.Province.Name,
							CityName = s.Applicator.City.Name,
							FriendNumber = (
								DbContext.Friends
									.Where(w => w.Status == StatusEnum.Accepted
										&& (w.ApplicatorUserId == s.ApplicatorUserId ||
											w.ReceiverUserId == s.ApplicatorUserId)).Count()
							),
							ReviewCount = s.Applicator.Reviews.Count,
							MainPhotoPath = s.Applicator.ApplicationUserMedias.FirstOrDefault(f => f.IsMainImage).UploadedPhoto

						})
						.FirstOrDefaultAsync();

			return result;

		}
		public async Task RejectRelation(string receiverUserId, string applicatorUserId)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				var relation = await DbContext.Friends.
						FirstOrDefaultAsync(f => f.ApplicatorUserId == applicatorUserId && f.ReceiverUserId == receiverUserId);

				if (relation == null) throw new KeyNotFoundException();

				// remove relation
				DbContext.Friends.Remove(relation);

				// remove notification
				await _notification.Remove(relation.Id.ToString());

				await DbContext.SaveChangesAsync();

				scope.Complete();
			}
		}

		public async Task AcceptedRelation(string receiverUserId, string applicatorUserId)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				var relation = await DbContext.Friends.FirstOrDefaultAsync(f => f.ApplicatorUserId == applicatorUserId && f.ReceiverUserId == receiverUserId);

				if (relation == null) throw new KeyNotFoundException();

				// update saved relation status
				relation.Status = StatusEnum.Accepted;

				// add new relation for receiver user
				var newRelation = new Friend
				{
					Status = StatusEnum.Accepted,
					ApplicatorUserId = receiverUserId,
					ReceiverUserId = applicatorUserId
				};

				DbContext.Add(newRelation);

				// remove notification
				await _notification.Remove(relation.Id.ToString());

				// set user activity
				await _userActivity.AddAsync(TableName.Friend, relation.Id.ToString(), relation.ReceiverUserId);
				await _userActivity.AddAsync(TableName.Friend, newRelation.Id.ToString(), newRelation.ReceiverUserId);

				await DbContext.SaveChangesAsync();

				scope.Complete();
			}

		}

		public async Task RemoveRelation(RemoveFriendRelationCommand model)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				// check request is excists or not 
				var relations =
				await DbContext.Friends
					.Where(w => (w.ApplicatorUserId == model.RemoverUserId && w.ReceiverUserId == model.FriendUserId)
							|| (w.ReceiverUserId == model.RemoverUserId && w.ApplicatorUserId == model.FriendUserId))
					.ToListAsync();

				// if realtion not approved or not rejected then can't be delete
				if (relations.Count != 2)
				{
					throw new KeyNotFoundException();
				}

				// remove relations
				DbContext.Friends.RemoveRange(relations);

				// save changes
				await DbContext.SaveChangesAsync();

				scope.Complete();
			}
		}

		public async Task<bool> CheckRelation(Guid Id)
		{
			return await DbContext.Friends.AnyAsync(s => s.Id.Equals(Id) && s.Status == StatusEnum.Accepted);
		}
		public async Task<Friend> GetById(Guid Id)
		{
			return await DbContext.Friends.Include(s=>s.Receiver).Include(s=>s.Receiver).ThenInclude(s=>s.ApplicationUserMedias).FirstOrDefaultAsync(s => s.Id.Equals(Id));
		}

		public int GetFriendsNumber(string userId)
		{
			var count = 
				DbContext.Friends
					.Where(w => w.Status == StatusEnum.Accepted
								&& (w.ApplicatorUserId == userId || w.ReceiverUserId == userId)).Count();

			return count / 2;
		}

		public async Task<IEnumerable<FriendRequestQuery>> GetRequests(string userId)
		{
			return await DbContext.Friends
					.Where(w => w.ReceiverUserId == userId && w.Status == StatusEnum.Waiting)
					.Select(s => new FriendRequestQuery
					{
						Id = s.Id,
						CreatedAt = s.CreatedAt,
						Message = s.Description,
						UserDetail = new UserProfileDetailQuery
						{
							Id = s.ApplicatorUserId,
							FullName = s.Applicator.FullName,
							UserName = s.Applicator.UserName,
							ProvinceName = s.Applicator.City.Province.Name,
							CityName = s.Applicator.City.Name,
							FriendNumber = (
								DbContext.Friends
									.Where(w => w.Status == StatusEnum.Accepted
										&& (w.ApplicatorUserId == s.ApplicatorUserId ||
											w.ReceiverUserId == s.ApplicatorUserId)).Count()
							),
							ReviewCount = s.Applicator.Reviews.Count,
							MainPhotoPath = s.Applicator.ApplicationUserMedias.FirstOrDefault(f => f.IsMainImage).UploadedPhoto
						}
					})
					.ToListAsync();
		}

	}
}
