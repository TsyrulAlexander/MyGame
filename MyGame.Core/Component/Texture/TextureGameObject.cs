using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Core.Component.GameObject;
using MyGame.Core.Scene;

namespace MyGame.Core.Component.Texture
{
	[Serializable]
	public class TextureGameObject : BaseGameObject {
		private readonly ContentManager _contentManager;
		private string _textureName;
		protected SpriteBatch SpriteBatch { get; } = GameServices.GetService<SpriteBatch>();
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
		public TextureGameObject(string textureName) {
			TextureName = textureName;
			_contentManager = SceneManager.CurrentScene.ContentManager;
		}
		protected override void OnPropertyChanged([CallerMemberName]string propertyName = null) {
			if (propertyName == nameof(TextureName)) {
				Texture = _contentManager.Load<Texture2D>(TextureName);
				Size = new Vector2(Texture.Width, Texture.Height);
			}
			base.OnPropertyChanged(propertyName);
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
