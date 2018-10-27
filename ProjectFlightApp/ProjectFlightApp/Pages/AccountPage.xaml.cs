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

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			// TODO: Temporarily always show login page
			await Navigation.PushAsync(new NavigationPage(new LoginPage()));
		}
	}
}