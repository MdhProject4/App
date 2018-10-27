using Xamarin.Forms;

namespace ProjectFlightApp.Pages
{
	public partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();

			// Add tab icons on iOS
			if (Device.RuntimePlatform == Device.iOS)
			{
				PageMap.Icon     = "images/ui/map.png";
				PageAccount.Icon = "images/ui/account.png";
				PageSearch.Icon  = "images/ui/search.png";
			}
		}
	}
}