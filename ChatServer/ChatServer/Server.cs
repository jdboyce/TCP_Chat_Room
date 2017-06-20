
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Linq;


namespace ChatServer
{
    public class ChatServer
    {
        const int portNumber = 11111;
        public IPAddress localIPParsed = IPAddress.Any;
        public TcpListener tcpListener;
        public Dictionary<string, TcpClient> clientDatabase;
        public Queue<string> messageQueue;
        public ILoggable logger;

        public ChatServer(ILoggable logger)
        { 
            tcpListener = new TcpListener(localIPParsed, portNumber);
            clientDatabase = new Dictionary<string, TcpClient>();
            messageQueue = new Queue<string>();
            this.logger = logger;
        }


        public void Run()
        {
            Console.WriteLine("\nThe chat server access key is: " + GetIP() + "\n\nListening for Connection Requests...");   /* 1 > */
            try
            {
                tcpListener.Start();
            }
            catch(SocketException e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            Task.Run(() => ListenForConnections());   /* 2 > */
            Task.Run(() => PrintQueue());   /* 7 > */
        }


        public string GetIP()   /* 1 */
        {
            string localIPv4 = null;
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip4 in ipaddress.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
            {
                localIPv4 = ip4.ToString();
            }
            return localIPv4;
        }


        public void ListenForConnections()   /* 2 */
        {
            while (true)
            {
                TcpClient newClient = tcpListener.AcceptTcpClient();
                Console.WriteLine("\nNew Connection Made");
                Task.Run(() => LogUsername(newClient));   /* 3 > */
            }
        }
        

        public void LogUsername(TcpClient client)   /* 3 */
        {
            Console.WriteLine("\nRequesting client username...");
            try
            {
                SendMessage(client, "\nWelcome to the chat room! Please enter a username:");
                string username = ReceiveMessage(client);
                clientDatabase.Add(username, client);
                AnnounceNewUser(username);   /* 4 > */
                Task.Run(() => ListenForMessages(username, client));  /* 5 > */
            }
            catch (Exception FailedConnection)
            {
                client.Close();
                logger.WriteToFile("" + FailedConnection);
                Console.WriteLine("Connection to client was lost.");
            }
        }


        public void AnnounceNewUser(string username)   /* 4 */
        {
            logger.WriteToFile("User \"" + username + "\" has joined the chat room.");
            Console.WriteLine("\nUsername \"{0}\" has been saved.", username);
            foreach (var client in clientDatabase)
            {
                SendMessage(client.Value, username + "* has joined the chat room.");   /* ^ */
            }
        }


        public void ListenForMessages(string username, TcpClient client)   /* 5 */
        {
            while (true)
            {
                try
                {
                    string chatMessage = username + "@: " + ReceiveMessage(client);   /* 6 > */
                    Console.WriteLine("\nReceived message from {0}", username);
                    messageQueue.Enqueue(chatMessage);   /* ^ */
                }
                catch (Exception FailedConnection)
                {
                    client.Close();
                    clientDatabase.Remove(username);
                    logger.WriteToFile("User \"" + username + "\" has left the chat room.");
                    logger.WriteToFile(""+FailedConnection);
                    Console.WriteLine("\n" + username + " has left the chat room.");
                    string clientDisconnected = username + " has left the chat room.";
                    messageQueue.Enqueue(clientDisconnected);
                    break;
                }
            }
        }


        public string ReceiveMessage(TcpClient client)   /* 6 */
        {
                NetworkStream reader = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];
                int bytesRead = reader.Read(buffer, 0, client.ReceiveBufferSize);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                return message;   /* ^ */
        }


        public void PrintQueue()   /* 7 */
        {
            while (true)
            {
                if(messageQueue.Count > 0)
                {
                    BroadcastMessage(messageQueue.Dequeue());   /* 8 > */
                }
            }
        }
       

        public void BroadcastMessage(string message)   /* 8 */
        {
            if (clientDatabase.Count > 0)
            {
                logger.WriteToFile(message);
                foreach (var client in clientDatabase)
                {
                    SendMessage(client.Value, message);   /* 9 > */
                    Console.WriteLine("\nMessage sent to: {0}", client.Key);
                }
            }
        }


        public void SendMessage(TcpClient client, string text)  /* 10 */
        {
                NetworkStream writer = client.GetStream();
                byte[] message = ASCIIEncoding.ASCII.GetBytes(text);
                writer.Write(message, 0, message.Length);
                writer.Flush();
                writer = null;
        }
    }
}