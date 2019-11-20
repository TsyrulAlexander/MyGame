using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Component.GameObject.Event;

namespace MyGame.Component.GameObject
{
	public interface IGameObject {
		event Action<PositionChangeEventArgs> PositionChange;
		event Action<SizeChangeEventArgs> SizeChange;
		Vector2 Position { get; set; }
		Vector2 Size { get; set; }
		float Rotation { get; set; }
		Color Color { get; set; }
		void Draw(SpriteBatch spriteBatch, GameTime gameTime);
		void Update(GameTime gameTime);
	}
}
