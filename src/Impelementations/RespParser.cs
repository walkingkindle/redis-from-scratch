using codecrafters_redis.src.Interfaces;
using System.Text;

namespace codecrafters_redis.src.Impelementations
{
    public class RespParser : IRespParser
    {
        public Endpoint ParseRespString(byte[] buffer, int readTotal)
        {
            string incomingMessage = Encoding.UTF8.GetString(buffer, 0, readTotal);

            if (string.IsNullOrEmpty(incomingMessage))
            {
                throw new Exception("Did not read anything from the stream");
            }

            return ParseInternal(incomingMessage);
        }

        private Endpoint ParseInternal(string raw)
        {
            var lines = raw.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            string endpoint = lines[2].ToUpperInvariant();

            if (lines.Length >= 5)
            {
                string value = lines[4];

                return Endpoint.CreateEndpoint(endpoint, value);
            }

            return Endpoint.CreateEndpoint(endpoint, null);
        }
    }
}
