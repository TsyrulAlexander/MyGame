using System.Threading.Tasks;
using MyGame.Network.Server;

namespace MyGame.Server.Console {
	class Program {
		static void Main(string[] args) {
			System.Console.WriteLine("Start");
			var serverTcp = new ServerTCP();
			try {
				Task.Run(serverTcp.Listen);
				System.Console.ReadKey();
			} finally {
				serverTcp.Disconnect();
			}
		}
	}
}