using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame.Core
{
	public static class GameServices {
		private static GameServiceContainer _container;
		public static GameServiceContainer Instance => _container ?? (_container = new GameServiceContainer());
		internal static void SetGameServiceContainer(GameServiceContainer container) {
			_container = container;
		}
		public static T GetService<T>() {
			return (T)Instance.GetService(typeof(T));
		}

		public static void AddService<T>(T service) {
			Instance.AddService(typeof(T), service);
		}

		public static void RemoveService<T>() {
			Instance.RemoveService(typeof(T));
		}
	}
}
