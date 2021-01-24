using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Utility;
using DomainClass;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizApp.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ManageAccountsController : Controller
    {
        private readonly UserManager<BizAppUser> _userManager;
        private readonly string _userId;
        private readonly IMapper _mapper;

        public ManageAccountsController(UserManager<BizAppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _userId = _userManager.GetUserId(User);
            _mapper = mapper;
        }

		public async Task<IActionResult> Index(string searchString, int? pageNumber)
		{
			bool shouldSearch = false;
			try
			{
				if (!string.IsNullOrEmpty(searchString)) shouldSearch = true;

                var query = _userManager.Users.Where(w => w.Id == _userId);

				int pageSize = 5;
                var items = (shouldSearch == false) ?
                        await query.ToListAsync()
                        : await query.Where(w => w.UserName.Contains(searchString) ||
                                                 w.Mobile.ToString().Contains(searchString) ||
                                                 w.Email.Contains(searchString) ||
                                                 w.FullName.Contains(searchString) )
                                    .ToListAsync();

				var users = items.Select(s => _mapper.Map<BizAppUser, UserViewModel>(s))
									.OrderByDescending(o => o.Id).AsQueryable();

				return View(PaginatedList<UserViewModel>.CreateAsync(users, pageNumber ?? 1, pageSize));
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                //var appUser = new BizAppUser
                //{
                //    UserName = user.Name,
                //    Email = user.Email
                //};

                //IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                //if (result.Succeeded)
                //    return RedirectToAction("Index");
                //else
                //{
                //    foreach (IdentityError error in result.Errors)
                //        ModelState.AddModelError("", error.Description);
                //}
            }
            return View(user);
        }

    }
}