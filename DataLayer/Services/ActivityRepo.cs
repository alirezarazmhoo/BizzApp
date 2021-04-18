using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class ActivityRepo : RepositoryBase<UserActivity>, IUserActivityRepo
	{
		public ActivityRepo(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task Add(UserActivity userActivity, ActivityObject activityObject)
		{
			UserActivity model = new UserActivity();
			switch (activityObject)
			{
				case ActivityObject.UserPhoto:
					model = new UserActivity
					{

					};
					break;
				case ActivityObject.Review:
					break;
				default:
					break;
			}

			if (string.IsNullOrEmpty(model.Table)) return;

			await DbContext.UserActivities.AddAsync(model);

			await DbContext.SaveChangesAsync();
		}
	}

	public enum ActivityObject
	{
		UserPhoto,
		Review
	}
}
