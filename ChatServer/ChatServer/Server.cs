
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;


namespace ChatServer
{
    public class ChatServer
    {

        const int portNumber = 11111;
        public IPAddress localIPParsed = IPAddress.Parse("10.2.20.50");
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
            Console.WriteLine("\nListening for Connection Requests...");
            tcpListener.Start();
            Task.Run(() => ListenForConnections());
            Task.Run(() => PrintQueue());
        }


        public void ListenForConnections()
        {
            while (true)
            {
                TcpClient newClient = tcpListener.AcceptTcpClient();
                Console.WriteLine("\nNew Connection Made");
                Task.Run(() => LogUsername(newClient));
        }}
        


        public void LogUsername(TcpClient client)
        {
            Console.WriteLine("\nRequesting client username...");
            try
            {
                SendMessage(client, "\nWelcome to the chat room! Please choose a username: ");
                string username = ReceiveMessage(client);
                clientDatabase.Add(username, client);
                AnnounceNewUser(username);
                Task.Run(() => ListenForMessages(username, client));
            }
            catch (Exception FailedConnection)
            {
                client.Close();
                logger.WriteToFile(""+FailedConnection);
                Console.WriteLine("Connection to client was lost.");
        }}


        public void AnnounceNewUser(string username)
        {
            logger.WriteToFile("User \"" + username + "\" has joined the chat room.");
            Console.WriteLine("\nUsername {0} has been saved.", username);
            foreach (var client in clientDatabase)
            {
                SendMessage(client.Value, "\n" + username + " has joined the chat room.");
        }}


        public void ListenForMessages(string username, TcpClient client)
        {
            while (true)
            {
                try
                {
                    string chatMessage = "\n" + username + ": " + ReceiveMessage(client) + "\n";
                    Console.WriteLine("\nReceived message from {0}", username);
                    messageQueue.Enqueue(chatMessage);
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
        }}}


        public string ReceiveMessage(TcpClient client)
        {
                NetworkStream reader = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];
                int bytesRead = reader.Read(buffer, 0, client.ReceiveBufferSize);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                return message;
        }


        public void PrintQueue()
        {
            while (true)
            {
                if(messageQueue.Count > 0)
                {
                    BroadcastMessage(messageQueue.Dequeue());
                }
            }}
       

        public void BroadcastMessage(string message)
        {
            if (clientDatabase.Count > 0)
            {
                logger.WriteToFile(message);
                foreach (var client in clientDatabase)
                {
                    SendMessage(client.Value, message);
                    Console.WriteLine("\nMessage sent to: {0}", client.Key);
                }
            }}
    

        public void SendMessage(TcpClient client, string text)
        {
                NetworkStream writer = client.GetStream();
                byte[] message = ASCIIEncoding.ASCII.GetBytes(text);
                writer.Write(message, 0, message.Length);
                writer.Flush();
                writer = null;
        }
    }
}