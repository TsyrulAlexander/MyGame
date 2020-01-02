using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame.Core.Component.Animation
{
	public interface ITypeAnimator<T> {
		event Action<T> ValueChange;
		event Action Completed;
		T Value { get; set; }
		void Initialize(T currentValue);
		void Update(GameTime gameTime);
	}
}
