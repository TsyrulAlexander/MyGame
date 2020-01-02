using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Core.Component.GameObject;

namespace MyGame.Core.Scene
{
	public abstract class BaseScene {
		protected GraphicsDeviceManager GraphicsDeviceManager { get; set; }
		public SpriteBatch SpriteBatch { get; private set; }
		public ContentManager ContentManager { get; set; }
		public GameWindow GameWindow { get; set; }
		public event Action<IGameObject> AddItem;
		public event Action<IGameObject> RemoveItem;
		protected virtual List<IGameObject> Items { get; } = new List<IGameObject>();

		public virtual void LoadContent(SpriteBatch spriteBatch) {
			GraphicsDeviceManager = GameServices.GetService<GraphicsDeviceManager>();
			GameWindow = GameServices.GetService<GameWindow>();
			ContentManager = new ContentManager(GameServices.Instance);
			ContentManager.RootDirectory = "Content";
			GameWindow.AllowUserResizing = true;
			GameWindow.ClientSizeChanged += GameWindowOnClientSizeChanged;
			SpriteBatch = spriteBatch;
		}
		public virtual void UnloadContent() {
			GameWindow.ClientSizeChanged -= GameWindowOnClientSizeChanged;
			ContentManager.Unload();
		}
		protected virtual void GameWindowOnClientSizeChanged(object sender, EventArgs e) { }
		public virtual void Update(GameTime gameTime) {
			for (var index = 0; index < Items.Count; index++) {
				var gameObject = Items[index];
				gameObject.Update(gameTime);
			}
		}
		protected virtual void BeginDraw(GameTime gameTime) {
			SpriteBatch.Begin();
		}
		protected virtual void EndDraw(GameTime gameTime) {
			SpriteBatch.End();
		}
		public virtual void Draw(GameTime gameTime) {
			BeginDraw(gameTime);
			Items.ForEach(gameObject => gameObject.Draw(gameTime));
			EndDraw(gameTime);
		}
		public virtual IEnumerable<IGameObject> GetGameObjects() {
			return Items;
		}
		public virtual void AddGameObject(IGameObject gameObject) {
			Items.Add(gameObject);
			OnAddItem(gameObject);
		}
		public virtual void RemoveGameObject(IGameObject gameObject) {
			Items.Remove(gameObject);
			OnRemoveItem(gameObject);
		}
		public virtual IGameObject GetGameObject(Guid id) {
			return Items.FirstOrDefault(gameObject => gameObject.Id == id);
		}
		protected virtual void OnAddItem(IGameObject obj) {
			AddItem?.Invoke(obj);
		}
		protected virtual void OnRemoveItem(IGameObject obj) {
			RemoveItem?.Invoke(obj);
		}
	}
}
