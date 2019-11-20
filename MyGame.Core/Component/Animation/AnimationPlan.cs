using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyGame.Core.Component.Animation {
	public class AnimationPlan<T> {
		public Dictionary<T, Animation> Animations { get; set; } = new Dictionary<T, Animation>();
		public void AddAnimation(Animation animation, T value) {
			Animations.Add(value, animation);
		}
		public void Update(GameTime gameTime) {
			foreach (var animation in Animations) {
				animation.Value.Update(gameTime);
			}
		}
		public Animation GetAnimation(T value) {
			return Animations[value];
		}
	}
}
