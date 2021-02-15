using DomainClass;
using DomainClass.Businesses;
using DomainClass.Enums;
using DomainClass.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

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
		public DbSet<HierarchyNamesCategory> CategoryHierarchyNames { get; set; }
		#endregion

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Feature>()
				.Property(b => b.ValueType).HasDefaultValue(BusinessFeatureType.Boolean);

			// default value for IsEnabled in users
			builder.Entity<BizAppUser>()
				.Property(b => b.IsEnabled).HasDefaultValue(true);
			
			// default value for Call Number in business
			builder.Entity<Business>()
				.Property(b => b.CallNumber).HasDefaultValue(0);

			builder.Seed();
			builder.Entity<City>().Property(p => p.Id).ValueGeneratedOnAdd();

			builder.Entity<BizAppUser>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

			// User Id Auto Generator 
			//builder.Entity<BizAppUser>().Property(p => p.Id).HasDefaultValueSql("NEWID()");
		}

		public override int SaveChanges()
		{
			UpdateSoftDeleteStatuses();
			return base.SaveChanges();
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			UpdateSoftDeleteStatuses();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			UpdateSoftDeleteStatuses();
			return base.SaveChangesAsync(cancellationToken);
		}

		private void UpdateSoftDeleteStatuses()
		{
			bool isSoftDelete;
			foreach (var entry in ChangeTracker.Entries())
			{
				isSoftDelete = typeof(ISoftDelete).IsAssignableFrom(entry.Entity.GetType());
				if (!isSoftDelete) continue;

				switch (entry.State)
				{
					case EntityState.Added:
						entry.CurrentValues["IsDeleted"] = false;
						break;
					case EntityState.Deleted:
						entry.State = EntityState.Modified;
						entry.CurrentValues["IsDeleted"] = true;
						break;
				}
			}
		}
	}
}
