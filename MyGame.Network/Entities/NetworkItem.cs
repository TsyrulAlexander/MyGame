using System;

namespace MyGame.Network.Entities {
	[Serializable]
	public abstract class NetworkItem<T>: INetworkItem {
		public T Value { get; set; }
		public Guid Id { get; set; }
		public string MethodName { get; set; }
		public NetworkItem(string methodName, Guid id, T value) {
			MethodName = methodName;
			Id = id;
			Value = value;
		}
	}
}