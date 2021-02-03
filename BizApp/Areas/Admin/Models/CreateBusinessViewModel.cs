using DomainClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.Admin.Models
{
	public class CreateBusinessViewModel
	{
		private const string WebSiteUrl = @"^(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\?([^#]*))?(#(.*))?";

		public CreateBusinessViewModel()
		{
		}

		public CreateBusinessViewModel(IList<ProvinceViewModel> provinces)
		{
			Provinces = provinces;
		}

		public Guid Id { get; set; }
		[Required(ErrorMessage = "لطفا نام را وارد کنید")]
		[MaxLength(100, ErrorMessage = "نام بسیار طولانی است")]
		[MinLength(3, ErrorMessage = "نام بسیار کوتاه است")]
		public string Name { get; set; }

		public string Description { get; set; }
		public int provinceId { get; set; }
		public int cityId { get; set; }
		[Required(ErrorMessage = "منطقه را انتخاب نمایید")]
		[Range(1, int.MaxValue, ErrorMessage = "لطفا منطقه را انتخاب کنید")]
		public int districtId { get; set; }
		[Required(ErrorMessage = "منطقه را انتخاب نمایید")]
		public int categoryId { get; set; }
		public string biograpy { get; set; }
		[RegularExpression(WebSiteUrl, ErrorMessage = "آدرس وبسایت معتبر نمی باشد")]
		public string websiteurl { get; set; }
		[Required(ErrorMessage = "آدرس را وارد کنید")]
		[MaxLength(200, ErrorMessage = "آدرس بسیار طولانی است")]
		[MinLength(2, ErrorMessage = "آدرس بسیار کوتاه است")]
		public string address { get; set; }
		[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "کد پستی نامعتبر است")]
		public string PostalCode { get; set; }
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required(ErrorMessage = "شماره تماس را وارد نمایید")]
		[DataType(DataType.PhoneNumber, ErrorMessage = "شماره تماس نا معتبر است")]
		[RegularExpression(@"^([0-9]{11})$", ErrorMessage = "شماره تماس نامعتبر است")]
		public long CallNumber { get; set; }

		public double latitude { get; set; }
		public double longitude { get; set; }
		public string UserCreatorId { get; set; }

		public virtual IList<ProvinceViewModel> Provinces { get; }

		public ICollection<Category> Categories { get; set; }

	}
}
