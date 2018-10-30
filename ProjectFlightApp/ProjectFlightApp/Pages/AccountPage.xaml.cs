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
		public AccountPage(MainPage main)
		{
			InitializeComponent();

			ViewNotifications.ItemSelected += async (sender, args) =>
			{
				await Navigation.PopModalAsync();
				main.ToPlane(((Notification) ViewNotifications.SelectedItem).FlightId);
			};
		}

		/// <summary>
		/// Notifications shown in the notifications view
		/// </summary>
		private ObservableCollection<Notification> notifications;

		/// <summary>
		/// Bookmarks shown in the notifications view
		/// </summary>
		private ObservableCollection<Bookmark> bookmarks;

		protected override void OnAppearing()
		{
			base.OnAppearing();

			// Set values on page
			LabelUsername.Text = $"Welcome {Account.Username}!";

			// Update notifications list
			var notificationsList = JsonConvert.DeserializeObject<Notification[]>(Account.Request("user/getNotifications"));

			// Update bookmarks list
			var bookmarksList = JsonConvert.DeserializeObject<Bookmark[]>(Account.Request("user/getSavedFlights"));

			// Set item sources
			notifications = new ObservableCollection<Notification>(notificationsList);
			ViewNotifications.ItemsSource = notifications;

			bookmarks = new ObservableCollection<Bookmark>(bookmarksList);
			ViewBookmarks.ItemsSource = bookmarks;
		}

		private async void ButtonSwitchAccount_OnClicked(object sender, EventArgs e) => 
			await Navigation.PushAsync(new NavigationPage(new LoginPage()));

		private void ButtonBack_OnClicked(object sender, EventArgs e) => Navigation.PopModalAsync();
	}
}