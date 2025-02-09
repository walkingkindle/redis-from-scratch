using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Main;
public class TCPServer
{

    private TcpListener _tcpListener;

    public TCPServer()
    {
        StartServer();

    }

    private void StartServer()
    {
        var port = 6379;

        var ipAddress = IPAddress.Parse("127.0.0.1");

        _tcpListener = new TcpListener(ipAddress, port);

        _tcpListener.Start();

        byte[] buffer = new byte[256];

        string receivedMessage;


        using TcpClient client = _tcpListener.AcceptTcpClient();

        var tcpStream = client.GetStream();

        int readTotal;

        while((readTotal = tcpStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            string incomingMessage = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            byte[] response = Encoding.UTF8.GetBytes("PONG");

            tcpStream.Write(response,0, response.Length);

            

        }
    }
}       