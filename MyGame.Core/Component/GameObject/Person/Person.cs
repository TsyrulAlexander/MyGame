using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MyGame.Core.Component.Texture;

namespace MyGame.Core.Component.GameObject.Person {
	[Serializable]
	public class Person : TextureGameObject {
		float speed = 5f;
		public Person(SerializationInfo info, StreamingContext context) : base(info, context) {
			
		}
		public Person(ContentManager contentManager, string textureName) : base(contentManager, textureName) { }
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			KeyboardState keyboardState = Keyboard.GetState();
			var isPositionChange = false;
			var position = Position;
			if (keyboardState.IsKeyDown(Keys.Left)) {
				position.X -= speed;
				isPositionChange = true;
			}
			if (keyboardState.IsKeyDown(Keys.Right)) {
				position.X += speed;
				isPositionChange = true;
			}
			if (keyboardState.IsKeyDown(Keys.Up)) {
				position.Y -= speed;
				isPositionChange = true;
			}
			if (keyboardState.IsKeyDown(Keys.Down)) {
				position.Y += speed;
				isPositionChange = true;
			}
			if (isPositionChange) {
				Position = position;
			}
		}
	}
}
