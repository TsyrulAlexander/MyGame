using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Core.Component.GameObject;

namespace MyGame.Core.Component.Texture
{
	[Serializable]
	public class TextureGameObject : BaseGameObject {
		protected SpriteBatch SpriteBatch { get; } = GameServices.GetService<SpriteBatch>();
		public Texture2D Texture { get; set; }
		public TextureGameObject(SerializationInfo info, StreamingContext context): base(info, context) {
			
		}
		public TextureGameObject(Texture2D texture) {
			Texture = texture;
		}
		
		public override void Draw( GameTime gameTime) {
			base.Draw(gameTime);
			if (!Visible || Texture == null) {
				return;
			}
			SpriteBatch.Draw(Texture, Position + Origin, SourceRectangle,
				Color, Rotation, Origin, Scale, SpriteEffects.None, ZIndex);
		}
	}
}
