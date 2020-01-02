using System;
using System.Collections.Generic;
using System.Text;

namespace MyGame.Core.Physics.Collider
{
	public interface ICollide<in T>
	{
		bool GetIsCollide(T value);
	}
}
