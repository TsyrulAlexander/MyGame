using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Core.Component.GameObject {

	public interface IGameObject : INotifyPropertyChanged, IUpdateable, IDrawable
	{
		Guid Id { get; set; }
		Vector2 Position { get; set; }
		Vector2 Size { get; set; }
		float Rotation { get; set; }
		Color Color { get; set; }
	}
}