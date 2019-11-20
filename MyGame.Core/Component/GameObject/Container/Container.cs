using System.Collections.Generic;

namespace MyGame.Core.Component.GameObject.Container {
	public class Container : BaseGameObject {
		public List<ContainerItem> Items { get; set; }
	}
}