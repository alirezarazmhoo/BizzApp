using DataLayer.Data;
using DataLayer.Infrastructure;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class UnitOfWorkRepo : IUnitOfWorkRepo
	{
		private ApplicationDbContext _DbContext;
		private ProvinceRepo provinceRepo;
		public UnitOfWorkRepo(ApplicationDbContext DbContext)
		{
			_DbContext = DbContext;
		}

		public IProvinceRepo ProvinceRepo => provinceRepo ?? new ProvinceRepo(_DbContext);

		public async Task SaveAsync()
		{
			await _DbContext.SaveChangesAsync();
		}
	}
}
