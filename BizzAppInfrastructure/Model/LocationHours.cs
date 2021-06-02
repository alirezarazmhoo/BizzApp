using DomainClass.Businesses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizzAppInfrastructure.Model
{
	public class LocationHours
	{
		public WeekDaysEnum Day { get; set; }
		public string DayName { get; set; }
		public TimeSpan? FromTime { get; set; }
		public TimeSpan? ToTime { get; set; }
	}
}
