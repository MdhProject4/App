using Xamarin.Forms.Xaml;

namespace ProjectFlightApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage
	{
		public MapPage()
		{
			InitializeComponent();

			// Hide the default UI once the page is loaded
			WebViewMap.Navigated += async (sender, args) => { await WebViewMap.EvaluateJavaScriptAsync("hideUi()"); };
		}
	}
}