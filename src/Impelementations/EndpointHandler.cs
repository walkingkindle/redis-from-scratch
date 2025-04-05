using codecrafters_redis.src.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace codecrafters_redis.src.Impelementations
{
    public class EndpointHandler : IEndpointHandler
    {
        public EndpointHandler(Dictionary<string, string> redisStore)
        {
            RedisStore = redisStore;
        }

        public Dictionary<string,string> RedisStore { get; set; }
        public string Handle(Endpoint endpoint)
        {
            switch (endpoint.EndpointName)
            {
                case "PING":
                    return "+PONG\r\n";
                case "ECHO":
                    var valueLength = Encoding.UTF8.GetByteCount(endpoint.Value);
                    return $"${valueLength}\r\n{endpoint.Value}\r\n";
                case "SET":
                    return HandleSet(endpoint.Key, endpoint.Value);
                case "GET":
                    return HandleGet(endpoint.Key);
                default:
                    return "-ERR unknown command\r\n";
            }
        }

        private string HandleGet(string key)
        {
            string retrievedValue;
            if(RedisStore.TryGetValue(key, out retrievedValue))
            {
                return $"${Encoding.UTF8.GetByteCount(retrievedValue)}\r\n{retrievedValue}\r\n";
            }
            return "$-1\r\n";
        }

        private string HandleSet(string key, string value)
        {
            RedisStore.Add(key:key,value:value);

            return "+OK\r\n";
        }
    }
}
