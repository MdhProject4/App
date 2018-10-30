using Windows.UI.Notifications;
using ProjectFlightApp.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationManager))]
namespace ProjectFlightApp.UWP
{
	public class NotificationManager : INotificationManager
	{
		// UWP requires no special permission
		public void RequestPermission() { }

		public void Send(string message)
		{
			// Setup notification
			var notifier = ToastNotificationManager.CreateToastNotifier();
			var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);
			var toastNodes = toastXml.GetElementsByTagName("text");
			
			// Add text
			toastNodes.Item(1)?.AppendChild(toastXml.CreateTextNode(message));

			// Display it
			var toast = new ToastNotification(toastXml);
			notifier.Show(toast);
		}
	}
}