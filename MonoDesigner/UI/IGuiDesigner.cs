using Microsoft.Xna.Framework;

namespace MonoDesigner.UI
{
	interface IGuiDesigner {
		void Draw(GameTime gameTime);
		void Update(GameTime gameTime);
		void Resize();
	}
}
