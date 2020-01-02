using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGame.Extended.Gui.Controls;
using MyGame.Core.Component.GameObject;
using MyGame.Core.Scene;

namespace MonoDesigner.UI
{
	class GameObjectControl: ListBox
	{
		public override List<object> Items {
			get {
				Control c = new Canvas();
				var gameObjects = SceneManager.CurrentScene.GetGameObjects();
				return gameObjects as List<object> ?? gameObjects.Cast<object>().ToList();
			}
		}
	}
}
