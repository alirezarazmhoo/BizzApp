using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected ApplicationDbContext DbContext;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public RepositoryBase(ApplicationDbContext dbContext)
		{
			DbContext = dbContext;
		}
		public RepositoryBase(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			DbContext = dbContext;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task CreateAsync(T entity)
		{
			await DbContext.Set<T>().AddAsync(entity);
		}

		public void Delete(T entity)
		{
			DbContext.Remove(entity);
		}

		public IQueryable<T> FindAll()
		{
			return DbContext.Set<T>().AsNoTracking();
		}

		public IQueryable<T> FindAll(Expression<Func<T, object>> expression)
		{
			return DbContext.Set<T>().Include(expression).AsNoTracking();
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
		{
			return DbContext.Set<T>().Where(expression).AsNoTracking();
		}

		public void Update(T entity)
		{
			DbContext.Set<T>().Update(entity);
		}
	}
}
