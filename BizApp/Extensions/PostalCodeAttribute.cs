using System.ComponentModel.DataAnnotations;

namespace BizApp.Extensions
{
	public class PostalCodeAttribute : ValidationAttribute
	{
        private const string pattern = @"^([0-9]{10})$";

        public PostalCodeAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var regex = new RegularExpressionAttribute(pattern);
            return regex.IsValid(value);
        }
    }
}
