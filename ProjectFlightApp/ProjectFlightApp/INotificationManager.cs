namespace ProjectFlightApp
{
	public interface INotificationManager
	{
		/// <summary>
		/// Request notifications permission
		/// </summary>
		/// <returns>If the request was accepted</returns>
		bool RequestPermission();

		/// <summary>
		/// Sends a notification to the device
		/// </summary>
		/// <param name="message">Message to display</param>
		void Send(string message);
	}
}