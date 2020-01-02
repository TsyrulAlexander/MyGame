using System;
using Microsoft.Xna.Framework;

namespace MyGame.Core.Component.Animation {
	public abstract class BaseTypeAnimator<T> : ITypeAnimator<T> {
		public event Action<T> ValueChange;
		public event Action Completed;
		public T Value { get; set; }
		public virtual void Initialize(T currentValue) {
			Value = currentValue;
		}
		public abstract void Update(GameTime gameTime);
		protected virtual void OnCompleted() {
			Completed?.Invoke();
		}
		protected virtual void OnValueChange(T newValue) {
			ValueChange?.Invoke(newValue);
		}
	}
}