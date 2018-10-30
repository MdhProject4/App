using Xamarin.Forms;

namespace ProjectFlightApp
{
	public class Bookmark
	{
		/// <summary>
		/// ID of the bookmark used by the database
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Username of the user who saved the bookmark
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// ID of the flight
		/// </summary>
		public string SavedId { get; set; }

		public EBookmarkType Type { get; set; }

		/// <summary>
		/// Icon. either plane or landing plane depending on type
		/// </summary>
		public ImageSource Icon => $"images/ui/{(Type == EBookmarkType.Plane ? "airplane" : "airplane-landing")}.png";
	}
}