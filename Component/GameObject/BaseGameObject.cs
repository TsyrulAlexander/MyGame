using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Component.GameObject.Event;

namespace MyGame.Component.GameObject {
	public class BaseGameObject : IGameObject {

		#region Fields: Private

		private Vector2 _position;
		private Vector2 _size;
		protected Rectangle destinationRectangle;
		protected Vector2 origin;

		#endregion

		#region Properties: Public

		public event Action<PositionChangeEventArgs> PositionChange;
		public event Action<SizeChangeEventArgs> SizeChange;
		public virtual Vector2 Position {
			get => _position;
			set {
				var oldValue = _position;
				_position = value;
				OnPositionChange(oldValue, value);
			}
		}
		public virtual Vector2 Size {
			get => _size;
			set {
				var oldValue = _size;
				_size = value;
				OnSizeChange(oldValue, value);
			}
		}
		public virtual float Rotation { get; set; }
		public Color Color { get; set; } = Color.White;
		public int ZIndex { get; set; }
		public int Scale { get; set; } = 1;

		#endregion

		#region Method: Public

		public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) { }
		public virtual void Update(GameTime gameTime) { }

		#endregion

		#region Method: Protected

		protected virtual void OnPositionChange(Vector2 oldValue, Vector2 newValue) {
			CalculateDestinationRectangle();
			PositionChange?.Invoke(new PositionChangeEventArgs {
				GameObject = this,
				OldValue = oldValue,
				NewValue = newValue
			});
		}
		protected virtual void OnSizeChange(Vector2 oldValue, Vector2 newValue) {
			CalculateDestinationRectangle();
			CalculateOriginRectangle();
			SizeChange?.Invoke(new SizeChangeEventArgs {
				GameObject = this,
				OldValue = oldValue,
				NewValue = newValue
			});
		}
		protected virtual void CalculateOriginRectangle() {
			origin = new Vector2(Size.X / 2, Size.Y / 2);
		}
		protected virtual void CalculateDestinationRectangle() {
			destinationRectangle = new Rectangle(Position.ToPoint(), Size.ToPoint());
		}

		#endregion

	}
}
