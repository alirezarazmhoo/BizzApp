using AutoMapper;
using BizApp.Areas.Profile.Models.Reviews;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BizApp.Areas.Profile.Controllers
{
	[Area("profile")]
	public class ReviewsController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkRepo _unitOfWork;

		public ReviewsController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<IActionResult> Index(string userName = null, int page = 1)
		{
			if (string.IsNullOrEmpty(userName)) userName = User.Identity.Name;
			if (string.IsNullOrEmpty(userName)) return NotFound();

			var data = await _unitOfWork.ReviewRepo.GetUseReviews(userName, page);
			var model = _mapper.Map<UserReviewViewModel>(data);

			return View(model);
		}

	}
}
