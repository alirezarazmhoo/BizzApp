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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace BizApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("/admin/operator")]
    public class ManageAccountsController : Controller
    {
        private readonly IUnitOfWorkRepo _unitOfWork;
        private readonly UserManager<BizAppUser> _userManager;
        private string _userId;
        private readonly IMapper _mapper;

        public ManageAccountsController(UserManager<BizAppUser> userManager, IMapper mapper, IUnitOfWorkRepo unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> Index(string searchString, string roleId, int? page)
        {
            bool shouldSearch = false;
            _userId = _userManager.GetUserId(User);

            try
            {
                if (!string.IsNullOrEmpty(searchString)) shouldSearch = true;

                //var query = _userManager.Users.Where(w => w.Id != _userId ) ;				

                //var query =  _unitOfWork.UserRepo.GetAll(roleId);

                int pageSize = 10;
                ViewBag.roleId = roleId;
                //var items = (shouldSearch == false) ?
                //		await query.ToListAsync()
                //		: await query.Where(w => w.UserName.Contains(searchString) ||
                //								 w.Mobile.ToString().Contains(searchString) ||
                //								 w.Email.Contains(searchString) ||
                //								 w.FullName.Contains(searchString))
                //					.ToListAsync();
                var items = (shouldSearch == false) ?
                        await _unitOfWork.UserRepo.GetAll(roleId)
                        : await _unitOfWork.UserRepo.GetAll(roleId, searchString);

                var users = items.Select(s => _mapper.Map<BizAppUser, UserViewModel>(s));
                PagedList<UserViewModel> res = new PagedList<UserViewModel>(users.AsQueryable(), page ?? 1, pageSize);
                return View(res);

                //return View(PaginatedList<UserViewModel>.CreateAsync(users.AsQueryable(), pageNumber ?? 1, pageSize));
            }
            catch // (Exception ex)
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
                    //var user = _mapper.Map<BizAppUser>(model);
                    var user = new BizAppUser
                    {
                        UserName = model.Username,
                        Address = model.Address,
                        Mobile = model.Mobile,
                        FullName = model.FullName,
                        Email = model.Email,
                        Password = model.Password,
                        EmailConfirmed = true,
                        IsEnabled = model.IsEnabled
                    };

                    // Create New User
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var result = await _userManager.CreateAsync(user, user.Password);
                        if (result.Succeeded)
                        {
                            if (model.roleId == "467ffd0e-d5f1-4301-b9c1-bf08f8d351d2")
                                await _userManager.AddToRoleAsync(user, "operator");
                            else
                            {
                                if (model.roleId == "447ffd0e-d5f1-4301-b9c1-bf08f8d351d2")
                                    await _userManager.AddToRoleAsync(user, "member");
                            }

                        }
                        scope.Complete();
                    }

                    return Json(new { success = true, responseText = CustomeMessages.Succcess });
                }
                catch //(Exception ex)
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
                    // Find user for get username and hash password
                    var user = await _userManager.FindByIdAsync(model.Id);

                    // Map UpdateOperatorViewModel to BizAppUser
                    user.Address = model.Address;
                    user.Email = model.Email;
                    user.Mobile = model.Mobile;
                    user.FullName = model.FullName;
                    user.IsEnabled = model.IsEnabled;

                    //update data
                    var result = await _userManager.UpdateAsync(user);

                    // if updated
                    if (result == IdentityResult.Success)
                        return Json(new { success = true, responseText = CustomeMessages.Succcess });

                    return Json(new { success = false, responseText = CustomeMessages.Fail });
                }
                catch
                {
                    return Json(new { success = false, responseText = CustomeMessages.Fail });
                }
            }

            return Json(new { success = false, responseText = CustomeMessages.Empty });
        }

        [HttpPost("getById")]
        public async Task<IActionResult> GetById(string itemId)
        {
            if (itemId == default)
            {
                return Json(new { success = false, responseText = CustomeMessages.Fail });
            }

            var user = await _userManager.FindByIdAsync(itemId);
            if (user == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<UserViewModel>(user);
            List<EditViewModels> StatusItem = new List<EditViewModels>();

            var edit = new List<EditViewModels>
            {
                new EditViewModels { key = "FullName", value = model.FullName },
                new EditViewModels { key = "Id", value = model.Id.ToString() },
                new EditViewModels { key = "Mobile", value = "0" + model.Mobile.ToString() },
                new EditViewModels { key = "Email", value = model.Email },
                new EditViewModels { key = "Address", value = model.Address }
            };
            StatusItem.Add(new EditViewModels() { key = model.IsEnabled.ToString(), value = "" });

            return Json(new { success = true, listItem = edit.ToList(), majoritem = itemId, statusitem = StatusItem });
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove(string id)
        {
            if (id == default) throw new NullReferenceException();

            try
            {
                var user = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(user);

                return Json(new { success = true, responseText = CustomeMessages.Succcess });
            }
            catch //(Exception ex)
            {
                return Json(new { success = false, responseText = CustomeMessages.Fail });
            }
        }

        


    }
}