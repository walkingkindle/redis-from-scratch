using System.Net.Sockets;

namespace codecrafters_redis.src.Interfaces
{
    public interface IStreamWriter
    {
        public ValueTask WriteToStream(NetworkStream stream, string incomingMessage);

    }
}
