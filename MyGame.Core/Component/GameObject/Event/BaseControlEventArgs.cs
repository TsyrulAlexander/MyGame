using System;

namespace MyGame.Core.Component.GameObject.Event {
	public class BaseControlEventArgs : EventArgs {
		public IGameObject GameObject { get; set; }
	}
}
