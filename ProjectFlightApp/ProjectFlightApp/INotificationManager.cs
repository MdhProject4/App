namespace ProjectFlightApp
{
	public interface INotificationManager
	{
		/// <summary>
		/// Request notifications permission
		/// </summary>
		void RequestPermission();

		/// <summary>
		/// Sends a notification to the device
		/// </summary>
		/// <param name="message">Message to display</param>
		void Send(string message);
	}
}