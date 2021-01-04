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
		protected ApplicationDbContext _DbContext;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public RepositoryBase(ApplicationDbContext DbContext)
		{
			_DbContext = DbContext;
		}
		public RepositoryBase(ApplicationDbContext DbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_DbContext = DbContext;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task CreateAsync(T entity)
		{
			await _DbContext.Set<T>().AddAsync(entity);
		}

		public void Delete(T entity)
		{
			_DbContext.Remove(entity);
		}

		public IQueryable<T> FindAll()
		{
			return _DbContext.Set<T>().AsNoTracking();
		}

		public IQueryable<T> FindAll(Expression<Func<T, object>> expression)
		{
			return _DbContext.Set<T>().Include(expression).AsNoTracking();
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
		{
			return _DbContext.Set<T>().Where(expression).AsNoTracking();
		}

		public void Update(T entity)
		{
			_DbContext.Set<T>().Update(entity);
		}
	}
}
