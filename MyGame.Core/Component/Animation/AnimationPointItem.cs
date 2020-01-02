using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using MyGame.Core.Utility;

namespace MyGame.Core.Component.Animation
{
	public class AnimationPointItem<T, TProperty> : IAnimationItem<TProperty> {
		private bool _isPlay;
		private int _currentAnimationPoint;
		public T Item { get; }
		public bool IsLoop { get; set; }
		protected MemberInfo MemberInfo { get; set; }
		private List<ITypeAnimator<TProperty>> AnimationPoints { get; set; } = new List<ITypeAnimator<TProperty>>();
		private ITypeAnimator<TProperty> CurrentAnimator => AnimationPoints[CurrentAnimationPoint];
		private int CurrentAnimationPoint {
			get => _currentAnimationPoint;
			set {
				if (value >= AnimationPoints.Count) {
					if (IsLoop) {
						_currentAnimationPoint = 0;
					} else {
						Stop();
						return;
					}
				} else {
					_currentAnimationPoint = value;
				}
				CurrentAnimator.Initialize(GetPropertyValue(MemberInfo));
			}
		}
		public AnimationPointItem(T item, Expression<Func<T, TProperty>> propFunc) {
			Item = item;
			MemberInfo = GetPropertyInfo(propFunc);
		}
		private void TypeAnimatorOnCompleted() {
			CurrentAnimationPoint++;
		}
		private void TypeAnimatorOnOnValueChange(TProperty obj) {
			SetPropertyValue(MemberInfo, obj);
		}
		public void AddAnimation(ITypeAnimator<TProperty> typeAnimator) {
			typeAnimator.Completed += TypeAnimatorOnCompleted;
			typeAnimator.ValueChange += TypeAnimatorOnOnValueChange;
			AnimationPoints.Add(typeAnimator);
		}
		public void Update(GameTime gameTime) {
			if (!_isPlay) {
				return;
			}
			CurrentAnimator.Update(gameTime);
		}
		public void Play() {
			_isPlay = true;
			CurrentAnimator.Initialize(GetPropertyValue(MemberInfo));
		}
		public void Stop() {
			_isPlay = false;
		}
		protected void SetPropertyValue(MemberInfo memberInfo, TProperty value) {
			ReflectionUtilities.SetPropertyValue(memberInfo, Item, value);
		}
		protected TProperty GetPropertyValue(MemberInfo memberInfo) {
			return ReflectionUtilities.GetPropertyValue<TProperty>(Item, memberInfo);
		}
		public MemberInfo GetPropertyInfo<TSource>(
			Expression<Func<TSource, TProperty>> propertyLambda) {
			return ReflectionUtilities.GetPropertyInfo(propertyLambda);
		}
	}
}
