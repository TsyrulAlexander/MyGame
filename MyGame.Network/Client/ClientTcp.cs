using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MyGame.Network.Entities;

namespace MyGame.Network.Client {
	public class ClientTcp {
		private TcpClient _client;
		private NetworkStream _stream;
		public event Action<NetworkItem> Message;
		public void Connect(string ipAddress, int port) {
			_client = new TcpClient(ipAddress, port);
			_stream = _client.GetStream();
			Task.Run(() => SubscribeMessage(_stream));
		}
		public void Send<T>(T value) where T : NetworkItem {
			var data = value.ToBytes();
			_stream.Write(data, 0, data.Length);
		}
		protected virtual void SubscribeMessage(NetworkStream stream) {
			try {
				while (true) {
					if (!stream.DataAvailable)
						continue;
					var message = stream.GetMessage();
					OnMessage(message as NetworkItem);
				}
			} catch {
				Disconnect();
				throw;
			}
		}
		public void Disconnect() {
			_stream?.Close();
			_client?.Close();
		}
		protected virtual void OnMessage(NetworkItem obj) {
			Message?.Invoke(obj);
		}
	}
}