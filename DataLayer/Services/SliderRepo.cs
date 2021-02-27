using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class SliderRepo : RepositoryBase<Slider>, ISliderRepo
	{
		public SliderRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}
		public async Task<Slider> GetById(int Id)
		{
			return await FindByCondition(s => s.Id.Equals(Id)).FirstOrDefaultAsync();
		}
		public async Task<IEnumerable<Slider>> GetAll()
		{
			return await FindAll(null)
		  .OrderByDescending(s => s.Id)
		  .ToListAsync();
		}
		public async Task<Slider> GetRandom()
		{
			Random random = new Random();
			Slider SelectedSlider = new Slider();
			var Item = await DbContext.Sliders.Where(s=>s.Status == DomainClass.Enums.SlideStatusEnum.Publish).ToListAsync();
			if (Item != null)
			{
				SelectedSlider = Item.ElementAt(random.Next(1, Item.Count()));
				return SelectedSlider;
			}
			else
			{
				return null; 
			}
		}
		public async Task AddOrUpdate(Slider model, IFormFile _File)
		{
			if (model.Id == 0)
			{
				if (_File != null)
				{
					var fileName = Path.GetFileName(_File.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Slider\Files", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						_File.CopyTo(stream);
					}
					model.Image = "/Upload/Slider/Files/" + fileName;
				}
			}
			else
			{
				var item = await DbContext.Sliders.FindAsync(model.Id);
				if (item != null)
				{
					if (_File != null)
					{
						if (item != null)
						{
							if (!string.IsNullOrEmpty(item.Image))
							{
								File.Delete($"wwwroot/{item.Image}");
							}
						}
						var fileName = Path.GetFileName(_File.FileName);
						var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Slider\Files", fileName);
						using (var stream = new FileStream(filePath, FileMode.Create))
						{
							_File.CopyTo(stream);
						}
						item.Image = "/Upload/Slider/Files/" + fileName;
					}
					item.Status = model.Status;
					item.Text = model.Text;
					item.Title = model.Title;
				}
			}
		}
		public async Task Remove(int Id)
		{
			Slider sliderItem = await DbContext.Sliders.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if(sliderItem != null)
			{
			if (!string.IsNullOrEmpty(sliderItem.Image))
			{
				File.Delete($"wwwroot/{sliderItem.Image}");
			}
				 DbContext.Sliders.Remove(sliderItem);
			}
		}
	}
}
