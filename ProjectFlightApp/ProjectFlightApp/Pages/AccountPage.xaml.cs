using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectFlightApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountPage
	{
		public AccountPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			// Set values on page
			LabelUsername.Text = $"{Account.Username} ({Account.Cookie})";
		}

		private async void ButtonTestLogin_OnClicked(object sender, EventArgs e)
		{
			var response = Account.Request("user");

			await DisplayAlert("Login", response, "Dismiss");
		}

		private async void ButtonSwitchAccount_OnClicked(object sender, EventArgs e) => 
			await Navigation.PushAsync(new NavigationPage(new LoginPage()));
	}
}