using System;
using Xamarin.Forms;

namespace ProjectFlightApp.Pages
{
	public partial class MainPage
	{
		public WebSocketManager WebSocket;

		public MainPage()
		{
			InitializeComponent();

			// Hide the default UI once the page is loaded
			WebViewMap.Navigated += async (sender, args) => { await WebViewMap.EvaluateJavaScriptAsync("hideUi()"); };

			// Try and connect to server
			if (Account.IsSignedIn)
				WebSocket = new WebSocketManager(new Uri("ws://localhost:5000/ws"));
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
	}
}