using BizApp.Extensions;
using DomainClass.Enums;
using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Profile.Models.Account
{
	public class EditAcountViewModel
	{
		public string Id { get; set; }
		[Required(ErrorMessage = "نام و نام خانوادگی را وارد کنید")]
		[MaxLength(100, ErrorMessage = " و نام خانوادگی بسیار طولانی است")]
		[MinLength(1, ErrorMessage = " و نام خانوادگی بسیار کوتاه است")]
		public string FullName { get; set; }
		[Required]
		public GenderEnum Gender { get; set; }
		[MaxLength(11, ErrorMessage = "کد ملی معتبر نمی باشد")]
		[MinLength(10, ErrorMessage = "کد ملی معتبر نمی باشد")]
		[NationalCode(ErrorMessage = "کد ملی معتبر نمی باشد")]
		public string NationalCode { get; set; }
		public int? CityId { get; set; }
		public string CityName { get; set; }
		[PostalCode(ErrorMessage = "تنها اعداد مندرج در کد پستی خود را وارد نمایید")]
		public string PostalCode { get; set; }
		[MaxLength(250, ErrorMessage ="آدرس وارد شده طولانی می باشد حداکثر صول 250 کاراکتر می باشد")]
		public string Address { get; set; }
		public string MainPhoto { get; set; }
		
		// birth date fields
		public int Year { get; set; }
		public int Month { get; set; }
		public int Day { get; set; }

	}
}
