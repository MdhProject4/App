using ProjectFlightApp.iOS;
using UserNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationManager))]
namespace ProjectFlightApp.iOS
{
	public class NotificationManager : INotificationManager
	{
		public void RequestPermission()
		{
			// Request permission
			UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => {});

			// Watch for notifications while the app is open
			UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
		}

		public void Send(string message)
		{
			var content = new UNMutableNotificationContent
			{
				Title = "Incoming Flight!",
				Body  = message,
				Sound = UNNotificationSound.Default
			};

			var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);
			var request = UNNotificationRequest.FromIdentifier("request0", content, trigger);

			UNUserNotificationCenter.Current.AddNotificationRequest(request, err => { });
		}
	}
}