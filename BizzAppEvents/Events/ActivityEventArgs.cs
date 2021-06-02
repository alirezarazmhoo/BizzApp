using System;

namespace DataLayer.Events
{
	public class ActivityEventArgs : EventArgs
	{
		public ActivityEventArgs(string table)
		{
			Table = table;
		}
		public string Id { get; set; }
		public string Table { get; set; }
	}
}
