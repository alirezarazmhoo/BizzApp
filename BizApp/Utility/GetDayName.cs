using DomainClass.Businesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Utility
{
	public static class GetDayName
	{
		public static string GetName(WeekDaysEnum weekDaysEnum)
		{
			switch (weekDaysEnum)
			{
				case WeekDaysEnum.Saturday:
					return "شنبه";
				case WeekDaysEnum.Sunday:
					return "یکشنبه";
				case WeekDaysEnum.Monday:
					return "دوشنبه";
				case WeekDaysEnum.Tuesday:
					return "سه شنبه";
				case WeekDaysEnum.Wednesday:
					return "چهارشنبه";
				case WeekDaysEnum.Thursday:
					return "پنجشنبه";
				case WeekDaysEnum.Friday:
					return "جمعه";
				default:
					return "نامشخص";


			}





		}


	}
}
