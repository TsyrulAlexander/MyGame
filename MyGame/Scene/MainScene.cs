using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using MyGame.Core.Component.GameObject;
using MyGame.Core.Component.GameObject.Person;
using MyGame.Core.Network;
using MyGame.Core.Scene;

namespace MyGame.Scene {
	public class MainScene : BaseScene {
		private GameSynchronizer Synchronizer;
		protected override void LoadContent() {
			base.LoadContent();
			Synchronizer = new GameSynchronizer(this);
			Synchronizer.Start();
			var btn = new Button(Content, @"grey_brick\grey_brick_state_1_left_side") {
				IsSynchronize = false,
				Scale = 0.2f
			};
			btn.Click += args => {
				var random = new Random();
				AddGameObject(new Person(Content, @"grey_brick\grey_brick_state_1_left_side") {
					Scale = 0.2f,
					Position = new Vector2(random.Next(50, Window.ClientBounds.Width), random.Next(50, Window.ClientBounds.Height))
				});
			};
			AddGameObject(btn);
		}
		public MainScene(IServiceCollection service) : base(service) { }
	}
}