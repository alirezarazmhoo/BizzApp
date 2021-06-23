using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Infrastructure.Reviews;
using DomainClass.Businesses;
using DomainClass.Enums;
using DomainClass.Queries;
using DomainClass.Review;
using DomainClass.Review.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class ReviewRepo : RepositoryBase<Review>, IReviewRepo
	{
		private int _pageSize;
		public ReviewRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
			_pageSize = 10;
		}
		public async Task<IEnumerable<Review>> GetRecentActivity(int? pageNumber)
		{
			if (pageNumber == 1)
			{
				pageNumber += 1;
			}
			else
			{
				pageNumber = pageNumber.HasValue == false ? 1 : pageNumber;
			}
			var Items = await DbContext.Reviews
				.Include(s => s.ReviewMedias)
				.Include(s => s.UsersInReviewLikes)
				.Include(s => s.BizAppUser)
				.ThenInclude(s => s.ApplicationUserMedias)
				.Include(s => s.Business)
				.ThenInclude(s=>s.District)
				.ThenInclude(s=>s.City)
				.ThenInclude(s=>s.Province)
				.Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted)
				.Skip((pageNumber.Value - 1) * 2).Take(2).ToListAsync();
			return Items;
		}
		public async Task<IEnumerable<CustomerBusinessMedia>> GetRecentActivityBusinessMedia(int? pageNumber)
		{
			pageNumber = pageNumber.HasValue == false ? 1 : pageNumber;

			var Items = await DbContext.CustomerBusinessMedias.Where(s => s.StatusEnum == StatusEnum.Accepted)
				.Include(s => s.CustomerBusinessMediaPictures)
				.Include(s => s.BizAppUser)
				.ThenInclude(s => s.ApplicationUserMedias)
				.Include(s => s.UsersInCustomerBusinessMediaLikes).Include(s => s.Business)
				.ThenInclude(s => s.District)
				.ThenInclude(s => s.City)
				.ThenInclude(s => s.Province)
				.Where(s => s.StatusEnum == StatusEnum.Accepted && s.CustomerBusinessMediaPictures.Where(s => s.StatusEnum == StatusEnum.Accepted).Count() > 0)
				.Skip((pageNumber.Value - 1) * 2).Take(2).ToListAsync();
			return Items;
		}
		public async Task<string> GetUsersFullName(Guid Id)
		{
			string FullNames = string.Empty;
			string newChar = string.Empty;
			string Main = string.Empty;
			int counter = 0;
			string end = string.Empty;
			var Item = await DbContext.CustomerBusinessMediaPictures.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if (Item != null)
			{
				var ItemsObject = await DbContext.UsersInCustomerBusinessMediaLikes.Include(s => s.BizAppUser).Where(s => s.CustomerBusinessMediaPicturesId.Equals(Item.Id)).ToListAsync();
				if (ItemsObject.Count > 0)
				{
					for (int i = 0; i < ItemsObject.Count; i++)
					{
						counter += 1;
						if (counter == 8)
						{
							break;
						}
						else
						{
							FullNames += ItemsObject[i].BizAppUser.FullName + "<br>";
						}
					}
					if (ItemsObject.Count > 8)
					{
						end = $"و {ItemsObject.Count - 8} دیگر";
						Main = $"<span>{FullNames},{end}</span>";
					}
					else
					{
						Main = $"<span>{FullNames}</span>";
					}
				}
				else
				{
					Main = $"<span>اولین نفر در ثبت نظر باشید!</span>";
				}
			}
			return Main;
		}
		public async Task<CustomerBusinessMedia> GetCustomerBusinessMediaById(Guid id)
		{
			return (await DbContext.CustomerBusinessMedias
				.Include(s => s.CustomerBusinessMediaPictures)
				.Include(s => s.BizAppUser)
				.ThenInclude(s => s.ApplicationUserMedias)
				.Include(s => s.Business)
				.Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted)
				.FirstOrDefaultAsync(s => s.Id.Equals(id)));
		}
		public async Task<int> BusinessReviewCount(Guid Id)
		{
			var BusinessItem = await DbContext.Reviews.Where(s => s.BusinessId.Equals(Id)).CountAsync();
			return BusinessItem;
		}
		private async Task<bool> IsOwner(Guid id, string currentUserId)
		{
			var review = await DbContext.Reviews.FirstOrDefaultAsync(w => w.Id == id);
			return review.BizAppUserId == currentUserId;
		}
		private IQueryable<UserReviewPaginateQuery> GetPaginateReviewQuery(string userId, int page, int pageSize = 10)
		{
			var query =
				DbContext.Reviews
					.Where(w => w.BizAppUserId == userId)
					.Paginate(page, pageSize)
					.Select(s => new UserReviewPaginateQuery
					{
						Id = s.Id,
						Rate = s.Rate,
						Description = s.Description,
						UsefulCount = s.UsefulCount,
						FunnyCount = s.FunnyCount,
						CoolCount = s.CoolCount,
						CreatedAt = s.Date,
						Status = s.StatusEnum,
						Business = new UserReviewPaginateQuery.BusinessQuery
						{
							Id = s.Business.Id,
							FeatureImage = s.Business.FeatureImage,
							CityId = s.Business.District.CityId,
							CityName = s.Business.District.City.Name,
							Name = s.Business.Name,
							OwnerFullName = s.Business.Owner.FullName,
							CategoryId = s.Business.CategoryId
							//Categories = s.Business.Category.Parents(s.Business.CategoryId).ToDictionary(f =>f.Id, f => f.Name)
							//OwnerUserName = s.Business.Owner.UserName
						},
						Media = s.ReviewMedias.Take(3)
									.Select(m => new ReviewMediaQuery
									{
										ImagePath = m.Image,
										CreatedAt = m.CreatedAt,
										Description = m.Description
									})
									.OrderByDescending(x => x.CreatedAt)
									.ToArray()
					});

			return query;
		}
		private Dictionary<int, string> GetBusinessCategories(int categoryId)
		{
			try
			{
				var result =
					DbContext.Categories
						.FromSqlRaw("EXEC [dbo].[sp_GetAllCategoryWithParentsById] @id = {0}", categoryId)
						.Select(s => new CategoryWithParentsQuery
						{
							Id = s.Id,
							Name = s.Name,
							ParentCategoryId = s.ParentCategoryId
						})
						.ToDictionary(k => k.Id, v => v.Name);

				return result;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public async Task<IEnumerable<UserReviewPaginateQuery>> GetUseReviews(string userName, int page)
		{
			var user = await DbContext.Users.FirstOrDefaultAsync(f => f.UserName == userName);
			if (user == null) throw new KeyNotFoundException("User Not Found");

			var result = await GetPaginateReviewQuery(user.Id, page)
							.Where(w => w.Status == StatusEnum.Accepted)
							.ToListAsync();

			// add business categories
			if (result.Count > 0)
			{
				foreach (var model in result)
				{
					model.Business.Categories = GetBusinessCategories(model.Business.CategoryId);
					//model.Business.Categories = dd;
				}
			}

			return result;
		}
		public async Task<UserReviewPaginateQuery> GetUseReview(Guid id)
		{
			var review = await DbContext.Reviews
					.Where(w => w.Id == id)
					.Select(s => new UserReviewPaginateQuery
					{
						Id = s.Id,
						Rate = s.Rate,
						Description = s.Description,
						UsefulCount = s.UsefulCount,
						FunnyCount = s.FunnyCount,
						CoolCount = s.CoolCount,
						CreatedAt = s.Date,
						Status = s.StatusEnum,
						Business = new UserReviewPaginateQuery.BusinessQuery
						{
							Id = s.Business.Id,
							FeatureImage = s.Business.FeatureImage,
							CityId = s.Business.District.CityId,
							CityName = s.Business.District.City.Name,
							Name = s.Business.Name,
							OwnerFullName = s.Business.Owner.FullName,
							CategoryId = s.Business.CategoryId
						}
					})
					.FirstOrDefaultAsync();

			review.Media =
				await DbContext.ReviewMedias.Take(3)
						.Select(m => new ReviewMediaQuery
						{
							ImagePath = m.Image,
							CreatedAt = m.CreatedAt,
							Description = m.Description
						})
						.OrderByDescending(x => x.CreatedAt)
						.ToArrayAsync();

			return review;
		}
		public async Task<IEnumerable<Review>> GetBusinessReviews(Guid Id)
		{
			var result = await DbContext.Reviews.Include(s => s.BizAppUser).ThenInclude(s => s.ApplicationUserMedias).Where(s => s.BusinessId.Equals(Id) && s.StatusEnum == StatusEnum.Accepted).ToListAsync();

			return result ?? new List<Review>();
		}
		public async Task AddReview(Review model, IFormFile[] files, string[] captions)
		{

			if (await DbContext.Users.AnyAsync(s => s.Id.Equals(model.BizAppUserId)) && await DbContext.Businesses.AnyAsync(s => s.Id.Equals(model.BusinessId)))
			{
				model.StatusEnum = StatusEnum.Waiting;
				model.Date = DateTime.Now;
				model.UsefulCount = 0;
				model.CoolCount = 0;
				model.FunnyCount = 0;
				model.Rate = model.Rate == 0 ? 1 : model.Rate;
				await DbContext.Reviews.AddAsync(model);
				await DbContext.SaveChangesAsync();
				if (files != null && files.Count() > 0)
				{
					for (int i = 0; i < files.Count(); i++)
					{
						var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(files[i].FileName).ToLower();
						var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Review\Files\", fileName);
						using (var stream = new FileStream(filePath, FileMode.Create))
						{
							files[i].CopyTo(stream);
						}
						DbContext.ReviewMedias.Add(new ReviewMedia()
						{
							LikeCount = 0,
							ReviewId = model.Id,
							CreatedAt = DateTime.Now,
							Description = captions[i],
							Image = "/Upload/Review/Files/" + fileName,
						});
					}
				}
			}

		}
		public async Task AddBusinessMedia(CustomerBusinessMedia model, IFormFile[] files)
		{
			if (await DbContext.Users.AnyAsync(s => s.Id.Equals(model.BizAppUserId)) && await DbContext.Businesses.AnyAsync(s => s.Id.Equals(model.BusinessId)))
			{
				model.Date = DateTime.Now;
				model.StatusEnum = StatusEnum.Accepted;
				await DbContext.CustomerBusinessMedias.AddAsync(model);
				await DbContext.SaveChangesAsync();
				if (files != null && files.Count() > 0)
				{
					foreach (var item in files)
					{
						var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(item.FileName).ToLower();
						var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\CustomerMediaBusiess\Files\", fileName);
						using (var stream = new FileStream(filePath, FileMode.Create))
						{
							item.CopyTo(stream);
						}
						DbContext.CustomerBusinessMediaPictures.Add(new CustomerBusinessMediaPictures()
						{
							LikeCount = 0,
							CustomerBusinessMediaId = model.Id,
							StatusEnum = StatusEnum.Waiting,
							Description = string.Empty,
							Image = "/Upload/CustomerMediaBusiess/Files/" + fileName,
						});

					}
				}
			}
		}
		public async Task<IEnumerable<Business>> GuessReview(List<int> Districts, int DistricId, string UserId, int? pageNumber, int districtId, int categoryId)
		{
			List<Business> FinalList = new List<Business>();
			List<Business> FinalList2 = new List<Business>();
			List<Review> Reviews = new List<Review>();
			List<Business> Businesses = new List<Business>();
			List<Guid> BusinessesHelper = new List<Guid>();
			if (pageNumber == 1)
			{
				pageNumber += 1;
			}
			else
			{
				pageNumber = pageNumber.HasValue == false ? 1 : pageNumber;
			}
			if (DistricId != 0)
			{
				Businesses = await DbContext.Businesses.Where(s => s.DistrictId.Equals(DistricId)).ToListAsync();
			}
			else if (Districts.Count > 0)
			{
				foreach (var item in Districts)
				{
					Businesses.AddRange(await DbContext.Businesses.Where(s => s.DistrictId.Equals(item)).ToListAsync());
				}
			}
			FinalList.AddRange(Businesses);
			if (!string.IsNullOrEmpty(UserId))
			{
				var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(UserId));
				var Reveiws = await DbContext.Reviews.Include(s => s.Business).Where(s => s.BizAppUserId.Equals(UserItem.Id)).ToListAsync();
				foreach (var item in Reveiws)
				{
					BusinessesHelper.Add(item.BusinessId);
				}
				foreach (var item in FinalList)
				{
					if (!BusinessesHelper.Any(s => s.Equals(item.Id)))
					{
						FinalList2.Add(item);
					}
				}
				FinalList.Clear();
				FinalList = FinalList2;
			}
			if(districtId != 0)
			{
				FinalList =  FinalList.Where(s => s.DistrictId == districtId).ToList();
			}
			if(categoryId != 0)
			{
				FinalList = FinalList.Where(s => s.CategoryId == categoryId).ToList();
			}

			return FinalList.Skip((pageNumber.Value - 1) * 3).Take(3).ToList(); 
		}
		public async Task<VotesAction> ChangeHelpFull(Guid Id, string UserId)
		{
			var Item = await DbContext.Reviews.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(UserId));
			UsersInReviewVotes usersInReviewVotes = new UsersInReviewVotes();
			if (Item != null && UserItem != null)
			{
				if (!await DbContext.UsersInReviewVotes.AnyAsync(s => s.BizAppUserId == UserId && s.ReviewId == Id && s.VotesType == VotesType.HelpFull))
				{
					Item.UsefulCount += 1;
					usersInReviewVotes.BizAppUserId = UserId;
					usersInReviewVotes.ReviewId = Id;
					usersInReviewVotes.VotesType = VotesType.HelpFull;
					await DbContext.UsersInReviewVotes.AddAsync(usersInReviewVotes);

					return VotesAction.Add;
				}
				else
				{
					Item.UsefulCount -= 1;
					var ReviewVoteItem = await DbContext.UsersInReviewVotes.FirstOrDefaultAsync(s => s.BizAppUserId == UserId && s.ReviewId == Id && s.VotesType == VotesType.HelpFull);
					if (ReviewVoteItem != null)
					{
						DbContext.UsersInReviewVotes.Remove(ReviewVoteItem);
					}
					return VotesAction.Submission;
				}
			}
			else
			{
				return VotesAction.Undefinded;
			}
		}
		public async Task<VotesAction> ChangeFunnyCount(Guid Id, string UserId)
		{
			var Item = await DbContext.Reviews.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(UserId));
			UsersInReviewVotes usersInReviewVotes = new UsersInReviewVotes();

			if (Item != null && UserItem != null)
			{
				if (!await DbContext.UsersInReviewVotes.AnyAsync(s => s.BizAppUserId == UserId && s.ReviewId == Id && s.VotesType == VotesType.Funny))
				{
					Item.FunnyCount += 1;
					usersInReviewVotes.BizAppUserId = UserId;
					usersInReviewVotes.ReviewId = Id;
					usersInReviewVotes.VotesType = VotesType.Funny;
					await DbContext.UsersInReviewVotes.AddAsync(usersInReviewVotes);
					return VotesAction.Add;
				}
				else
				{
					Item.FunnyCount -= 1;
					var ReviewVoteItem = await DbContext.UsersInReviewVotes.FirstOrDefaultAsync(s => s.BizAppUserId == UserId && s.ReviewId == Id && s.VotesType == VotesType.Funny);
					if (ReviewVoteItem != null)
					{
						DbContext.UsersInReviewVotes.Remove(ReviewVoteItem);
					}
					return VotesAction.Submission;

				}
			}
			else
			{
				return VotesAction.Undefinded;
			}
		}
		public async Task<VotesAction> ChangeCoolCount(Guid Id, string UserId)
		{
			var Item = await DbContext.Reviews.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(UserId));
			UsersInReviewVotes usersInReviewVotes = new UsersInReviewVotes();

			if (Item != null && UserItem != null)
			{
				if (!await DbContext.UsersInReviewVotes.AnyAsync(s => s.BizAppUserId == UserId && s.ReviewId == Id && s.VotesType == VotesType.Cool))
				{
					Item.CoolCount += 1;
					usersInReviewVotes.BizAppUserId = UserId;
					usersInReviewVotes.ReviewId = Id;
					usersInReviewVotes.VotesType = VotesType.Cool;
					await DbContext.UsersInReviewVotes.AddAsync(usersInReviewVotes);
					return VotesAction.Add;
				}
				else
				{
					Item.CoolCount -= 1;
					var ReviewVoteItem = await DbContext.UsersInReviewVotes.FirstOrDefaultAsync(s => s.BizAppUserId == UserId && s.ReviewId == Id && s.VotesType == VotesType.Cool);
					if (ReviewVoteItem != null)
					{
						DbContext.UsersInReviewVotes.Remove(ReviewVoteItem);
					}
					return VotesAction.Submission;

				}
			}
			else
			{
				return VotesAction.Undefinded;
			}
		}
		public async Task<VotesAction> ChangeLikeCount(Guid Id, string UserId)
		{
			var Item = await DbContext.CustomerBusinessMediaPictures.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(UserId));
			UsersInCustomerBusinessMediaLike usersInCustomerBusinessMediaLike = new UsersInCustomerBusinessMediaLike();
			if (Item != null && UserItem != null)
			{
				if (!await DbContext.UsersInCustomerBusinessMediaLikes.
					AnyAsync(s => s.BizAppUserId == UserId &&
					s.CustomerBusinessMediaPicturesId == Id))
				{
					Item.LikeCount += 1;
					usersInCustomerBusinessMediaLike.CustomerBusinessMediaPicturesId = Item.Id;
					usersInCustomerBusinessMediaLike.BizAppUserId = UserId;
					await DbContext.UsersInCustomerBusinessMediaLikes.AddAsync(usersInCustomerBusinessMediaLike);
					return VotesAction.Add;
				}
				else
				{
					Item.LikeCount -= 1;
					var VoteItem = await DbContext.UsersInCustomerBusinessMediaLikes.
						FirstOrDefaultAsync(s => s.BizAppUserId == UserId
						&& s.CustomerBusinessMediaPicturesId == Id);
					if (VoteItem != null)
					{
						DbContext.UsersInCustomerBusinessMediaLikes.Remove(VoteItem);
					}
					return VotesAction.Submission;

				}
			}
			else
			{
				return VotesAction.Undefinded;
			}
		}
		public async Task<int> GetUserTotalReview(string Id)
		{
			var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if (UserItem != null)
			{
				return await DbContext.Reviews.Where(s => s.BizAppUserId.Equals(UserItem.Id)).CountAsync();
			}
			else
			{
				return 0;
			}
		}
		public async Task<int> GetUserTotalReviewMedia(string Id)
		{
			var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if (UserItem != null)
			{
				return await DbContext.ReviewMedias.Where(s => s.Review.BizAppUserId.Equals(UserItem.Id)).CountAsync();
			}
			else
			{
				return 0;
			}
		}
		public async Task<int> GetUserTotalBusinessMedia(string Id)
		{
			var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if (UserItem != null)
			{
				return await DbContext.CustomerBusinessMediaPictures.Where(s => s.CustomerBusinessMedia.BizAppUserId.Equals(UserItem.Id)).CountAsync();
			}
			else
			{
				return 0;
			}
		}
		public async Task PostReview(Review Model, IFormFile[] file)
		{
			int Average = 0;
			var BusinessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(Model.BusinessId));
			if (BusinessItem != null && await DbContext.Users.AnyAsync(s => s.Id.Equals(Model.BizAppUserId)))
			{
				Model.Date = DateTime.Now;
				Model.StatusEnum = StatusEnum.Waiting;
				Model.Rate = Model.Rate == 0 ? 1 : Model.Rate;
				await DbContext.Reviews.AddAsync(Model);
				await DbContext.SaveChangesAsync();
				if (file != null && file.Count() > 0)
				{
					foreach (var item in file)
					{
						var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(item.FileName).ToLower();
						var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Review\Files\", fileName);
						using (var stream = new FileStream(filePath, FileMode.Create))
						{
							item.CopyTo(stream);
						}
						DbContext.ReviewMedias.Add(new ReviewMedia()
						{
							CreatedAt = DateTime.Now,
							Description = string.Empty,
							ReviewId = Model.Id,
							Image = "/Upload/Review/Files/" + fileName,
						});
					}
				}
				var OtherReviews = await DbContext.Reviews.Where(s => s.BusinessId.Equals(Model.BusinessId)).ToListAsync();
				Average = Convert.ToInt32(OtherReviews.Average(s => s.Rate));
				BusinessItem.Rate = Average;
				await DbContext.SaveChangesAsync();
			}
		}
		public async Task<Review> GetById(string Id)
		{
			Guid ReviewId = new Guid(Id);
			return await DbContext.Reviews.
				Include(s => s.Business).
				Include(s => s.BizAppUser).
				Include(s => s.Business).
				ThenInclude(s => s.District).Include(s => s.ReviewMedias)
				.Where(s => s.Id.Equals(ReviewId) && s.StatusEnum == StatusEnum.Accepted).
				FirstOrDefaultAsync();
		}
		public async Task<int> GetBusinessTotalReview(Guid Id)
		{
			return await DbContext.Reviews.Where(s => s.BusinessId.Equals(Id)).CountAsync();
		}
		public async Task<int> GetBusinessTotalCustomerMedia(Guid Id)
		{
			return await DbContext.CustomerBusinessMediaPictures.Where(s => s.CustomerBusinessMedia.BusinessId.Equals(Id) && s.CustomerBusinessMedia.StatusEnum == StatusEnum.Accepted).CountAsync();
		}
		public async Task<bool> CheckUserAlreadyExistsInBusinessLikeGallery(string Id, Guid GalleryId)
		{
			var CustomerBusinessMediaPicture = await DbContext.CustomerBusinessMediaPictures.Where(s => s.Id.Equals(GalleryId)).FirstOrDefaultAsync();
			if (CustomerBusinessMediaPicture != null)
			{
				return await DbContext.UsersInCustomerBusinessMediaLikes.AnyAsync(s => s.CustomerBusinessMediaPicturesId.Equals(GalleryId) && s.BizAppUserId.Equals(Id));
			}
			else
			{
				return false;
			}
		}
		public async Task<IEnumerable<Review>> GetUserReview(string Id)
		{
			return await DbContext.Reviews.Include(s => s.Business).Include(s => s.ReviewMedias).Where(s => s.BizAppUserId.Equals(Id) && s.StatusEnum == StatusEnum.Accepted).OrderByDescending(s => s.Date).ToListAsync();
		}
		public async Task<ReviewMedia> GetReviewMediaDetail(Guid Id)
		{
			return await DbContext.ReviewMedias.Include(s => s.Review.BizAppUser).Include(s => s.Review.BizAppUser).ThenInclude(s => s.ApplicationUserMedias).Include(s => s.Review.Business).FirstOrDefaultAsync(s => s.Id.Equals(Id));
		}
		public async Task<IEnumerable<CustomerBusinessMediaPictures>> GetCustomerBusinessMediaPictures(string Id)
		{
			return await DbContext.CustomerBusinessMediaPictures.Include(s => s.CustomerBusinessMedia).Where(s => s.CustomerBusinessMedia.BizAppUserId.Equals(Id) && s.StatusEnum == StatusEnum.Accepted).OrderByDescending(s => s.CustomerBusinessMedia.Date).ToListAsync();
		}

	    public async Task AddCustomerBusinessMedia(CustomerBusinessMedia model, IFormFile[] files, string[] captions)
		{
	
				model.StatusEnum = StatusEnum.Waiting;
				model.Date = DateTime.Now;
				await DbContext.CustomerBusinessMedias.AddAsync(model);
				if (files != null && files.Count() > 0)
				{
					for (int i = 0; i < files.Count(); i++)
					{
						var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(files[i].FileName).ToLower();
						var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\CustomerMediaBusiess\Files\", fileName);
						using (var stream = new FileStream(filePath, FileMode.Create))
						{
							files[i].CopyTo(stream);
						}
						DbContext.CustomerBusinessMediaPictures.Add(new  CustomerBusinessMediaPictures()
						{
							LikeCount = 0,
							CustomerBusinessMediaId = model.Id,
							Description = captions[i],
							Image = "/Upload/CustomerMediaBusiess/Files/" + fileName,
						});
					}
				}
		}
		public async Task<int> GetUserTotalVotes(string UserId)
		{
			var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(UserId));
			if(UserItem != null)
			{
				return await DbContext.UsersInReviewVotes.Where(s => s.BizAppUserId.Equals(UserItem.Id)).CountAsync();
			}
			return 0; 
		}
		public async Task<int> GetUserTotalBusinessLike(string UserId)
		{
			var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(UserId));
			if (UserItem != null)
			{
				return await DbContext.UsersInCustomerBusinessMediaLikes.Where(s => s.BizAppUserId.Equals(UserItem.Id)).CountAsync();
			}
			return 0;
		}
		//public async Task<Guid> AddCustomerBusinessMediaDropZone(CustomerBusinessMedia model, IFormFile files)
		//{
		//	model.StatusEnum = StatusEnum.Waiting;
		//	model.Date = DateTime.Now;
		//	await DbContext.CustomerBusinessMedias.AddAsync(model);
		//	if (files != null )
		//	{mu
				
		//			var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(files.FileName).ToLower();
		//			var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\CustomerMediaBusiess\Files\", fileName);
		//			using (var stream = new FileStream(filePath, FileMode.Create))
		//			{
		//				files.CopyTo(stream);
		//			}
		//			var objet=  DbContext.CustomerBusinessMediaPictures.Add(new CustomerBusinessMediaPictures()
		//			{
		//				LikeCount = 0,
		//				CustomerBusinessMediaId = model.Id,
		//				Image = "/Upload/CustomerMediaBusiess/Files/" + fileName,
		//			});
		//		return objet.Entity.Id;


		//	}
		//}




	}
}