using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectFlightApp
{
	public class WebSocketManager : IDisposable
	{
		/// <summary>
		/// Main client
		/// </summary>
		private readonly ClientWebSocket client;

		/// <summary>
		/// Probably not needed
		/// </summary>
		private readonly CancellationTokenSource cts;

		/// <summary>
		/// If we're connected to the server
		/// </summary>
		public bool Connected => client.State == WebSocketState.Open;

		/// <summary>
		/// If the main 'wait for server' loop should be running
		/// </summary>
		private bool running;

		/// <summary>
		/// Notification manager to send local notifications
		/// </summary>
		private readonly INotificationManager notificationManager;

		public WebSocketManager(Uri uri)
		{
			client  = new ClientWebSocket();
			cts     = new CancellationTokenSource();
			running = true;

			notificationManager = DependencyService.Get<INotificationManager>();

			ConnectAsync(uri);
		}

		private async void ConnectAsync(Uri uri)
		{
			// Add cookies from account
			client.Options.Cookies = Account.Cookies;

			// Connect
			await client.ConnectAsync(uri, cts.Token);

			// Send message so we're added to the server
			// TODO: We probably want to send the username
			Send("HI");

			// Continue listening for the server
			await Task.Run(async () =>
			{
				// Buffer for responses
				var buffer = new byte[16];

				// Temporary variable to get result
				WebSocketReceiveResult result = null;

				while (running)
				{
					// Wait for server to send something
					result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token);

					// Get response string
					var response = Encoding.ASCII.GetString(buffer).TrimEnd('\0');

					// Check so it's an actual plane notification
					if (!response.StartsWith("OK") && !response.StartsWith("ERR"))
					{
						using (var webClient = new WebClient())
						{
							// When download finished
							webClient.DownloadStringCompleted += (sender, args) =>
							{
								// Parse response
								var info = JsonConvert.DeserializeObject<JsonFlightInfoResult>(args.Result).Info;

								// Basic error checking
								if (info == null)
									return;

								// Send notification
								notificationManager.Send($"Plane {info.Id} from {info.DepartureCountry} should land in {info.DestinationCountry} in about an hour");
							};

							// Start download in background
							webClient.DownloadStringAsync(new Uri($"http://web.kraxarn.com:5000/api/getFlight/{response}"));
						}
					}
				}

				// Send close to server
				if (result?.CloseStatus != null)
					await client.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, cts.Token);
			});
		}

		private async void Send(string message) => 
			await client.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(message)), WebSocketMessageType.Text, true, cts.Token);

		public void Dispose() => running = false;
	}
}