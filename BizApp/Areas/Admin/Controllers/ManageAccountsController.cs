using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizApp.Areas.Admin.Controllers
{
	[Area("admin")]
	[Route("/admin/operator")]
	public class ManageAccountsController : Controller
	{
		private readonly UserManager<BizAppUser> _userManager;
		private string _userId;
		private readonly IMapper _mapper;

		public ManageAccountsController(UserManager<BizAppUser> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string searchString, int? pageNumber)
		{
			bool shouldSearch = false;
			_userId = _userManager.GetUserId(User);

			try
			{
				if (!string.IsNullOrEmpty(searchString)) shouldSearch = true;

				var query = _userManager.Users.Where(w => w.Id != _userId);


				int pageSize = 5;
				var items = (shouldSearch == false) ?
						await query.ToListAsync()
						: await query.Where(w => w.UserName.Contains(searchString) ||
												 w.Mobile.ToString().Contains(searchString) ||
												 w.Email.Contains(searchString) ||
												 w.FullName.Contains(searchString))
									.ToListAsync();

				var users = items.Select(s => _mapper.Map<BizAppUser, UserViewModel>(s))
									.OrderByDescending(o => o.Id);

				return View(PaginatedList<UserViewModel>.CreateAsync(users.AsQueryable(), pageNumber ?? 1, pageSize));
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create(UserViewModel model)
		{
			ModelState.Remove("Id");
			if (ModelState.IsValid)
			{
				try
				{
					var user = _mapper.Map<BizAppUser>(model);
					user.EmailConfirmed = true;
					// Create New User
					if (model.Id == default)
					{
						using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
						{
							var result = await _userManager.CreateAsync(user, user.Password);
							if (result.Succeeded)
							{
								await _userManager.AddToRoleAsync(user, "operator");
							}
							scope.Complete();
						}
					}
					return Json(new { success = true, responseText = CustomeMessages.Succcess });
				}
				catch (Exception ex)
				{
					return Json(new { success = false, responseText = CustomeMessages.Fail });
				}

			}
			return Json(new { success = false, responseText = CustomeMessages.Fail });
		}

		[HttpPost("update")]
		public async Task<IActionResult> Update(UpdateOperatorViewModel model)
		{
			ModelState.Remove("Id");
			if (ModelState.IsValid)
			{
				try
				{
					// Map UpdateOperatorViewModel to BizAppUser
					var user = _mapper.Map<BizAppUser>(model);
					// Update New User
					await _userManager.UpdateAsync(user);
					return Json(new { success = true, responseText = CustomeMessages.Succcess });
				}
				catch (Exception ex)
				{
					return Json(new { success = false, responseText = CustomeMessages.Fail });
				}
			}

			return Json(new { success = false, responseText = CustomeMessages.Empty });
		}

		[HttpGet("getById")]
		public async Task<IActionResult> GetById(Guid id)
		{
			if (id == default)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}

			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				return NotFound();
			}

			var model = _mapper.Map<UserViewModel>(user);
			var edit = new List<EditViewModels>
			{
				new EditViewModels { key = "FullName", value = model.FullName },
				new EditViewModels { key = "Id", value = model.Id.ToString() },
				new EditViewModels { key = "Mobile", value = "0" + model.Mobile.ToString() },
				new EditViewModels { key = "Email", value = model.Email },
				new EditViewModels { key = "Address", value = model.Address }
			};

			return Json(new { success = true, listItem = edit.ToList(), majoritem = id });
		}

		[HttpPost("remove")]
		public async Task<IActionResult> Remove(Guid id)
		{
			if (id == default) throw new NullReferenceException();

			try
			{
				var user = await _userManager.FindByIdAsync(id.ToString());
				await _userManager.DeleteAsync(user);

				return Json(new { success = true, responseText = CustomeMessages.Succcess });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}
	}


}