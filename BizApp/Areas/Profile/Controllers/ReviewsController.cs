using AutoMapper;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BizApp.Areas.Profile.Controllers
{
	public class ReviewsController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkRepo _unitOfWork;

		public ReviewsController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{

			return View();
		}
	}
}
