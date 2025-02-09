using System.Net;
using System.Net.Sockets;
using System.Text;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

// Uncomment this block to pass the first stage
TcpListener server = new TcpListener(IPAddress.Any, 6379);
server.Start();

while (true)
{
    var listener = await server.AcceptSocketAsync();
    await HandleClientAsync(listener);
}

static async Task HandleClientAsync(Socket clientSocket)
{
    byte[] buffer = new byte[1024];

    try
    {
        int bytesRead = await clientSocket.ReceiveAsync(buffer, SocketFlags.None);
        if (bytesRead == 0) return; 
        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
        Console.WriteLine($"Received: {receivedMessage}");

        if (receivedMessage.Equals("ping", StringComparison.OrdinalIgnoreCase))
        {
            byte[] response = Encoding.UTF8.GetBytes("PONG\n");
            await clientSocket.SendAsync(response, SocketFlags.None);
            Console.WriteLine("Sent: PONG");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    finally
    {
        clientSocket.Close();
        Console.WriteLine("Client disconnected.");
    }
}