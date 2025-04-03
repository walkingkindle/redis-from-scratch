using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using codecrafters_redis;
using System.Net.Sockets;
using codecrafters_redis.src.Interfaces;

namespace Main;
class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
                          .ConfigureServices((hostContext, services) =>
                          {
                              var startup = new Startup();
                              startup.ConfigureServices(services);
                          });
        var host = builder.Build();

        await StartServer(host, args);
    }

    private static async Task StartServer(IHost host, string[] args)
    {
        var listener = host.Services.GetRequiredService<TcpListener>();
        var clientHandler = host.Services.GetRequiredService<IRouteManagerService>();

        listener.Start();
        Console.WriteLine("Server is running on port 4221...");

        try
        {
            while (true)
            {
                var handler = await listener.AcceptTcpClientAsync();
                var stream = handler.GetStream();
                _ = Task.Run(() => clientHandler.HandleClient(handler, stream));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Server error: {ex.Message}");
        }
        finally
        {
            listener.Stop();
        }
    }
}
