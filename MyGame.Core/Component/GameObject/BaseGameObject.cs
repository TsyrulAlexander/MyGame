using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Core.Annotations;
using MyGame.Core.Component.GameObject.Event;
using MyGame.Core.Network.Helper;
using MyGame.Core.Serialize;
using MyGame.Network;

namespace MyGame.Core.Component.GameObject {
	[Serializable]
	public class BaseGameObject : IGameObject, ISerializable {

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
		private bool _isSynchronize = true;

		#endregion

		#region Properties: Public
		public event PropertyChangedEventHandler PropertyChanged;
		public event Action<PositionChangeEventArgs> PositionChange;
		public event Action<SizeChangeEventArgs> SizeChange;
		public bool IsSynchronize {
			get => _isSynchronize;
			set {
				if (value == _isSynchronize) {
					return;
				}
				_isSynchronize = value;
				OnPropertyChanged();
			}
		}
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
				var oldValue = _position;
				_position = value;
				OnPositionChange(oldValue, value);
				OnPropertyChanged();
			}
		}
		public Vector2 Size {
			get => _size;
			set {
				var oldValue = _size;
				_size = value;
				OnSizeChange(oldValue, value);
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
				OnPropertyChanged();
			}
		}
		public bool IsVisible {
			get => _isVisible;
			set {
				_isVisible = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Method: Public

		public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) { }
		public virtual void Update(GameTime gameTime) { }

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			//info.SetType(typeof(TypeReference));
			//var type = this.GetType();
			//info.AddValue("AssemblyName", type.Assembly.FullName);
			//info.AddValue("FullName", type.FullName);
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

		protected virtual void OnPositionChange(Vector2 oldValue, Vector2 newValue) {
			CalculatePositionRectangle();
			PositionChange?.Invoke(new PositionChangeEventArgs {
				GameObject = this,
				OldValue = oldValue,
				NewValue = newValue
			});
		}
		protected virtual void OnSizeChange(Vector2 oldValue, Vector2 newValue) {
			CalculateOriginVector();
			CalculateDestinationRectangle();
			CalculatePositionRectangle();
			SizeChange?.Invoke(new SizeChangeEventArgs {
				GameObject = this,
				OldValue = oldValue,
				NewValue = newValue
			});
		}
		protected virtual void CalculateOriginVector() {
			Origin = new Vector2(Size.X / 2, Size.Y / 2);
		}
		protected virtual void CalculateDestinationRectangle() {
			SourceRectangle = new Rectangle(Point.Zero, Size.ToPoint());
		}
		protected virtual void CalculatePositionRectangle() {
			PositionRectangle = new Rectangle(Position.ToPoint(), Size.ToPoint());
		}
		#endregion

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}