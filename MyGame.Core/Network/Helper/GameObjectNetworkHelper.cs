using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace MyGame.Core.Network.Helper
{
	[Serializable]
	[SecurityPermissionAttribute(SecurityAction.LinkDemand,
		Flags = SecurityPermissionFlag.SerializationFormatter)]
	public class GameObjectNetworkHelper: IObjectReference
	{
		public object GetRealObject(StreamingContext context) {
			throw new NotImplementedException();
		}
	}
}
