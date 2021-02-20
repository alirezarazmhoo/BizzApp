using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data
{
	public class BaseDbContext : UnitOfWork
	{
		public BaseDbContext(DbContextOptions<BaseDbContext> options)
			:base(options)
		{
		}

		#region Tables
		public DbSet<Province> Provinces { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<District> Districts { get; set; }

		#endregion
	}
}
