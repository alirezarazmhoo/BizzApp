using BizApp.Areas.WebApi.Controllers;
using BizApp.Areas.WebApi.Models;
using DataLayer.Data;
using DataLayer.Infrastructure;
using DataLayer.Services;
using DomainClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BizzAppTest
{
	public class UnitTest1
	{
		BusinessController  _businessController;
		IUnitOfWorkRepo _UnitOfWork;
		ApplicationDbContext _DbContext;
		UserManager<BizAppUser> _userManager;
		IUserActivityRepo _userActivity;
		public UnitTest1()
		{
			_UnitOfWork = new UnitOfWorkRepo(_DbContext, _userManager , _userActivity);
			_businessController = new BusinessController(_UnitOfWork); 
		}
		[Fact]
		public   void GetChosen()
		{
			var httpContext = new DefaultHttpContext();
			httpContext.Request.Headers["Token"] = "POJF2BILVSVIKHODA7WZCUVJAANWUZ675";
			var mockedRepository = new Mock<IUnitOfWorkRepo>();
			var controller = new BusinessController(_UnitOfWork)
			{
				ControllerContext = new ControllerContext()
				{
					HttpContext = httpContext,
				}
			};
			var id = new Guid("4e9b06be-2a73-4c40-fea1-08d8e04ff1b3");
			var result =  controller.GetById(id);

			//var okResult = _businessController.GetById(id).Result;
			 Assert.IsType<Task<BusinessItem>>(result);
			//Assert.Equal(id , result.id);

		}
	}
}
