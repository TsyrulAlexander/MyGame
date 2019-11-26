using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework.Input;

namespace MyGame.Network {
	public static class NetworkUtilities {
		public static byte[] ToBytes(this object obj, Func<BinaryFormatter, SurrogateSelector> selector = null) {
			var bf = new BinaryFormatter();
			if (selector != null) {
				bf.SurrogateSelector = selector(bf);
			}
			bf.AssemblyFormat = FormatterAssemblyStyle.Full;
			using (var ms = new MemoryStream()) {
				bf.Serialize(ms, obj);
				return ms.ToArray();
			}
		}

		public static object GetMessage(this NetworkStream stream, Func<BinaryFormatter, SurrogateSelector> selector = null) {
			var formatter = new BinaryFormatter();
			if (selector != null) {
				formatter.SurrogateSelector = selector(formatter);
			}
			formatter.AssemblyFormat = FormatterAssemblyStyle.Full;
			var obj = formatter.Deserialize(stream);
			return obj;
		}
	}
}
