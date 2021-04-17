using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.Models
{
	public class CategoryDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Icon { get; set; }
		public string Image { get; set; }
	}
	public class InputCategroy
	{
		public double longitude { get; set; }
		public double latitude { get; set; }
		public string id { get; set; }
	}


	
}
