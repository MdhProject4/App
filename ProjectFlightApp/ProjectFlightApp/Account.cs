using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace ProjectFlightApp
{
	/// <summary>
	/// Just to keep track of the current user signed in
	/// </summary>
	public static class Account
	{
		#region Public

		/// <summary>
		/// If the user is signed in (username set)
		/// </summary>
		public static bool IsSignedIn => !string.IsNullOrEmpty(Username);

		/// <summary>
		/// Username of currently signed in user
		/// </summary>
		public static string Username
		{
			get
			{
				// Check if username is set
				if (_username != null)
					return _username;

				// Get from properties and set private username
				if (Properties.ContainsKey("username"))
				{
					_username = Properties["username"] as string;
					return _username;
				}

				// Otherwise, username isn't set yet
				return null;
			}

			set
			{
				// Set private variable
				_username = value;

				// Save to properties
				Properties["username"] = value;
			}
		}

		/// <summary>
		/// Cookie assigned with current login
		/// </summary>
		public static CookieContainer Cookies
		{
			get
			{
				// Check if username is set
				if (_cookies != null)
					return _cookies;

				// Get from properties and set private username
				if (Properties.ContainsKey("cookie"))
				{
					// TODO: Not sure if this works
					var cookie = Properties["cookie"] as string;
					_cookies = new CookieContainer();
					_cookies.Add(new Uri("http://localhost:5000"), new Cookie(".AspNetCore.Cookies", cookie));

					return _cookies;
				}

				// Otherwise, username isn't set yet
				return null;
			}

			set
			{
				// Set private variable
				_cookies = value;

				// Save to file
				Properties["cookie"] = Cookie;
			}
		}

		#endregion

		#region Private

		private static IDictionary<string, object> Properties => Application.Current.Properties;

		private static string _username;

		private static CookieContainer _cookies;

		/// <summary>
		/// Shorthand for getting the only cookie we use atm
		/// </summary>
		public static string Cookie => Cookies?.GetCookies(new Uri("http://localhost:5000"))[0].Value;

		#endregion

		/// <summary>
		/// Save (all properties) changes
		/// </summary>
		public static async void Save() => await Application.Current.SavePropertiesAsync();

		/// <summary>
		/// Requests using the account cookies
		/// </summary>
		/// <param name="path">Path to the requested url (like user)</param>
		/// <returns></returns>
		public static string Request(string path)
		{
			using (var client = new WebClient())
			{
				client.Headers.Add(HttpRequestHeader.Cookie, $".AspNetCore.Cookies={Cookie}");
				return client.DownloadString($"http://localhost:5000/{path}");
			}
		}
	}
}