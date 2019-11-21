using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

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
					try {
						var message = GetMessage(Stream);
						Server.BroadcastMessage(message, Id);
					} catch {
						break;
					}
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
		protected virtual object GetMessage(NetworkStream stream) {
			var formatter = new BinaryFormatter();
			var obj = formatter.Deserialize(stream);
			return obj;
		}
	}
}