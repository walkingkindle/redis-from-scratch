using codecrafters_redis.src.Interfaces;
using System.Text;

namespace codecrafters_redis.src.Impelementations
{
    public class EndpointHandler : IEndpointHandler
    {
        public string Handle(Endpoint endpoint)
        {
            switch (endpoint.EndpointName)
            {
                case "PING":
                    return "+PONG\r\n";
                case "ECHO":
                    var valueLength = Encoding.UTF8.GetByteCount(endpoint.Value);
                    return $"${valueLength}\r\n{endpoint.Value}\r\n";
                default:
                    return "-ERR unknown command\r\n";
            }
        }
    }
}
