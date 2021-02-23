using DomainClass.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
	public class Slider
	{
		private string title;
		public Slider()
		{
			Status = SlideStatusEnum.Publish;
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(50)")]
		public string Title
		{
			get { return title; }
			set
			{
				title = (string.IsNullOrEmpty(value)) ? "بدون عنوان" : value;
			}
		}
		[Required]
		public SlideStatusEnum Status { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(255)")]
		public string Image { get; set; }
		public string Text { get; set; }
	}
}
