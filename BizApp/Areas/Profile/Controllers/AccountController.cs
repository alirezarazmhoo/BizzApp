using AutoMapper;
using BizApp.Areas.Profile.Models.Account;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using BizApp.Utility;
using System;
using DomainClass.Commands;

namespace BizApp.Areas.Profile.Controllers
{
	[Area("profile")]
	[Authorize]
	public class AccountController : ProfileController
	{
		public AccountController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(unitOfWork, httpContextAccessor, mapper)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Edit()
		{
			var userId = CurrentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
			var userDetail = await UnitOfWork.UserRepo.GetById(userId);

			var model = Mapper.Map<EditAcountViewModel>(userDetail);
			var persianDate = userDetail.BirthDate.ToPersianDateString();

			// set birth date
			if (!string.IsNullOrEmpty(persianDate))
			{
				var dateParts = persianDate.Split('/');
				model.Day = int.Parse(dateParts[2]);
				model.Month = int.Parse(dateParts[1]);
				model.Year = int.Parse(dateParts[0]);
			}

			// set main photo image
			model.MainPhoto = await UnitOfWork.UserRepo.GetMainPhoto(userId);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Update(EditAcountViewModel model)
		{
			//ModelState.Remove("Id");
			if (!ModelState.IsValid)
			{
				return View("edit", model);
			}

			try
			{
				var command = Mapper.Map<EditAcountCommand>(model);

				await UnitOfWork.UserRepo.UpdateUserInformation(command);

				return RedirectToAction("edit");
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}
	}
}
