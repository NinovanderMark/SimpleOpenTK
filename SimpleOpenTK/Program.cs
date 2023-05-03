using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform.Windows;
using OpenTK.Windowing.Desktop;
using SimpleOpenTK;

namespace SimpleOpenTK
{
    public class Program
    {
        public static int Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .AddCommandLine(args)
              .Build();


            var serviceProvider = ConfigureServices(configuration);

            try
            {
                return Application(serviceProvider);
            }
            catch (Exception ex)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = color;
                return -1;
            }
            finally
            {
                serviceProvider.Dispose();
            }
        }

        private static ServiceProvider ConfigureServices(IConfiguration configuration)
            => new ServiceCollection()
                .AddSingleton(configuration)
                .AddOptions()
                .AddLogging(configure => configure.AddConsole())
                .Configure<VideoConfiguration>(configuration.GetSection("video"))
                .AddTransient<Window>()
                .BuildServiceProvider();

        private static int Application(IServiceProvider services)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Application started");

            // This line creates a new instance, and wraps the instance in a using statement so it's automatically disposed once we've exited the block.
            using (var window = services.GetRequiredService<Window>())
            {
                window.Run();
            }

            logger.LogInformation("Application terminated");
            return 0;
        }
    }
}
