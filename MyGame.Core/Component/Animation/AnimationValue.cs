using System;

namespace MyGame.Core.Component.Animation
{
	class AnimationValue<T> {
		public event Action<T> Change;
		private T _value;
		public T Value {
			get => _value;
			set {
				_value = value;
				OnChange(value);
			}
		}
		protected virtual void OnChange(T obj) {
			Change?.Invoke(obj);
		}
	}
}
