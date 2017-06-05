
using System;
using System.Threading;
using System.Net.Sockets;


namespace ChatClient
{
    public class ServerScanner
    {

        public TcpClient ScanForServer(string localIP, int portNumber)
        {
            while (true)
            {
                try
                {
                    TcpClient tcpClient = new TcpClient(localIP, portNumber);
                    return tcpClient;
                }
                catch
                {
                    Console.WriteLine("\nSearching for connection...");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
