using System;
using Xamarin.Forms;

namespace ProjectFlightApp.Pages
{
	public partial class MainPage
	{
		public WebSocketManager WebSocket;

		public MainPage()
		{
			// Load main layout
			InitializeComponent();

			// Hide the default UI once the page is loaded
			WebViewMap.Navigated += async (sender, args) => { await WebViewMap.EvaluateJavaScriptAsync("hideUi()"); };
		}

		private async void ButtonAccount_OnTapped(object sender, EventArgs e)
		{
			if (Account.IsSignedIn)
				await Navigation.PushModalAsync(new NavigationPage(new AccountPage()));
			else
				await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
		}

		/// <summary>
		/// Moves map to plane
		/// </summary>
		/// <param name="id"></param>
		public async void ToPlane(string id) => 
			await WebViewMap.EvaluateJavaScriptAsync($"getFlight('{id}')");

		private void EntrySearch_OnCompleted(object sender, EventArgs e) => 
			ToPlane(EntrySearch.Text.Trim());

		protected override void OnAppearing()
		{
			base.OnAppearing();

			// If not already connected and signed in, try to connect to web socket server
			if (WebSocket == null && Account.IsSignedIn)
			{
				DependencyService.Get<INotificationManager>().RequestPermission();
				WebSocket = new WebSocketManager(new Uri("ws://web.kraxarn.com:5000/ws"));
			}
		}
	}
}