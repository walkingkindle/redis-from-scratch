using codecrafters_redis.src.Interfaces;
using System.Text;

namespace codecrafters_redis.src.Impelementations
{
    public class RespParser : IRespParser
    {
        public string ParseRespString(byte[] buffer, int readTotal)
        {
            string incomingMessage = Encoding.UTF8.GetString(buffer, 0, readTotal);

            if (string.IsNullOrEmpty(incomingMessage))
            {
                throw new Exception("Did not read anything from the stream");
            }

            return ParseInternal(incomingMessage);
        }

        private string ParseInternal(string raw)
        {
            // Naive RESP array parser: *1\r\n$4\r\nPING\r\n
            var lines = raw.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length >= 3 && lines[0].StartsWith("*") && lines[1].StartsWith("$"))
                return lines[2].ToUpperInvariant();

            return raw.Trim().ToUpperInvariant();
        }
    }
}
