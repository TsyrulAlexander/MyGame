using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using MyGame.Core.Component.GameObject;
using MyGame.Core.Utility;

namespace MyGame.Core.Physics.Collider
{
	public class RectangleCollider: ICollider {
		public event Action<ColliderEventArgs> Collide;
		public IGameObject GameObject { get; }
		public Rectangle Rectangle { get; set; }
		protected MemberInfo RectangleInfo { get; set; }
		protected MemberInfo PositionInfo { get; set; }
		protected MemberInfo SizeInfo { get; set; }
		public RectangleCollider(IGameObject gameObject) {
			GameObject = gameObject;
			GameObject.PropertyChanged += GameObjectOnPropertyChanged;
		}
		private void GameObjectOnPropertyChanged(object sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == RectangleInfo?.Name) {
				Rectangle = ReflectionUtilities.GetPropertyValue<Rectangle>(GameObject, RectangleInfo);
			} else if (e.PropertyName == PositionInfo?.Name || e.PropertyName == SizeInfo?.Name) {
				Rectangle = new Rectangle(GameObject.Position.ToPoint(), GameObject.Size.ToPoint());
			}
		}
		public void SetRectangleWithObject() {
			PositionInfo = ReflectionUtilities.GetPropertyInfo<IGameObject, Vector2>(gameObject => gameObject.Position);
			SizeInfo = ReflectionUtilities.GetPropertyInfo<IGameObject, Vector2>(gameObject => gameObject.Size);
			Rectangle = new Rectangle(GameObject.Position.ToPoint(), GameObject.Size.ToPoint());
		}
		public void SetRectangle(Rectangle rectangle) {
			Rectangle = rectangle;
		}
		public void SetRectangle(Expression<Func<IGameObject, Rectangle>> propFunc) {
			RectangleInfo = ReflectionUtilities.GetPropertyInfo(propFunc);
			Rectangle = ReflectionUtilities.GetPropertyValue<Rectangle>(GameObject, RectangleInfo);
		}
		public bool GetIsCollide(RectangleCollider value) {
			int topEdge1 = Rectangle.Y + Rectangle.Height;
			int rightEdge1 = Rectangle.X + Rectangle.Width;
			int leftEdge1 = Rectangle.X;
			int bottomEdge1 = Rectangle.Y;
			int topEdge2 = value.Rectangle.Y + value.Rectangle.Height;
			int rightEdge2 = value.Rectangle.X + value.Rectangle.Width;
			int leftEdge2 = value.Rectangle.X;
			int bottomEdge2 = value.Rectangle.Y;
			if (leftEdge1 < rightEdge2 && rightEdge1 > leftEdge2 && bottomEdge1 < topEdge2 && topEdge1 > bottomEdge2) {
				return true;
			}
			return false;
		}
		public bool GetIsCollide(CircleCollider value) {
			throw new NotImplementedException();
		}
		public void OnCollide(ICollider collider) {
			Collide?.Invoke(new ColliderEventArgs {
				Collider = collider
			});
		}
		protected bool IsTouchingLeft(Rectangle rectangle) {
			return this.Rectangle.Right > rectangle.Left &&
				this.Rectangle.Left < rectangle.Left &&
				this.Rectangle.Bottom > rectangle.Top &&
				this.Rectangle.Top < rectangle.Bottom;
		}
		protected bool IsTouchingRight(Rectangle rectangle) {
			return this.Rectangle.Left < rectangle.Right &&
				this.Rectangle.Right > rectangle.Right &&
				this.Rectangle.Bottom > rectangle.Top &&
				this.Rectangle.Top < rectangle.Bottom;
		}
		protected bool IsTouchingTop(Rectangle rectangle) {
			return this.Rectangle.Bottom > rectangle.Top &&
				this.Rectangle.Top < rectangle.Top &&
				this.Rectangle.Right > rectangle.Left &&
				this.Rectangle.Left < rectangle.Right;
		}
		protected bool IsTouchingBottom(Rectangle rectangle) {
			return this.Rectangle.Top < rectangle.Bottom &&
				this.Rectangle.Bottom > rectangle.Bottom &&
				this.Rectangle.Right > rectangle.Left &&
				this.Rectangle.Left < rectangle.Right;
		}
	}
}
