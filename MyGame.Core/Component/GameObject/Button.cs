using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MyGame.Core.Component.Texture;
using MyGame.Network.Entities;

namespace MyGame.Core.Component.GameObject {
	[Serializable]
	public class Button : TextureGameObject {
		private MouseState _lastMouseState;
		private MouseState _currentMouseState;
		public event Action<EventArgs> Click;
		public Button(ContentManager contentManager, string textureName) : base(contentManager, textureName) { }
		public Button(SerializationInfo info, StreamingContext context) : base(info, context) {

		}
		public override void Update(GameTime gameTime) {
			_lastMouseState = _currentMouseState;
			base.Update(gameTime);
			_currentMouseState = Mouse.GetState();
			if (_lastMouseState.LeftButton == ButtonState.Released &&
				_currentMouseState.LeftButton == ButtonState.Pressed) {
				if (PositionRectangle.Intersects(new Rectangle(_lastMouseState.Position, new Point(1, 1)))) {
					OnClick();
				}
			}
		}
		protected virtual void OnClick() {
			Click?.Invoke(EventArgs.Empty);
		}
	}
}
