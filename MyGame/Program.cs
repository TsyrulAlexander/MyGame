using System;
using MyGame.Scene;

namespace MyGame
{
    public static class Program
    {
        [STAThread]
        static void Main() {
			using (var game = new MainScene()) {
				game.Run();
			}
		}
    }
}
