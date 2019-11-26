using System;
using Microsoft.Extensions.DependencyInjection;
using MyGame.Scene;

namespace MyGame
{
    public static class Program
    {
        [STAThread]
        static void Main() {
			var services = ConfigureServices();
			var serviceProvider = services.BuildServiceProvider();
			using (var game = serviceProvider.GetService<MainScene>()) {
				game.Run();
			}
		}
		private static IServiceCollection ConfigureServices() {
			IServiceCollection services = new ServiceCollection();
			services.AddSingleton(services);
			services.AddSingleton<MainScene>();
			return services;
		}
	}
}
