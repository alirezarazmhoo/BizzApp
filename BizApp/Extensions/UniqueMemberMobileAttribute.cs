using DataLayer.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BizApp.Extensions
{
	public class UniqueMemberMobileAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var _context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
			var entity = _context.Users.SingleOrDefault(e => e.Mobile == Convert.ToInt64(value));

			if (entity != null)
			{
				return new ValidationResult(GetErrorMessage(value.ToString()));
			}
			return ValidationResult.Success;
		}

		public string GetErrorMessage(string email)
		{
			return $"شماره موبایل تکراری است";
		}
	}
}
