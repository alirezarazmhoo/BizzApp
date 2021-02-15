using System;

namespace BizApp.Areas.Admin.Models
{
	public class SetBusinessFeatureValueViewModel
	{
		public int FeatureId { get; set; }
		public Guid BusinessId{ get; set; }
		public string Value { get; set; }
	}
}
