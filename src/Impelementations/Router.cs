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

               var readTotal = stream.Read(buffer, 0, buffer.Length);

               string incomingMessage = Encoding.UTF8.GetString(buffer, 0, readTotal);

                if (string.IsNullOrEmpty(incomingMessage))
                {
                    throw new Exception("Did not read anything from the stream");
                }
                await _streamWriter.WriteToStream(stream, incomingMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
            finally
            {
                handler.Close();
            }
    }

    }
 }
