using DataLayer.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BizApp.Extensions
{
	public class UniqueUserNameAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var _context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
			var entity = _context.Users.SingleOrDefault(e => e.UserName == value.ToString());

			if (entity != null)
			{
				return new ValidationResult(GetErrorMessage(value.ToString()));
			}

			return ValidationResult.Success;
		}

		public string GetErrorMessage(string email)
		{
			return $"نام کاربری تکراری است";
		}
	}
}
