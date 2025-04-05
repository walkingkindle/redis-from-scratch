using codecrafters_redis.src.Interfaces;
using System.Net.Sockets;
using System.Text;

namespace codecrafters_redis.src.Impelementations
{
    public class Router: IRouteManagerService
    {
        private readonly IStreamWriter _streamWriter;
        public Router(IStreamWriter streamWriter)
        {
            _streamWriter = streamWriter;
            
        }

        public async Task HandleClient(TcpClient handler, NetworkStream stream)
        {
            try
            {
                byte[] buffer = new byte[256];

                while (true)
                {
                    int readTotal = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (readTotal == 0)
                    {
                        // Client disconnected
                        break;
                    }
                    string incomingMessage = Encoding.UTF8.GetString(buffer, 0, readTotal);

                    if (string.IsNullOrEmpty(incomingMessage))
                    {
                        throw new Exception("Did not read anything from the stream");
                    }
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
