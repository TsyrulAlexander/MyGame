using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MyGame.Core.Utility
{
	public static class ReflectionUtilities
	{
		public  static void SetPropertyValue(MemberInfo memberInfo, object obj, object propertyValue) {
			switch (memberInfo) {
				case PropertyInfo propertyInfo:
					propertyInfo.SetValue(obj, propertyValue);
					break;
				case FieldInfo fieldInfo:
					fieldInfo.SetValue(obj, propertyValue);
					break;
			}
		}
		public static T GetPropertyValue<T>(object obj, MemberInfo memberInfo) {
			if (memberInfo is PropertyInfo propertyInfo) {
				return (T)propertyInfo.GetValue(obj);
			}
			if (memberInfo is FieldInfo fieldInfo) {
				return (T)fieldInfo.GetValue(obj);
			}
			return default;
		}
		public static MemberInfo GetPropertyInfo<TSource, TProperty>(
			Expression<Func<TSource, TProperty>> propertyLambda) {
			var type = typeof(TSource);
			if (!(propertyLambda.Body is MemberExpression member))
				throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");
			var propInfo = member.Member;
			if (propInfo == null)
				throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");
			if (type != propInfo.ReflectedType &&
				!type.IsSubclassOf(propInfo.ReflectedType))
				throw new ArgumentException(
					$"Expression '{propertyLambda}' refers to a property that is not from type {type}.");
			return propInfo;
		}
	}
}
