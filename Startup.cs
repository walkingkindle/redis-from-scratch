using codecrafters_redis.src.Impelementations;
using codecrafters_redis.src.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Sockets;
using StreamWriter = codecrafters_redis.src.Impelementations.StreamWriter;

namespace Main
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<TcpListener>(sp =>
            {
                var ipEndPoint = new IPEndPoint(IPAddress.Any, 4221);
                var listener = new TcpListener(ipEndPoint);
                return listener;
            });

            services.AddTransient<IRouteManagerService, Router>();

            services.AddTransient<IStreamWriter, StreamWriter>();
        }

        }
    }