using System.Net.Sockets;

namespace codecrafters_redis.src.Interfaces
{
    internal interface IRouteManagerService
    {
        public Task HandleClient(TcpClient handler, NetworkStream stream);
    }
}