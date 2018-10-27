using System;
using System.Net;

namespace ProjectFlightApp
{
	/// <summary>
	/// Extension of <see cref="WebClient"/> which adds support for cookie storage
	/// </summary>
	public class CookieWebClient : WebClient
	{
		// Public way to access cookies
		public CookieContainer CookieContainer { get; }

		// Add to constructor and create the cookie container
		public CookieWebClient() => CookieContainer = new CookieContainer();

		// Override GetWebRequest to steal the cookies
		protected override WebRequest GetWebRequest(Uri address)
		{
			var request = base.GetWebRequest(address) as HttpWebRequest;

			if (request != null)
				request.CookieContainer = CookieContainer;

			return request;
		}
	}
}