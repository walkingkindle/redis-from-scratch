using codecrafters_redis.src.Interfaces;
using System.Net.Sockets;
using System.Text;

namespace codecrafters_redis.src.Impelementations
{
    public class Router: IRouteManagerService
    {
        private readonly IStreamWriter _streamWriter;
        private readonly IRespParser _parser;
        public Router(IStreamWriter streamWriter, IRespParser parser)
        {
            _streamWriter = streamWriter;
            _parser = parser;
            
        }

        public async Task HandleClient(TcpClient handler, NetworkStream stream)
        {
            try
            {
                byte[] buffer = new byte[256];

                while (handler.Connected)
                {
                    int readTotal = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (readTotal == 0) break;

                    string incomingMessage = _parser.ParseRespString(buffer, readTotal);

                    await _streamWriter.WriteToStream(stream, incomingMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
            finally{
                stream.Close();
                handler.Close();
            }
        }

    }
 }
