using Microsoft.Xna.Framework;
using MyGame.Component.GameObject.Event;

namespace MyGame.Component.GameObject.Container {
	public class ContainerItem {
		internal Container Container { get; }
		internal IGameObject GameObject { get; }
		internal Vector2 ItemPosition { get; private set; }

		public ContainerItem(Container container, IGameObject gameObject) {
			Container = container;
			GameObject = gameObject;
			container.PositionChange += ItemPositionChange;
			gameObject.PositionChange += ItemPositionChange;
		}
		private void ItemPositionChange(PositionChangeEventArgs eventArgs) {
			ItemPosition = Vector2.Add(Container.Position, GameObject.Position);
		}
	}
}