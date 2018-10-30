using System;
using ProjectFlightApp.iOS;
using UserNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationManager))]
namespace ProjectFlightApp.iOS
{
	public class NotificationManager : INotificationManager
	{
		public bool RequestPermission()
		{
			// Temporary variable
			var accepted = false;

			// Request permission
			UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => accepted = true);

			// Return response
			return accepted;
		}

		public void Send(string message)
		{
			var content = new UNMutableNotificationContent
			{
				Title = "title",
				Subtitle = "subtitle",
				Body = message
			};

			var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);

			var request = UNNotificationRequest.FromIdentifier("request0", content, trigger);

			UNUserNotificationCenter.Current.AddNotificationRequest(request, err =>
			{
				if (err != null)
					throw new InvalidOperationException($"Failed to send notification: {err}");
			});
		}
	}
}