using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using MyGame.Network.Entities;

namespace MyGame.Network.Client {
	public class ClientTcp {
		private TcpClient _client;
		private NetworkStream _stream;
		public event Action<INetworkItem> Message;
		public Func<BinaryFormatter, SurrogateSelector> Selector;
		public void Connect(string ipAddress, int port, Func<BinaryFormatter, SurrogateSelector> selectorFunc = null) {
			Selector = selectorFunc;
			_client = new TcpClient(ipAddress, port);
			_stream = _client.GetStream();
			Task.Run(() => SubscribeMessage(_stream));
		}
		public void Send<T>(T value) where T : INetworkItem {
			var data = value.ToBytes(Selector);
			_stream.Write(data, 0, data.Length);
		}
		protected virtual void SubscribeMessage(NetworkStream stream) {
			try {
				while (true) {
					if (!stream.DataAvailable)
						continue;
					var message = stream.GetMessage(Selector);
					OnMessage(message as INetworkItem);
				}
			} catch (Exception ex) {
				Disconnect();
				throw;
			}
		}
		public void Disconnect() {
			_stream?.Close();
			_client?.Close();
		}
		protected virtual void OnMessage(INetworkItem obj) {
			Message?.Invoke(obj);
		}
	}
}