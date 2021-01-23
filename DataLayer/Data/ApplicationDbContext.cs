using DomainClass;
using DomainClass.Businesses;
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
		public DbSet<Business> Businesses { get; set; }
		public DbSet<BusinessCallNumber> BusinessCallNumbers { get; set; }
		public DbSet<BusinessFeature> BusinessFeatures { get; set; }
		public DbSet<BusinessGallery> BusinessGalleries { get; set; }
		public DbSet<BusinessTime> BusinessTimes{ get; set; }		
		public DbSet<Category> Categories { get; set; }
		public DbSet<CategoryFeature> CategoryFeatures { get; set; }
		public DbSet<Province> Provinces { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<District> Districts { get; set; }
		public DbSet<Feature> Features { get; set; }
		#endregion

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Feature>()
				.Property(b => b.ValueType).HasDefaultValue("bool");

			builder.Seed();

			builder.Entity<City>().Property(p => p.Id).ValueGeneratedOnAdd();
		}
	}
}
