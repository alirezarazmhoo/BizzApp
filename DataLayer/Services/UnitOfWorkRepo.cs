﻿using DataLayer.Data;
using DataLayer.Infrastructure;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class UnitOfWorkRepo : IUnitOfWorkRepo
	{
		private ApplicationDbContext _DbContext;
		private readonly ProvinceRepo provinceRepo;
		private readonly DistrictRepo districtRepo;
		private readonly CityRepo cityRepo;
		private readonly CategoryRepo categoryRepo;
		private readonly FeatureRepo featureRepo;
		private readonly CategoryFeatureRepo categoryFeaturesRepo;

		public UnitOfWorkRepo(ApplicationDbContext DbContext)
		{
			_DbContext = DbContext;
		}

		public IProvinceRepo ProvinceRepo => provinceRepo ?? new ProvinceRepo(_DbContext);
		public ICityRepo CityRepo => cityRepo ?? new CityRepo(_DbContext);
		public IDistrictRepo DistrictRepo => districtRepo ?? new DistrictRepo(_DbContext);
		public ICateogryRepo  CategoryRepo =>  categoryRepo ?? new CategoryRepo(_DbContext);
		public IFeatureRepo FeatureRepo =>  featureRepo ?? new FeatureRepo(_DbContext);
		public ICategoryFeatureRepo CategoryFeaturesRepo => categoryFeaturesRepo ?? new CategoryFeatureRepo(_DbContext);
		public async Task SaveAsync()
		{
			await _DbContext.SaveChangesAsync();
		}
	}
}