using System;

namespace MyGame.Core.Physics.Collider {
	public class ColliderEventArgs : EventArgs {
		public ICollider Collider { get; set; }
	}
}