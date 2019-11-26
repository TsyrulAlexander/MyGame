using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Core.Component;
using MyGame.Core.Component.GameObject;

namespace MyGame.Core.Scene
{
	public class BaseScene: Game {
		public event Action<IGameObject> AddItem;
		public event Action<IGameObject> RemoveItem;
		private readonly IServiceCollection _service;
		protected GraphicsDeviceManager Graphics;
		protected SpriteBatch SpriteBatch;
		private List<IGameObject> Items { get; }
		public BaseScene(IServiceCollection service) {
			_service = service;
			Items = new List<IGameObject>();
			Graphics = new GraphicsDeviceManager(this);
			service.Replace(ServiceDescriptor.Singleton(Graphics));
			service.Replace(ServiceDescriptor.Singleton(Content));
			Content.RootDirectory = "Content";
			IsFixedTimeStep = true;
			TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d);
			IsMouseVisible = true;
		}

		protected override void LoadContent() {
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			_service.Replace(ServiceDescriptor.Singleton(SpriteBatch));
		}

		protected override void Update(GameTime gameTime) {
			if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
				Exit();
			}
			base.Update(gameTime);
			for (var index = 0; index < Items.Count; index++) {
				var gameObject = Items[index];
				gameObject.Update(gameTime);
			}
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);
			SpriteBatch.Begin();
			base.Draw(gameTime);
			Items.ForEach(gameObject => gameObject.Draw(SpriteBatch, gameTime));
			SpriteBatch.End();
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
