using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectFlightApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountPage
	{
		public AccountPage() => InitializeComponent();

		/// <summary>
		/// Notifications shown in the notifications view
		/// </summary>
		private ObservableCollection<Notification> notifications;

		protected override void OnAppearing()
		{
			base.OnAppearing();

			// Set values on page
			LabelUsername.Text = $"Welcome {Account.Username}!";

			// Update notifications list
			var notificationsList = JsonConvert.DeserializeObject<Notification[]>(Account.Request("user/getNotifications"));

			// Set item sources
			notifications = new ObservableCollection<Notification>(notificationsList);
			ViewNotifications.ItemsSource = notifications;
		}

		private async void ButtonTestLogin_OnClicked(object sender, EventArgs e)
		{
			var response = JsonConvert.DeserializeObject<JsonUserResult>(Account.Request("user"));

			await DisplayAlert("Login", $"{response}", "Dismiss");
		}

		private async void ButtonSwitchAccount_OnClicked(object sender, EventArgs e) => 
			await Navigation.PushAsync(new NavigationPage(new LoginPage()));

		private void ButtonBack_OnClicked(object sender, EventArgs e) => Navigation.PopModalAsync();
	}
}