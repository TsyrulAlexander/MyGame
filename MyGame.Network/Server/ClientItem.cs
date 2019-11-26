using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MyGame.Network.Server {
	public class ClientItem {
		protected internal Guid Id { get; private set; }
		protected internal NetworkStream Stream { get; private set; }
		protected readonly TcpClient Client;
		protected readonly ServerTCP Server;
		public ClientItem(TcpClient tcpClient, ServerTCP server) {
			Id = Guid.NewGuid();
			Client = tcpClient;
			Server = server;
			Server.AddConnection(this);
		}

		public void Process() {
			try {
				Stream = Client.GetStream();
				while (true) {
					var message = GetMessage(Stream);
					Server.BroadcastMessage(message, Id);
				}
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			} finally {
				Server.RemoveConnection(Id);
				Close();
			}
		}
		protected internal void Close() {
			Stream?.Close();
			Client?.Close();
		}
		protected virtual byte[] GetMessage(NetworkStream stream) {
			var data = new byte[1024];
			using (var ms = new MemoryStream()) {
				do {
					var bytes = Stream.Read(data, 0, data.Length);
					ms.Write(data, 0, bytes);
				}
				while (Stream.DataAvailable);
				return ms.ToArray();
			}
			
			//var data = new byte[1024];
			//using (var ms = new MemoryStream()) {
			//	int numBytesRead;
			//	while ((numBytesRead = stream.Read(data, 0, data.Length)) > 0) {
			//		ms.Write(data, 0, numBytesRead);
			//	}
			//	return ms.ToArray();
			//}
		}
	}
}