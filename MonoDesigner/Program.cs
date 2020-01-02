using System;
using MonoDesigner.Scene;
using MyGame.Core.Scene;

namespace MonoDesigner
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
			SceneManager.Add(new MainScene());
			using (var game = new MyGame.MyGame())
                game.Run();
        }
    }
}
