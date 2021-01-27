using System;

namespace BizApp.Areas.Admin.Models
{
	public class BusinessListViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string DistrictName { get; set; }
		public string CategoryName { get; set; }
		public string CityName { get; set; }
		public string Creator { get; set; }
		public string CreatedDate { get; set; }

	}
}
