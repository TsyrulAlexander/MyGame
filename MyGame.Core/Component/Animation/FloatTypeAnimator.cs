using System;
using Microsoft.Xna.Framework;

namespace MyGame.Core.Component.Animation {
	public class FloatTypeAnimator : BaseTypeAnimator<float> {
		
		private float Value { get; set; }
		private int Milliseconds { get; set; }
		private double Step { get; set; }
		public FloatTypeAnimator(float value, float newValue, int milliseconds) {
			Value = value;
			Milliseconds = milliseconds;
			Step = (newValue - value) / milliseconds;
		}
		public override void Update(GameTime gameTime) {
			if (Milliseconds <= 0) {
				OnCompleted();
				return;
			}
			Milliseconds -= gameTime.ElapsedGameTime.Milliseconds;
			OnValueChange(Value + (float)(Step * gameTime.ElapsedGameTime.TotalMilliseconds));
		}
		protected override void OnValueChange(float newValue) {
			if (Math.Abs(newValue - Value) > 0.0000) {
				Value = newValue;
				base.OnValueChange(newValue);
			}
		}
	}
}