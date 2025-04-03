using codecrafters_redis.src.Interfaces;
using System.Net.Sockets;
using System.Text;

namespace codecrafters_redis.src.Impelementations
{
    public class StreamWriter : IStreamWriter
    {
        public async ValueTask WriteToStream(NetworkStream stream, string incomingMessage)
        {
            if (incomingMessage == "*1\r\n$4\r\nPING\r\n")
            {
                byte[] outgoingMessage = Encoding.UTF8.GetBytes("+PONG\r\n");

                await stream.WriteAsync(outgoingMessage);
            }

            await stream.WriteAsync(Encoding.UTF8.GetBytes("Error"));
        }
    }
}
