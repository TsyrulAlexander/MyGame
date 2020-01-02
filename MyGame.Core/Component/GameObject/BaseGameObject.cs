using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using MyGame.Core.Annotations;

namespace MyGame.Core.Component.GameObject {
	[Serializable]
	public abstract class BaseGameObject : IGameObject, ISerializable {

		#region Fields: Private

		private Vector2 _position;
		private Vector2 _size;
		protected Rectangle SourceRectangle;
		protected Rectangle PositionRectangle;
		protected Vector2 Origin;
		private Guid _id = Guid.NewGuid();
		private float _rotation;
		private Color _color = Color.White;
		private int _zIndex;
		private float _scale = 1;
		private bool _isVisible = true;
		private bool _enabled = true;
		private int _drawOrder = 1;
		private int _updateOrder = 1;

		#endregion

		#region Properties: Public
		public event PropertyChangedEventHandler PropertyChanged;
		public Guid Id {
			get => _id;
			set {
				_id = value;
				OnPropertyChanged();
			}
		}
		public Vector2 Position {
			get => _position;
			set {
				_position = value;
				CalculatePositionRectangle();
				OnPropertyChanged();
			}
		}
		public Vector2 Size {
			get => _size;
			set {
				_size = value;
				CalculateOriginVector();
				CalculateDestinationRectangle();
				CalculatePositionRectangle();
				OnPropertyChanged();
			}
		}
		public float Rotation {
			get => _rotation;
			set {
				_rotation = value;
				OnPropertyChanged();
			}
		}
		public Color Color {
			get => _color;
			set {
				_color = value;
				OnPropertyChanged();
			}
		}
		public int ZIndex {
			get => _zIndex;
			set {
				_zIndex = value;
				OnPropertyChanged();
			}
		}
		public float Scale {
			get => _scale;
			set {
				_scale = value;
				CalculateSize();
				OnPropertyChanged();
			}
		}
		public bool Visible {
			get => _isVisible;
			set {
				_isVisible = value;
				VisibleChanged?.Invoke(this, EventArgs.Empty);
				OnPropertyChanged();
			}
		}
		public bool Enabled {
			get => _enabled;
			set {
				_enabled = value;
				EnabledChanged?.Invoke(this, EventArgs.Empty);
				OnPropertyChanged();
			}
		}
		public int DrawOrder {
			get => _drawOrder;
			set {
				_drawOrder = value;
				DrawOrderChanged?.Invoke(this, EventArgs.Empty);
				OnPropertyChanged();
			}
		}
		public int UpdateOrder {
			get => _updateOrder;
			set {
				_updateOrder = value;
				UpdateOrderChanged?.Invoke(this, EventArgs.Empty);
				OnPropertyChanged();
			}
		}
		public event EventHandler<EventArgs> DrawOrderChanged;
		public event EventHandler<EventArgs> VisibleChanged;
		
		public event EventHandler<EventArgs> EnabledChanged;
		public event EventHandler<EventArgs> UpdateOrderChanged;
		#endregion

		#region Method: Public

		public virtual void Draw(GameTime gameTime) { }
		public virtual void Update(GameTime gameTime) { }
		
		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue(nameof(_position), _position, _position.GetType());
			info.AddValue(nameof(_size), _size, _size.GetType());
			info.AddValue(nameof(_id), _id, typeof(Guid));
			info.AddValue(nameof(_rotation), _rotation, _rotation.GetType());
			//info.AddValue(nameof(_color), _color, _color.GetType());
			info.AddValue(nameof(_zIndex), _zIndex, _zIndex.GetType());
			info.AddValue(nameof(_scale), _scale, _scale.GetType());
			info.AddValue(nameof(_isVisible), _isVisible, _isVisible.GetType());
		}

		#endregion

		public BaseGameObject() {
		}

		public BaseGameObject(SerializationInfo info, StreamingContext context) {
			_id = (Guid)info.GetValue(nameof(_id), _id.GetType());
			_position = (Vector2)info.GetValue(nameof(_position), _position.GetType());
		}

		#region Method: Protected
		protected virtual void CalculateOriginVector() {
			Origin = new Vector2(Size.X / 2, Size.Y / 2);
		}
		protected virtual void CalculateDestinationRectangle() {
			SourceRectangle = new Rectangle(Point.Zero, new Point((int)(Size.X / Scale), (int)(Size.Y / Scale)));
		}
		protected virtual void CalculateSize() {
			Size = new Vector2(Size.X * Scale, Size.Y * Scale);
		}
		protected virtual void CalculatePositionRectangle() {
			PositionRectangle = new Rectangle(Position.ToPoint(), Size.ToPoint());
		}
		protected virtual Vector2 GetRotationVector(float? rotation = null) {
			return new Vector2(GetRotationCos(rotation), GetRotationSin(rotation));
		}
		protected virtual float GetRotationCos(float? rotation = null) {
			return (float) Math.Cos(rotation ?? Rotation);
		}
		protected virtual float GetRotationSin(float? rotation = null) {
			return (float)Math.Sin(rotation ?? Rotation);
		}
		protected virtual float GetTotalMilliseconds(GameTime gameTime) {
			return (float)gameTime.ElapsedGameTime.TotalMilliseconds;
		}
		#endregion

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}