using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.Admin.Models
{
	public class CreateBusinessViewModel
	{
		public CreateBusinessViewModel()
		{
		}

		public CreateBusinessViewModel(IList<ProvinceViewModel> provinces)
		{
			Provinces = provinces;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int provinceId { get; set; }
		public int cityId { get; set; }
		public int districtId { get; set; }
		public int categoryId { get; set; }
		public string biograpy { get; set; }
		public string websiteurl { get; set; }
		public string address { get; set; }
		public double latitude { get; set; }
		public double longitude { get; set; }
		public string UserCreatorId { get; set; }

		public virtual IList<ProvinceViewModel> Provinces { get; }

		public ICollection<Category> Categories { get; set; }

	}
}
