using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataLayer.Data;
using Microsoft.Extensions.Configuration;
using DomainClass;
using DataLayer.Services;
using DataLayer.Infrastructure;
using AutoMapper;
using BizApp.Automapper;

namespace BizApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{


			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Data Source=45.159.113.39,2014;Initial Catalog=BizAppTestDatabase;User ID=BizzApp;Password=BizzApp2021;MultipleActiveResultSets=true"));

			//services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Data Source=45.159.113.39,2014;Initial Catalog=BizApp;User ID=BizzApp;Password=BizzApp2021;MultipleActiveResultSets=true"));

	
			services.AddDefaultIdentity<BizAppUser>(options =>
			{
				options.SignIn.RequireConfirmedAccount = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				

			}).AddRoles<IdentityRole>()
			   .AddEntityFrameworkStores<ApplicationDbContext>();

			//services.AddIdentity<BizAppUser, CustomRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


			services.AddTransient<IUnitOfWorkRepo, UnitOfWorkRepo>();

			var config = new MapperConfiguration(c =>
			{
				c.AddProfile(new AutomapperProfile());
			});
			var mapper = config.CreateMapper();
			services.AddSingleton(mapper);

			//services.AddAutoMapper(typeof(Startup));
			
			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			services.AddRazorPages();
			services.AddControllersWithViews();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "adminArea",
					pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
				.RequireAuthorization();
				//endpoints.MapControllerRoute(
				//	name: "adminArea",
				//	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}
