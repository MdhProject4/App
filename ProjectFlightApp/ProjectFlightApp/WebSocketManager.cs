using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

		public WebSocketManager(Uri uri)
		{
			client = new ClientWebSocket();
			cts = new CancellationTokenSource();
			running = true;

			ConnectAsync(uri);
		}

		private async void ConnectAsync(Uri uri)
		{
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

					// TODO: Use buffer to determine what was received
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