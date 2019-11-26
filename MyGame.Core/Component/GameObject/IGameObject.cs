using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Core.Component.GameObject.Event;

namespace MyGame.Core.Component.GameObject {

	public interface IGameObject : INotifyPropertyChanged {
		event Action<PositionChangeEventArgs> PositionChange;
		event Action<SizeChangeEventArgs> SizeChange;
		bool IsSynchronize { get; set; }
		Guid Id { get; set; }
		Vector2 Position { get; set; }
		Vector2 Size { get; set; }
		float Rotation { get; set; }
		Color Color { get; set; }
		bool IsVisible { get; set; }
		void Draw(SpriteBatch spriteBatch, GameTime gameTime);
		void Update(GameTime gameTime);
	}
}