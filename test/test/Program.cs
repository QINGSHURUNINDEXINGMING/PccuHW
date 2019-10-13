using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperWebSocket;

namespace test
{
    class Program
    {
        private static WebSocketServer wsServer;
        static void Main(string[] args)
        {
            wsServer = new WebSocketServer();

            int port = 8088;

            wsServer.Setup(port);

            wsServer.NewSessionConnected += WsServer_NewSessionConnected;

            wsServer.NewMessageReceived += WsServer_NewMessageReceived;

            wsServer.NewDataReceived += WsServer_NewDataReceived;

            wsServer.SessionClosed += WsServer_SessionClosed;

            wsServer.Start();

            Console.WriteLine("Server is running on port " + port + ". Press ENTER to exit....\n");

            Console.ReadKey();
        }

        private static void WsServer_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            Console.WriteLine("SessionClosed\n");
        }

        private static void WsServer_NewDataReceived(WebSocketSession session, byte[] value)
        {
            Console.WriteLine("NewDataReceived\n");
        }

        private static void WsServer_NewMessageReceived(WebSocketSession session, string value)
        {
            Console.WriteLine("NewMessageReceived: " + value);
            if (value == "Hello server_HTML\n")
            {
                session.Send("Hello client\n");
            }
        }

        private static void WsServer_NewSessionConnected(WebSocketSession session)
        {
            Console.WriteLine("NewSessionConnected\n");
        }
    }
}
