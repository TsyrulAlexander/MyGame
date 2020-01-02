using System;
using System.Collections.Generic;
using System.Text;
using MyGame.Core.Component.GameObject;

namespace MyGame.Core.Physics.Collider
{
	public interface ICollider: IRectangleCollide, ICircleCollide {
		IGameObject GameObject { get; }
		void OnCollide(ICollider collider);
	}
}
