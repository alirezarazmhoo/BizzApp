using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public class UnitOfWork : DbContext, IUnitOfWork //where TContext : DbContext 
	{
		public UnitOfWork(DbContextOptions options)
			: base(options)
		{
		}

		public async Task SaveChangesAsync()
		{
			await base.SaveChangesAsync();
		}
	}
}
