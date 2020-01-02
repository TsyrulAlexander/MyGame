using System;
using System.Collections.Generic;

namespace MyGame.Core.Scene {
	public static class SceneManager {
		public static event Action<BaseScene> SceneChange;
		private static List<BaseScene> Scenes { get; set; } = new List<BaseScene>();
		private static int _lastExecuted = -1;
		public static void Clear() {
			Scenes.Clear();
			_lastExecuted = -1;
		}

		public static int Size => Scenes.Count;
		public static int LastExecuted => _lastExecuted;
		public static BaseScene CurrentScene {
			get {
				if (_lastExecuted == -1) {
					return null;
				}
				return Scenes[_lastExecuted];
			}
		}
		 
		public static void Add(BaseScene scene) {
			if (_lastExecuted + 1 < Scenes.Count) {
				int numCommandsToRemove = Scenes.Count - (_lastExecuted + 1);
				for (var i = 0; i < numCommandsToRemove; i++) {
					Scenes.RemoveAt(_lastExecuted + 1);
				}
			}
			Scenes.Add(scene);
			_lastExecuted = Scenes.Count - 1;
			OnSceneChange();
		}


		public static void Undo() {
			if (_lastExecuted >= 0) {
				if (Scenes.Count > 0) {
					_lastExecuted--;
					OnSceneChange();
				}
			}
		}


		public static void Redo() {
			if (_lastExecuted + 1 < Scenes.Count) {
				_lastExecuted++;
				OnSceneChange();
			}
		}
		private static void OnSceneChange() {
			SceneChange?.Invoke(Scenes[_lastExecuted]);
		}
	}
}