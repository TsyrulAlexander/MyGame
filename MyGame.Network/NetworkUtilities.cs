using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyGame.Network {
	public static class NetworkUtilities {
		public static byte[] ToBytes(this object obj) {
			var bf = new BinaryFormatter();
			using (var ms = new MemoryStream()) {
				bf.Serialize(ms, obj);
				return ms.ToArray();
			}
		}

		public static object GetMessage(this NetworkStream stream) {
			var formatter = new BinaryFormatter();
			var obj = formatter.Deserialize(stream);
			return obj;
		}
	}
}
