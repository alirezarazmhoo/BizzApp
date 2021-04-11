using DomainClass;
using DomainClass.Businesses;
using DomainClass.Enums;
using DomainClass.Infrastructure;
using DomainClass.Review;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
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
		public virtual DbSet<Business> Businesses { get; set; }
		public virtual DbSet<BusinessCallNumber> BusinessCallNumbers { get; set; }
		public virtual DbSet<BusinessFeature> BusinessFeatures { get; set; }
		public virtual DbSet<BusinessGallery> BusinessGalleries { get; set; }
		public virtual DbSet<BusinessTime> BusinessTimes{ get; set; }		
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<CategoryFeature> CategoryFeatures { get; set; }
		public virtual DbSet<Province> Provinces { get; set; }
		public virtual DbSet<City> Cities { get; set; }
		public virtual DbSet<District> Districts { get; set; }
		public virtual DbSet<Feature> Features { get; set; }
		public virtual DbSet<HierarchyNamesCategory> CategoryHierarchyNames { get; set; }
		public virtual DbSet<CategoryTerm> CategoryTerms { get; set; }
		public DbSet<Slider> Sliders { get; set; }
		public virtual DbSet<ApplicationUserMedia>  ApplicationUserMedias { get; set; }
		public virtual DbSet<Review>  Reviews { get; set; }
		public virtual DbSet<ReviewMedia>   ReviewMedias { get; set; }
		public virtual DbSet<UsersInReviewLike>  UsersInReviewLikes { get; set; }
		public virtual DbSet<CustomerBusinessMedia>  CustomerBusinessMedias { get; set; }
		public virtual DbSet<UsersInCustomerBusinessMediaLike>   UsersInCustomerBusinessMediaLikes { get; set; }
		public virtual DbSet<CustomerBusinessMediaPictures>  CustomerBusinessMediaPictures { get; set; }
		public virtual DbSet<BusinessFaq>  BusinessFaqs { get; set; }
		public virtual DbSet<BusinessFaqAnswer>  BusinessFaqAnswers { get; set; }
		public virtual DbSet<MessageToBusiness> MessageToBusinesses { get; set; }
		public virtual DbSet<UsersInCommunityVotes> UsersInCommunityVotes { get; set; }
		public virtual DbSet<UsersInReviewVotes>  UsersInReviewVotes { get; set; }

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
			// default value for Rate in business
			builder.Entity<Business>()
				.Property(b => b.Rate).HasDefaultValue(0);
			// default value for Is Sponsor in business
			builder.Entity<Business>()
				.Property(b => b.IsSponsor).HasDefaultValue(false);
			// default value appliccation user media createdAt
			builder.Entity<ApplicationUser>()
				.Property(b => b.CreateDate).HasDefaultValue(DateTime.Now);

			// Seed data
			builder.SeedMainAdmin();
			builder.SeedOwnerRole();

			builder.Entity<City>().Property(p => p.Id).ValueGeneratedOnAdd();

			builder.Entity<BizAppUser>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

			// Gender default value
			builder.Entity<BizAppUser>().Property(p => p.Gender).HasDefaultValue(GenderEnum.Male);

			// relation between user and business owner
			//builder.Entity<Business>().HasOne(b => b.Owner).WithMany(u => u.)


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
