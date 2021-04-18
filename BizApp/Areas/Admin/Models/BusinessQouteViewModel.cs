using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
	public class BusinessQouteViewModel
	{
		public int BusinessQouteId { get; set; }
		[Required(ErrorMessage = "نام راوارد کنید")]
		[MaxLength(50, ErrorMessage = "نام بسیار طولانی است")]
		[MinLength(1, ErrorMessage = "نام بسیار کوتاه است")]
		[DisplayName("سوال")]
		public string Ask { get; set; }
		public int CategoryId { get; set; }
		public bool IsSelectedAnswer { get; set; }
		public string Answer { get; set; }

	}
}
