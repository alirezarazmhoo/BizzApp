using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class UnitOfWorkRepo : IUnitOfWorkRepo
	{
		private ApplicationDbContext _DbContext;
		private readonly ClaimsPrincipal _currentUser;
		private readonly ProvinceRepo provinceRepo;
		private readonly DistrictRepo districtRepo;
		private readonly CityRepo cityRepo;
		private readonly CategoryRepo categoryRepo;
		private readonly FeatureRepo featureRepo;
		private readonly CategoryFeatureRepo categoryFeaturesRepo;
		private readonly BusinessRepo businessRepo;

		public UnitOfWorkRepo(ApplicationDbContext DbContext)
		{
			_DbContext = DbContext;
		}

		public UnitOfWorkRepo(ApplicationDbContext DbContext, ClaimsPrincipal currentUser)
		{
			_DbContext = DbContext;
			_currentUser = currentUser;
		}

		public IProvinceRepo ProvinceRepo => provinceRepo ?? new ProvinceRepo(_DbContext);
		public ICityRepo CityRepo => cityRepo ?? new CityRepo(_DbContext);
		public IDistrictRepo DistrictRepo => districtRepo ?? new DistrictRepo(_DbContext);
		public ICateogryRepo  CategoryRepo =>  categoryRepo ?? new CategoryRepo(_DbContext);
		public IFeatureRepo FeatureRepo =>  featureRepo ?? new FeatureRepo(_DbContext);
		public ICategoryFeatureRepo CategoryFeaturesRepo => categoryFeaturesRepo ?? new CategoryFeatureRepo(_DbContext);
		public IBusinessRepo BusinessRepo => businessRepo ?? new BusinessRepo(_DbContext, _currentUser);
		public async Task SaveAsync()
		{
			await _DbContext.SaveChangesAsync();
		}
	}
}
