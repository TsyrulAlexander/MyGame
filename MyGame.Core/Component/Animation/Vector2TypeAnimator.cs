using Microsoft.Xna.Framework;

namespace MyGame.Core.Component.Animation
{
	public class Vector2TypeAnimator: BaseTypeAnimator<Vector2> {
		public Vector2 NewValue { get; }
		public int Milliseconds { get; }
		protected double UseMilliseconds { get; set; }
		private double _xStep;
		private double _yStep;
		public Vector2TypeAnimator(Vector2 newValue, int milliseconds) {
			NewValue = newValue;
			Milliseconds = milliseconds;
		}
		public override void Initialize(Vector2 currentValue) {
			base.Initialize(currentValue);
			UseMilliseconds = Milliseconds;
			var differenceVector = NewValue - Value;
			_xStep = differenceVector.X / Milliseconds;
			_yStep = differenceVector.Y / Milliseconds;
		}
		public override void Update(GameTime gameTime) {
			if (UseMilliseconds <= 0) {
				OnCompleted();
				return;
			}
			UseMilliseconds -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
			OnValueChange(new Vector2((float)(Value.X + _xStep * gameTime.ElapsedGameTime.TotalMilliseconds),
				(float)(Value.Y + _yStep * gameTime.ElapsedGameTime.TotalMilliseconds)));
		}
		protected override void OnValueChange(Vector2 newValue) {
			if (Value.Equals(newValue)) {
				return;
			}
			Value = newValue;
			base.OnValueChange(newValue);
		}
	}
}
