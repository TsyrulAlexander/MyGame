using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDesigner.Command;
using MonoDesigner.UI;
using MonoGame.Extended.BitmapFonts;
using MyGame.Core;
using MyGame.Core.Scene;

namespace MonoDesigner.Scene
{
	class MainScene: BaseScene {
		private IGuiDesigner _guiDesigner;

		public override void LoadContent(SpriteBatch spriteBatch) {
			base.LoadContent(spriteBatch);
			var font = ContentManager.Load<BitmapFont>(@"Font\Default");
			_guiDesigner = new GuiDesigner(GraphicsDeviceManager.GraphicsDevice, font);
			GameServices.AddService(_guiDesigner);
			InitializeCommandManager();
		}
		protected virtual void InitializeCommandManager() {
			var controlGroup = new CommandGroup("TestGroup");
			controlGroup.Add(new CreateButtonCommand());
			CommandManager.AddGroup(controlGroup);
		}
		public override void Update(GameTime gameTime) {
			_guiDesigner.Update(gameTime);
			base.Update(gameTime);
		}
		public override void Draw(GameTime gameTime) {
			_guiDesigner.Draw(gameTime);
			base.Draw(gameTime);
		}
		protected override void GameWindowOnClientSizeChanged(object sender, EventArgs e) {
			base.GameWindowOnClientSizeChanged(sender, e);
			_guiDesigner.Resize();
		}
	}
}
