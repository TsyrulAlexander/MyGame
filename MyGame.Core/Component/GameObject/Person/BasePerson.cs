using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Core.Component.Texture;

namespace MyGame.Core.Component.GameObject.Person {
	[Serializable]
	public abstract class BasePerson : TextureGameObject {
		private float _speed = 1;
		protected Keys LeftKey { get; set; }
		protected Keys RightKey { get; set; }
		protected Keys UpKey { get; set; }
		protected Keys DownKey { get; set; }
		protected float Speed {
			get => _speed;
			set {
				_speed = value;
				OnPropertyChanged();
			}
		}
		public BasePerson(SerializationInfo info, StreamingContext context) : base(info, context) {
			
		}
		public BasePerson(Texture2D texture) : base(texture) { }
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			var keyboardState = Keyboard.GetState();
			if (keyboardState.IsKeyDown(LeftKey)) {
				OnLeft(gameTime);
			}
			if (keyboardState.IsKeyDown(RightKey)) {
				OnRight(gameTime);
			}
			if (keyboardState.IsKeyDown(UpKey)) {
				OnUp(gameTime);
			}
			if (keyboardState.IsKeyDown(DownKey)) {
				OnDown(gameTime);
			}
		}

		protected virtual void OnLeft(GameTime gameTime) { }
		protected virtual void OnRight(GameTime gameTime) { }
		protected virtual void OnUp(GameTime gameTime) { }
		protected virtual void OnDown(GameTime gameTime) { }
	}
}
