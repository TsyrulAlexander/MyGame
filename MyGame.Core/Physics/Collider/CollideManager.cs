using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyGame.Core.Physics.Collider
{
	internal enum CollidedType {
		Rectangle,
		Circle
	}
	
	public static class CollideManager {
		private class CollidedManagerInfo {
			public ICollider Collider { get; set; }
			public CollidedType Type { get; set; }
			public CollidedManagerInfo(ICollider collider, CollidedType type) {
				Collider = collider;
				Type = type;
			}
		}
		private static List<CollidedManagerInfo> ColliderList { get; set; } = new List<CollidedManagerInfo>();
		public static void AddCollider(RectangleCollider collider) {
			ColliderList.Add(new CollidedManagerInfo(collider, CollidedType.Rectangle));
		}
		public static void Update(GameTime gameTime) {
			foreach (var colliderInfo1 in ColliderList) {
				foreach (var colliderInfo2 in ColliderList) {
					if (colliderInfo1 != colliderInfo2 && GetIsCollide(colliderInfo1, colliderInfo2)) {
						colliderInfo1.Collider.OnCollide(colliderInfo2.Collider);
					}
				}
			}
		}
		private static bool GetIsCollide(CollidedManagerInfo info1, CollidedManagerInfo info2) {
			switch (info2.Type) {
				case CollidedType.Rectangle:
					return info1.Collider.GetIsCollide((RectangleCollider) info2.Collider);
				case CollidedType.Circle:
					return info1.Collider.GetIsCollide((CircleCollider)info2.Collider);
				default:
					return false;
			}
		}
	}
}
