using System;
using Microsoft.Xna.Framework;

namespace MyGame.Network.Entities
{
	public class PositionNetworkItem: NetworkItem<Vector2>
	{
		public PositionNetworkItem(string methodName, Guid id, Vector2 value) : base(methodName, id, value) { }
	}
}
