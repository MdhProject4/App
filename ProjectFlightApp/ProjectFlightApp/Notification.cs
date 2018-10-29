﻿namespace ProjectFlightApp
{
	public class Notification
	{
		/// <summary>
		/// Notification ID
		/// </summary>
		public string Id       { get; set; }
		
		/// <summary>
		/// Username the notification belongs to
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Flight being notified about
		/// </summary>
		public string FlightId { get; set; }

		/// <summary>
		/// If the user has been notified already
		/// </summary>
		public bool Notified   { get; set; }
	}
}