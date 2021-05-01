using System;

namespace DataLayer.Extensions
{
	public class DuplicateFriendRequestException : InvalidOperationException
	{
		public DuplicateFriendRequestException(string message) : base(message)
		{			
		}
	}
}
