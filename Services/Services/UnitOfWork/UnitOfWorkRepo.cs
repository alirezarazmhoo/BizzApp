using DataLayer.Data;
using DataLayer.Infrastructure;
using DataLayer.Infrastructure.Reviews;
using DomainClass;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class UnitOfWorkRepo : IUnitOfWorkRepo
	{
		private ApplicationDbContext _DbContext;
		private readonly UserManager<BizAppUser> _userManager;
		private readonly IUserActivityRepo _userActivity;
		private readonly ClaimsPrincipal _currentUser;
		private readonly ProvinceRepo provinceRepo;
		private readonly BusinessQouteRepo businessQouteRepo;
		private readonly BusinessQouteUserRepo businessQouteUserRepo;
		private readonly DistrictRepo districtRepo;
		private readonly CityRepo cityRepo;
		private readonly CategoryRepo categoryRepo;
		private readonly FeatureRepo featureRepo;
		private readonly CategoryFeatureRepo categoryFeaturesRepo;
		private readonly BusinessRepo businessRepo;
		private readonly SliderRepo sliderRepo;
		private readonly UserRepo userRepo;
		private readonly UserProfileRepo profileRepo;
		private readonly ReviewRepo reviewRepo;
		private readonly UserPhotoRepo userPhotoRepo;
		private readonly UserProfileRepo userProfileRepo;
		private readonly BusinessReviewCountRepo businessReviewCountRepo;
		private readonly BusinessHomePageRepo businessHomePageRepo;
		private readonly AskTheCommunityRepo askTheCommunityRepo;
		private readonly UserFavoritsRepo userFavoritsRepo;
		private readonly UserActivityRepo userActivity;
		private readonly FriendRepo friendRepo;
		private readonly NotificationRepo notificationRepo;
		private readonly BusinessRecentlyViewdRepo  businessRecentlyViewdRepo;
		private readonly NearBusinessSuggestProfileRepo  nearBusinessSuggestProfileRepo;

		public UnitOfWorkRepo(ApplicationDbContext DbContext)
		{
			_DbContext = DbContext;
		}

		public UnitOfWorkRepo(ApplicationDbContext DbContext, UserManager<BizAppUser> userManager, IUserActivityRepo userActivity)
		{
			_DbContext = DbContext;
			_userManager = userManager;
			_userActivity = userActivity;
		}
		public UnitOfWorkRepo(ApplicationDbContext dbContext, UserManager<BizAppUser> userManager, IUserActivityRepo userActivity, ClaimsPrincipal currentUser) : this(dbContext, userManager, userActivity)
		{
			_currentUser = currentUser;
		}

		public IProvinceRepo ProvinceRepo => provinceRepo ?? new ProvinceRepo(_DbContext);
		public IBusinessQouteRepo BusinessQouteRepo => businessQouteRepo ?? new BusinessQouteRepo(_DbContext);
		public IBusinessQouteUserRepo BusinessQouteUserRepo => businessQouteUserRepo ?? new BusinessQouteUserRepo(_DbContext);
		public ICityRepo CityRepo => cityRepo ?? new CityRepo(_DbContext);
		public IDistrictRepo DistrictRepo => districtRepo ?? new DistrictRepo(_DbContext);
		public ICateogryRepo CategoryRepo => categoryRepo ?? new CategoryRepo(_DbContext);
		public IFeatureRepo FeatureRepo => featureRepo ?? new FeatureRepo(_DbContext);
		public ICategoryFeatureRepo CategoryFeaturesRepo => categoryFeaturesRepo ?? new CategoryFeatureRepo(_DbContext);
		public IBusinessRepo BusinessRepo => businessRepo ?? new BusinessRepo(_DbContext, _currentUser, _userManager);
		public ISliderRepo SliderRepo => sliderRepo ?? new SliderRepo(_DbContext);
		public IUserRepo UserRepo => userRepo ?? new UserRepo(_DbContext);
		public IReviewRepo ReviewRepo => reviewRepo ?? new ReviewRepo(_DbContext);
		public IUserProfileRepo UserProfileRepo => userProfileRepo ?? new UserProfileRepo(_DbContext);
		public IUserPhotoRepo UserPhotoRepo => userPhotoRepo ?? new UserPhotoRepo(_DbContext, _userActivity);
		public IBusinessReviewCountRepo BusinessReviewCountRepo => businessReviewCountRepo ?? new BusinessReviewCountRepo(_DbContext);
		public IUserProfileRepo ProfileRepo => profileRepo ?? new UserProfileRepo(_DbContext);
		public IBusinessHomePageRepo BusinessHomePageRepo => businessHomePageRepo ?? new BusinessHomePageRepo(_DbContext);
		public IAskTheCommunityRepo AskTheCommunityRepo => askTheCommunityRepo ?? new AskTheCommunityRepo(_DbContext);
		public IUserFavoritsRepo UserFavoritsRepo => userFavoritsRepo ?? new UserFavoritsRepo(_DbContext);
		public IUserActivityRepo UserActivityRepo => userActivity ?? new UserActivityRepo(_DbContext, _userManager);
		public INotificationRepo NotificationRepo => notificationRepo ?? new NotificationRepo(_DbContext);
		public IFriendRepo FriendRepo => friendRepo ?? new FriendRepo(_DbContext, UserActivityRepo, NotificationRepo);
		public IBusinessRecentlyViewdRepo BusinessRecentlyViewdRepo => businessRecentlyViewdRepo ?? new BusinessRecentlyViewdRepo(_DbContext);
		public INearBusinessSuggestProfileRepo  NearBusinessSuggestProfileRepo =>  nearBusinessSuggestProfileRepo ?? new NearBusinessSuggestProfileRepo(_DbContext);

		public async Task SaveAsync()
		{
			await _DbContext.SaveChangesAsync();
		}
	}
}
