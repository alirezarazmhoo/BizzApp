using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
	public class CreateBusinessViewModel
	{
		private const string WebSiteUrlRegex = @"^(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\?([^#]*))?(#(.*))?";

		public CreateBusinessViewModel()
		{
		}

		public Guid Id { get; set; }
		[Required(ErrorMessage = "لطفا نام را وارد کنید")]
		[MaxLength(100, ErrorMessage = "نام بسیار طولانی است")]
		[MinLength(3, ErrorMessage = "نام بسیار کوتاه است")]
		public string Name { get; set; }
		public string Description { get; set; }
		//public int provinceId { get; set; }
		//public int cityId { get; set; }
		[Required(ErrorMessage = "منطقه را انتخاب نمایید")]
		[Range(1, int.MaxValue, ErrorMessage = "لطفا منطقه را انتخاب کنید")]
		public int DistrictId { get; set; }
		public bool IsCity { get; set; }
		[Required(ErrorMessage = "منطقه را انتخاب نمایید")]
		public int CategoryId { get; set; }
		public string Biography { get; set; }
		[RegularExpression(WebSiteUrlRegex, ErrorMessage = "آدرس وبسایت معتبر نمی باشد")]
		public string WebsiteUrl { get; set; }
		[Required(ErrorMessage = "آدرس را وارد کنید")]
		[MaxLength(200, ErrorMessage = "آدرس بسیار طولانی است")]
		[MinLength(2, ErrorMessage = "آدرس بسیار کوتاه است")]
		public string Address { get; set; }
		[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "کد پستی نامعتبر است")]
		public string PostalCode { get; set; }
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required(ErrorMessage = "شماره تماس را وارد نمایید")]
		[RegularExpression(@"^([0-9]{11})|([0-9]{10})$", ErrorMessage = "شماره تماس نامعتبر است")]
		public long CallNumber { get; set; }
		[RegularExpression(@"^([0][9][0-9]{9})|([9][0-9]{9})$", ErrorMessage = "شماره موبایل نامعتبر است")]
		public long? Mobile { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string UserCreatorId { get; set; }
		public string CategoryName { get; set; }
		public string DistrictName { get; set; }
		public string FeatureImage { get; set; }
		public Dictionary<int,string> GalleryImages { get; set; }
	}
}
