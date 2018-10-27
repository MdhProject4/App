using System;
using Xamarin.Forms.Xaml;

namespace ProjectFlightApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

		private async void CancelButton_OnClicked(object sender, EventArgs e)
		{
		}
	}
}