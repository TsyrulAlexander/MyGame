using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Microsoft.Xna.Framework;
using MyGame.Core.Component.GameObject;
using MyGame.Core.Scene;
using MyGame.Core.Serialize;
using MyGame.Core.Serialize.Surrogate;
using MyGame.Network.Client;
using MyGame.Network.Entities;

namespace MyGame.Core.Network
{
	public class GameSynchronizer {
		public BaseScene Scene { get; }
		string ipAddress = "127.0.0.1";
		int port = 8888;
		ClientTcp clientTcp = new ClientTcp();
		public GameSynchronizer(BaseScene scene) {
			Scene = scene;
			Scene.AddItem += SceneOnAddItem;
			Scene.RemoveItem += SceneOnRemoveItem;
		}
		public void Start() {
			clientTcp.Connect(ipAddress, port);
			clientTcp.Message += ClientTcpOnMessage;
			clientTcp.Selector += Selector;
		}
		private void ClientTcpOnMessage(INetworkItem obj) {//todo
			if (obj.MethodName == "CreateGameObject") {
				var gameObjectItem = (GameObjectNetworkItem)obj;
				Scene.AddGameObject(gameObjectItem.Value);
				
			} else if (obj.MethodName == "SetPosition") {
				var positionItem = (PositionNetworkItem)obj;
				var gameObject = Scene.GetGameObject(obj.Id);
				if (gameObject != null) {
					gameObject.Position = positionItem.Value;
				}
			}
		}
		public void Stop() {
			clientTcp.Disconnect();
		}
		private void SceneOnRemoveItem(IGameObject obj) {
			throw new NotImplementedException();
		}
		private void SceneOnAddItem(IGameObject gameObject) {
			gameObject.PropertyChanged += GameObjectOnPropertyChanged;
			if (!gameObject.IsSynchronize) {
				return;
			}
			clientTcp.Send(new GameObjectNetworkItem("CreateGameObject", gameObject.Id, gameObject));
		}
		public static SurrogateSelector Selector(BinaryFormatter formatter) {
			var ss = new SurrogateSelector();
			ss.AddSurrogate(typeof(Vector2), formatter.Context,
				new Vector2Surrogate());
			return ss;
		}
		private void GameObjectOnPropertyChanged(object sender, PropertyChangedEventArgs e) {
			var gameObject = (IGameObject) sender;
			if (!gameObject.IsSynchronize) {
				return;
			}
			if (e.PropertyName == nameof(gameObject.Position)) {
				clientTcp.Send(new PositionNetworkItem("SetPosition", gameObject.Id, gameObject.Position));
			}
		}
	}
}
