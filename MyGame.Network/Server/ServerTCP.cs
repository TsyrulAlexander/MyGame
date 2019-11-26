using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MyGame.Network.Server
{
	public class ServerTCP
	{
		static TcpListener _tcpListener;
		protected List<ClientItem> Clients { get; } = new List<ClientItem>();
		protected internal void AddConnection(ClientItem clientItem) {
			Clients.Add(clientItem);
		}
		protected internal void RemoveConnection(Guid id) {
			var client = Clients.FirstOrDefault(c => c.Id == id);
			if (client != null)
				Clients.Remove(client);
		}
		protected internal void BroadcastMessage(byte[] data, Guid senderId) {
			if (data.Length == 0) {
				return;
			}
			foreach (var client in Clients) {
				if (client.Id != senderId) {
					client.Stream.Write(data, 0, data.Length);
				}
			}
		}
		//protected internal void BroadcastMessage(object message, Guid senderId) {
		//	var data = message.ToBytes();
		//	foreach (var client in Clients) {
		//		if (client.Id != senderId) {
		//			client.Stream.Write(data, 0, data.Length);
		//		}
		//	}
		//}
		public void Listen() {
			try {
				_tcpListener = new TcpListener(IPAddress.Any, 8888);
				_tcpListener.Start();
				while (true) {
					var tcpClient = _tcpListener.AcceptTcpClient();
					var clientObject = new ClientItem(tcpClient, this);
					Task.Run(clientObject.Process);
				}
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
				Disconnect();
			}
		}
		public void Disconnect() {
			_tcpListener?.Stop();
			for (var i = 0; i < Clients.Count; i++) {
				Clients[i].Close();
			}
		}
	}
}
