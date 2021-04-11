﻿using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Infrastructure.Reviews;
using DomainClass.Enums;
using DomainClass.Review;
using DomainClass.Review.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
				.Include(s => s.Business).Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted)
				.Skip((pageNumber.Value - 1) * 3).Take(3).ToListAsync();
			return Items;
		}
		public async Task<IEnumerable<CustomerBusinessMedia>> GetRecentActivityBusinessMedia(int? pageNumber)
		{
			pageNumber = pageNumber.HasValue == false ? 1 : pageNumber;

			var Items = await DbContext.CustomerBusinessMedias.Where(s => s.StatusEnum == StatusEnum.Accepted)
				.Include(s => s.CustomerBusinessMediaPictures)
				.Include(s => s.BizAppUser)
				.ThenInclude(s => s.ApplicationUserMedias)
				.Include(s => s.UsersInCustomerBusinessMediaLikes).Include(s => s.Business).Where(s => s.StatusEnum == StatusEnum.Accepted && s.CustomerBusinessMediaPictures.Where(s=>s.StatusEnum == StatusEnum.Accepted).Count()>0)
				.Skip((pageNumber.Value - 1) * 3).Take(3).ToListAsync();
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

		private IQueryable<UserReviewPaginateQuery> GetPaginateReviewQuery(string userName, int page, int pageSize = 10)
		{
			var query =
				DbContext.Reviews
						.Where(w => w.StatusEnum == StatusEnum.Accepted && w.BizAppUser.UserName == userName)
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
								OwnerFullName = s.Business.Owner.FullName
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
		public async Task<IEnumerable<UserReviewPaginateQuery>> GetUseReviews(string userName, int page)
		{
			var result = await GetPaginateReviewQuery(userName, page, _pageSize)
							.Where(w => w.Status == StatusEnum.Waiting)
							.ToListAsync();

			return result;
		}

		public async Task<IEnumerable<Review>> GetBusinessReviews(Guid Id)
		{
			var BusinessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if (BusinessItem != null)
			{
				return await DbContext.Reviews.Include(s => s.BizAppUser).ThenInclude(s => s.ApplicationUserMedias).Where(s => s.BusinessId.Equals(Id) && s.StatusEnum == StatusEnum.Accepted).ToListAsync();
			}
			else
			{
				return new List<Review>();
			}
		}
		public async Task AddReview(Review model, IFormFile[] files)
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
					foreach (var item in files)
					{
						var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(item.FileName).ToLower();
						var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Review\Files\", fileName);
						using (var stream = new FileStream(filePath, FileMode.Create))
						{
							item.CopyTo(stream);
						}
						DbContext.ReviewMedias.Add(new ReviewMedia()
						{
							LikeCount = 0,
							ReviewId = model.Id,
							CreatedAt = DateTime.Now,
							Description = string.Empty,
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
		//public async Task<CustomerBusinessMedia> GetBusinessMedia(Guid id)
		//{
		//	var BusinessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Equals(id));
		//	if(BusinessItem != null)
		//	{
		//		var CustomerBusinessItem = await DbContext.CustomerBusinessMedias.Where(s=>s.BusinessId.Equals(id) && s.)
		//	}
		//}


	}
}
