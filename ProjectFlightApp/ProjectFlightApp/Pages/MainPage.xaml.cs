using System;
using Xamarin.Forms;

namespace ProjectFlightApp.Pages
{
	public partial class MainPage
	{
		public MainPage()
		{
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
	}
}