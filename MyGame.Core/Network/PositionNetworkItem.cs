using System;
using Microsoft.Xna.Framework;
using MyGame.Network.Entities;

namespace MyGame.Core.Network
{
	[Serializable]
	public class PositionNetworkItem: NetworkItem<Vector2>
	{
		public PositionNetworkItem(string methodName, Guid id, Vector2 value) : base(methodName, id, value) { }
	}
}
