namespace BizApp.Areas.Admin.Models
{
	public class EditBusinessViewModel : CreateBusinessViewModel
	{
		public string CategoryName { get; set; }
		public string DistrictName { get; set; }
	}
}
