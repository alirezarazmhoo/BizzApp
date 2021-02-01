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
		public int categoryId { get; set; }
		public string biograpy { get; set; }
		public string websiteurl { get; set; }
		[Required(ErrorMessage = "آدرس را وارد کنید")]
		[MaxLength(200, ErrorMessage = "آدرس بسیار طولانی است")]
		[MinLength(2, ErrorMessage = "آدرس بسیار کوتاه است")]
		public string address { get; set; }
		public string PostalCode { get; set; }
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		public double latitude { get; set; }
		public double longitude { get; set; }
		public string UserCreatorId { get; set; }

		public virtual IList<ProvinceViewModel> Provinces { get; }

		public ICollection<Category> Categories { get; set; }

	}
}
