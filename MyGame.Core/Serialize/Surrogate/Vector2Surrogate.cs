using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace MyGame.Core.Serialize.Surrogate
{
	public class Vector2Surrogate: ISerializationSurrogate
	{
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) {
			var value = (Vector2) obj;
			info.AddValue("x", value.X, typeof(float));
			info.AddValue("y", value.Y, typeof(float));
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {
			var x = info.GetSingle("x");
			var y = info.GetSingle("y");
			return new Vector2(x, y);
		}
	}
}
