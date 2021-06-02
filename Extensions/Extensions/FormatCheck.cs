using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Extensions
{
	public static class FormatCheck
	{

	
		public static MediaType GetFormat(string url)
		{
			string[] _UrlList = new string[] { };
			_UrlList = url.Split(".");
			var lstitem = _UrlList[_UrlList.Length - 1];
			if (string.Equals(lstitem, "AVI", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "MP4", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "DIVX", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "WMV", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "mkv", StringComparison.OrdinalIgnoreCase))
			{
				return MediaType.Video;
			}
			else if (string.Equals(lstitem, "jpg", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "jpeg", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "DIVX", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "pjpeg", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "gif", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "png", StringComparison.OrdinalIgnoreCase) ||
				  string.Equals(lstitem, "x-png", StringComparison.OrdinalIgnoreCase))
			{
				return MediaType.Picture;
			}
			else
			{
				return MediaType.Undefined;
			}


		}

	}
}
