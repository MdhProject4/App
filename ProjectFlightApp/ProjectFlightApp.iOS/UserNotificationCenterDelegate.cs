using System;
using UserNotifications;

namespace ProjectFlightApp.iOS
{
	public class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
	{
		public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler) => 
			completionHandler(UNNotificationPresentationOptions.Alert);
	}
}