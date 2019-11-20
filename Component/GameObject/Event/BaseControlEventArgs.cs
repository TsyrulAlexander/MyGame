using System;

namespace MyGame.Component.GameObject.Event {
	public class BaseControlEventArgs : EventArgs {
		public IGameObject GameObject { get; set; }
	}
}
