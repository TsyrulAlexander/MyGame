using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Component.GameObject;

namespace MyGame.Component
{
	class TextureGameObject : BaseGameObject {
		public GraphicsDevice Device { get; }
		public Texture2D Texture { get; set; }
		public TextureGameObject(GraphicsDevice device) {
			Device = device;
		}
		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			base.Draw(spriteBatch, gameTime);
			spriteBatch.Draw(Texture, Position, destinationRectangle, Color, Rotation, origin, Scale, SpriteEffects.None, ZIndex);
		}
	}
}
