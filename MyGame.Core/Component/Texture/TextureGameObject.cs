using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Core.Component.GameObject;

namespace MyGame.Core.Component.Texture
{
	[Serializable]
	public class TextureGameObject : BaseGameObject {
		private readonly ContentManager _contentManager;
		private string _textureName;
		public string TextureName {
			get => _textureName;
			set {
				_textureName = value;
				OnPropertyChanged();
			}
		}
		public Texture2D Texture { get; set; }
		public TextureGameObject(SerializationInfo info, StreamingContext context): base(info, context) {
			
		}
		public TextureGameObject(ContentManager contentManager, string textureName) {
			_contentManager = contentManager;
			TextureName = textureName;
		}
		protected override void OnPropertyChanged([CallerMemberName]string propertyName = null) {
			if (propertyName == nameof(TextureName)) {
				Texture = _contentManager.Load<Texture2D>(TextureName);
				Size = new Vector2(Texture.Width, Texture.Height);
			}
			base.OnPropertyChanged(propertyName);
		}
		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			base.Draw(spriteBatch, gameTime);
			if (!IsVisible || Texture == null) {
				return;
			}
			spriteBatch.Draw(Texture, Position + Origin, SourceRectangle,
				Color, Rotation, Origin, Scale, SpriteEffects.None, ZIndex);
		}
	}
}
