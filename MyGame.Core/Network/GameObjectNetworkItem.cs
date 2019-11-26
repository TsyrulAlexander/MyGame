using System;
using System.Collections.Generic;
using System.Text;
using MyGame.Core.Component.GameObject;
using MyGame.Network.Entities;

namespace MyGame.Core.Network
{
	[Serializable]
	public class GameObjectNetworkItem: NetworkItem<IGameObject>
	{
		public GameObjectNetworkItem(string methodName, Guid id, IGameObject value) : base(methodName, id, value) { }
	}
}
