using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame.Core.Component.Animation {
	//public class DoubleTypeAnimator : ITypeAnimator<double> {
	//	public event Action<double> OnValueChange;
	//	protected double Value;
	//	protected double NewValue;
	//	protected int Milliseconds;
	//	protected double Step;
	//	public void Initialize(double value, double newValue, int milliseconds) {
	//		Value = value;
	//		NewValue = newValue;
	//		Milliseconds = milliseconds;
	//		Step = (newValue - value) / milliseconds;
	//	}
	//	public void Update(GameTime gameTime) {
	//		if (Milliseconds <= 0) {
	//			return;
	//		}
	//		Milliseconds -= gameTime.ElapsedGameTime.Milliseconds;
	//		Value += Step * gameTime.ElapsedGameTime.Milliseconds;
	//	}
	//}
}
