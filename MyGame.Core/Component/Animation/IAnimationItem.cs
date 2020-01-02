using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame.Core.Component.Animation
{
	public interface IAnimationItem<TProperty> {
		bool IsLoop { get; set; }
		void AddAnimation(ITypeAnimator<TProperty> typeAnimator);
		void Update(GameTime gameTime);
		void Play();
		void Stop();
	}
}
