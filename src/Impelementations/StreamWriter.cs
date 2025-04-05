using codecrafters_redis.src.Interfaces;
using System.Net.Sockets;
using System.Text;

namespace codecrafters_redis.src.Impelementations
{
    public class StreamWriter : IStreamWriter
    {
        public async ValueTask WriteToStream(NetworkStream stream, string outgoingMessage)
        {
            byte[] outgoingMessageBytes = Encoding.UTF8.GetBytes(outgoingMessage);

            await stream.WriteAsync(outgoingMessageBytes);
        }
    }
}
