using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using MyGame.Core.Utility;

namespace MyGame.Core.Component.Animation
{
	public class AnimationTriggerItem<TObject, TValue, TTriggerValue> where TObject : INotifyPropertyChanged {
		protected TObject Item { get; }
		protected MemberInfo ValueMember;
		protected MemberInfo TriggerInfo;
		protected bool _isPlay;
		protected bool _isPause;
		protected readonly Dictionary<TTriggerValue, ITypeAnimator<TValue>> Animations = new Dictionary<TTriggerValue, ITypeAnimator<TValue>>();
		protected ITypeAnimator<TValue> CurrentTypeAnimator { get; set; }
		public bool IsLoop { get; set; }
		public AnimationTriggerItem(TObject item, Expression<Func<TObject, TValue>> propFunc, Expression<Func<TObject, TTriggerValue>> triggerFunc) {
			Item = item;
			ValueMember = GetPropertyInfo(propFunc);
			TriggerInfo = GetPropertyInfo(triggerFunc);
			item.PropertyChanged += ItemOnPropertyChanged;
		}
		private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs e) {
			if (e.PropertyName != TriggerInfo.Name) {
				return;
			}
			InitializeTypeTrigger();
		}
		protected void InitializeTypeTrigger() {
			var triggerValue = GetPropertyValue<TTriggerValue>(TriggerInfo);
			if (!Animations.ContainsKey(triggerValue)) {
				return;
			}
			var newTypeAnimator = Animations[triggerValue];
			if (newTypeAnimator == CurrentTypeAnimator) {
				return;
			}
			CurrentTypeAnimator = newTypeAnimator;
			var propertyValue = GetPropertyValue<TValue>(ValueMember);
			CurrentTypeAnimator.Initialize(propertyValue);
		}
		public void AddAnimation(TTriggerValue value, ITypeAnimator<TValue> typeAnimator) {
			Animations.Add(value, typeAnimator);
			typeAnimator.ValueChange += TypeAnimatorOnValueChange;
			typeAnimator.Completed += TypeAnimatorOnCompleted;
		}
		private void TypeAnimatorOnCompleted() {
			if (IsLoop) {
				InitializeTypeTrigger();
			}
			_isPause = true;
		}
		private void TypeAnimatorOnValueChange(TValue obj) {
			SetPropertyValue(ValueMember, obj);
		}
		public void Update(GameTime gameTime) {
			if (!_isPlay) {
				return;
			}
			CurrentTypeAnimator?.Update(gameTime);
		}
		public void Play() {
			_isPlay = true;
		}
		public void Stop() {
			_isPlay = false;
		}
		protected void SetPropertyValue(MemberInfo memberInfo, TValue value) {
			ReflectionUtilities.SetPropertyValue(memberInfo, Item, value);
		}
		protected T GetPropertyValue<T>(MemberInfo memberInfo) {
			return ReflectionUtilities.GetPropertyValue<T>(Item, memberInfo);
		}
		public MemberInfo GetPropertyInfo<T, TR>(
			Expression<Func<T, TR>> propertyLambda) {
			return ReflectionUtilities.GetPropertyInfo(propertyLambda);
		}
	}
}
