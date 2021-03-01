using System.ComponentModel.DataAnnotations;

namespace BizApp.Extensions
{
	public class MobileAttribute : ValidationAttribute
	{
        private const string pattern = @"^([0][9][0-9]{9})|([9][0-9]{9})$";

        public MobileAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var regex = new RegularExpressionAttribute(pattern);
            return regex.IsValid(value);
        }
    }
}
