using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass.Businesses
{
	public class BusinessTime
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public Guid BusinessId { get; set; }
		public Business Business { get; set; }
		[Required]
		public WeekDaysEnum Day { get; set; }
		[Required]
		[Column(TypeName = "time(1)")]
		public TimeSpan FromTime { get; set; }
		[Column(TypeName = "time(1)")]
		public TimeSpan? ToTime { get; set; }
	}

	public enum WeekDaysEnum
	{
		Saturday = 1,
		Sunday = 2,
		Monday = 3,
		Tuesday = 4,
		Wednesday = 5,
		Thursday = 6,
		Friday = 7
	}
}
