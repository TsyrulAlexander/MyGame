using MyGame.Core.Component.GameObject;

namespace MyGame.Core.Physics.Collider
{
	public class CircleCollider: ICollider
	{
		public IGameObject GameObject {
			get;
		}
		public bool GetIsCollide(RectangleCollider value) {
			throw new System.NotImplementedException();
		}
		public bool GetIsCollide(CircleCollider value) {
			throw new System.NotImplementedException();
		}
		public void OnCollide(ICollider collider) {
			throw new System.NotImplementedException();
		}
	}
}
