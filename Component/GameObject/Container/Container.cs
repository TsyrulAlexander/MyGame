using System.Collections.Generic;

namespace MyGame.Component.GameObject.Container {
	public class Container : BaseGameObject {
		public List<ContainerItem> Items { get; set; }
	}
}