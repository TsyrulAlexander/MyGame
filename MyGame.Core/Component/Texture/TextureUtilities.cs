using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Core.Component.Texture {
	public class TextureUtilities {
		public static Texture2D CreateTexture(GraphicsDevice device, int width, int height, Color color) {
			var texture = new Texture2D(device, width, height);
			texture.SetData(new[] {
				color
			});
			return texture;
		}
	}
}
