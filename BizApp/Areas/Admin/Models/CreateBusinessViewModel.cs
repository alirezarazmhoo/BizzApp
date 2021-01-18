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

		public virtual IList<ProvinceViewModel> Provinces { get; }
	}
}
