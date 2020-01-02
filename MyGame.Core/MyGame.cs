using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Core;
using MyGame.Core.Physics.Collider;
using MyGame.Core.Scene;

namespace MyGame
{
	public class MyGame : Game {
		protected readonly GraphicsDeviceManager Graphics;
		protected SpriteBatch SpriteBatch;
		public BaseScene Scene { get; set; }
		public MyGame() {
			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsFixedTimeStep = true;
			TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d);
			IsMouseVisible = true;
		}
		protected void InitServiceCollection() {
			GameServices.SetGameServiceContainer(Services);
			GameServices.AddService(SpriteBatch);
			GameServices.AddService(Window);
			GameServices.AddService(Graphics);

		}
		protected override void LoadContent() {
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			InitServiceCollection();
			if (SceneManager.CurrentScene != null) {
				SerCurrentScene(SceneManager.CurrentScene);
			}
			SceneManager.SceneChange += SceneManagerOnSceneChange;
		}
		private void SceneManagerOnSceneChange(BaseScene scene) {
			SerCurrentScene(scene);
		}
		protected override void UnloadContent() {
			base.UnloadContent();
			Scene?.UnloadContent();
		}
		protected void SerCurrentScene(BaseScene scene) {
			Scene?.UnloadContent();
			scene.LoadContent(SpriteBatch);
			Scene = scene;
		}
		protected override void Update(GameTime gameTime) {
			if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
				Exit();
			}
			CollideManager.Update(gameTime);
			base.Update(gameTime);
			Scene?.Update(gameTime);
		}
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);
			SpriteBatch.Begin();
			base.Draw(gameTime);
			SpriteBatch.End();
			Scene?.Draw(gameTime);
		}
	}
}
