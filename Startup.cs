﻿using codecrafters_redis.src.Impelementations;
using codecrafters_redis.src.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
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
                var ipEndPoint = new IPEndPoint(IPAddress.Any, 6379);
                var listener = new TcpListener(ipEndPoint);
                return listener;
            });
            services.AddSingleton<Dictionary<string, string>>();

            services.AddTransient<IRouteManagerService, Router>();

            services.AddTransient<IStreamWriter, StreamWriter>();

            services.AddTransient<IRespParser, RespParser>();

            services.AddTransient<IEndpointHandler, EndpointHandler>();
        }

        }
    }