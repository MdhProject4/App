using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectFlightApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage
	{
		public LoginPage() => InitializeComponent();

		/// <summary>
		/// Username/password entry changed, used to enable/disable login button
		/// </summary>
		private void Entry_OnTextChanged(object sender, TextChangedEventArgs e) => 
			ButtonLogin.IsEnabled = EntryUsername.Text != null && EntryPassword.Text != null;

		private async void ButtonLogin_OnClicked(object sender, EventArgs e)
		{
			var username = EntryUsername.Text;
			var password = EntryPassword.Text;

			using (var client = new CookieWebClient())
			{
				var response = client.DownloadString($"http://web.kraxarn.com:5000/user/login?username={username}&password={password}");
				var json = JsonConvert.DeserializeObject<JsonResult>(response);

				if (json.Error)
					await DisplayAlert("Login failed", "Invalid username, password or you put your sock on backwards", "uh...");
				else
				{
					// Cookies received
					Account.Cookies = client.CookieContainer;

					// Username used
					Account.Username = username;

					// Save changes
					Account.Save();

					// Go back to account page
					await Navigation.PopModalAsync();
				}
			}
		}

		private async void ButtonCancel_OnClicked(object sender, EventArgs e) => await Navigation.PopModalAsync();
	}
}