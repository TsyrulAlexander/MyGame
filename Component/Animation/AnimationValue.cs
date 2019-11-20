using System;
using System.Collections.Generic;
using System.Text;

namespace MyGame.Component.Animation
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
