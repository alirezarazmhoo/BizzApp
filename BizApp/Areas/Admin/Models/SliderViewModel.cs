using DomainClass.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
    public class SliderViewModel
    {
       
        public int Id { get; set; }

        [Display(Name = "عنوان ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title
        {
            get;
            set;

        }
        [Display(Name = "وضعیت ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public SlideStatusEnum Status { get; set; }
        [Required]
        public string Image { get; set; }
        public string Text { get; set; }
         
        [Display(Name = "تصویر ")]

        public IFormFile imageUrl { get; set; }
    }
}
