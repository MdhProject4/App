using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectFlightApp
{
	public class WebSocketManager : INotifyPropertyChanged
	{
		private readonly ClientWebSocket client;
		private readonly CancellationTokenSource cts;

		public bool Connected => client.State == WebSocketState.Open;

		public WebSocketManager(Uri uri)
		{
			client = new ClientWebSocket();
			cts = new CancellationTokenSource();

			ConnectAsync(uri);
		}

		private async void ConnectAsync(Uri uri)
		{
			await client.ConnectAsync(uri, cts.Token);

			UpdateState();

			await Task.Factory.StartNew(async () =>
			{
				while (true)
				{
					WebSocketReceiveResult result;
					var message = new ArraySegment<byte>(new byte[4096]);
					do
					{
						result = await client.ReceiveAsync(message, cts.Token);
						var bytes = message.Skip(message.Offset).Take(result.Count).ToArray();
						var messageString = Encoding.UTF8.GetString(bytes);

						Debug.WriteLine($"Message: {messageString}"); // TODO

					} while (!result.EndOfMessage);
				}
			}, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
		}

		public async void Send(string message) => 
			await client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, cts.Token);

		private void UpdateState()
		{
			OnPropertyChanged(nameof(Connected));
			Debug.WriteLine($"WebSocket State: {client.State}");
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propertyName = null) => 
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}