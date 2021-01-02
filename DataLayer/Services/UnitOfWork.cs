using DataLayer.Data;
using DataLayer.Infrastructure;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class UnitOfWork : IUnitOfWorkRepo
	{
		private ApplicationDbContext _DbContext;

		public UnitOfWork(ApplicationDbContext DbContext)
		{
			_DbContext = DbContext;
		}

		public async Task SaveAsync()
		{
			await _DbContext.SaveChangesAsync();
		}
	}
}
