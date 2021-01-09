using DataLayer.Data;
using DataLayer.Infrastructure;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class UnitOfWorkRepo : IUnitOfWorkRepo
	{
		private ApplicationDbContext _DbContext;
		private ProvinceRepo provinceRepo;
		private DistrictRepo districtRepo;
		private CityRepo cityRepo;

		public UnitOfWorkRepo(ApplicationDbContext DbContext)
		{
			_DbContext = DbContext;
		}

		public IProvinceRepo ProvinceRepo => provinceRepo ?? new ProvinceRepo(_DbContext);
		public ICityRepo CityRepo => cityRepo ?? new CityRepo(_DbContext);
		public IDistrictRepo DistrictRepo => districtRepo ?? new DistrictRepo(_DbContext);

		public async Task SaveAsync()
		{
			await _DbContext.SaveChangesAsync();
		}
	}
}
