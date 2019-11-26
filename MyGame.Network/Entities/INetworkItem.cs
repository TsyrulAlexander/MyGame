using System;

namespace MyGame.Network.Entities {
	public interface INetworkItem {
		Guid Id { get; set; }
		string MethodName { get; set; }
	}
}
