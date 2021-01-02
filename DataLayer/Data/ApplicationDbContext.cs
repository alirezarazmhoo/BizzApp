using DomainClass;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data
{
	public class ApplicationDbContext : IdentityDbContext<BizAppUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
				: base(options)
		{
		}

		#region Tables
		public DbSet<Province> Provinces { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<District> Districts { get; set; }

		#endregion

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);


			builder.Entity<City>().Property(p => p.Id).ValueGeneratedOnAdd();
		}
	}
}
