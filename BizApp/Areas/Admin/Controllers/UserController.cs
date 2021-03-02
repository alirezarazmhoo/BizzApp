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
    public class UserController : Controller
    {
        private readonly IUnitOfWorkRepo _unitOfWork;
        
        private readonly IMapper _mapper;

        public UserController( IMapper mapper, IUnitOfWorkRepo unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> DetailInfo( string userId)
        {
            var user = _unitOfWork.UserRepo.GetById(userId).Result;
                return View(user);
           
        }

        


    }
}