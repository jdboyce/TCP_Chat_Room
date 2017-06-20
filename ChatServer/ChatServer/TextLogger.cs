
using System;
using System.IO;

namespace ChatServer
{
    public class TextLogger : ILoggable
    {
        public void WriteToFile(string textToLog)
        {
            File.AppendAllText("ChatRoomLog.txt", Environment.NewLine + DateTime.Now + " - " + textToLog + Environment.NewLine);
        }
    }
}






























//const string localIP = "10.2.20.50";


/* namespace Server
{
    class Program
    {
        const int portNumber = 54321;
        const string serverIP = "127.0.0.1";

        static void Main(string[] args)
        {



            //---listen at the specified IP and port no.---

            IPAddress localIPAddress = IPAddress.Parse(serverIP);
            TcpListener listener = new TcpListener(localIPAddress, portNumber);
            Console.WriteLine("Listening...");
            listener.Start();





            //---incoming client connected---

            TcpClient client = listener.AcceptTcpClient();





            //---get the incoming data through a network stream---

            NetworkStream networkStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];





            //---read incoming stream---

            int bytesRead = networkStream.Read(buffer, 0, client.ReceiveBufferSize);





            //---convert the data received into a string---

            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received : " + dataReceived);





            //---write back the text to the client---

            Console.WriteLine("Sending back : " + dataReceived);
            networkStream.Write(buffer, 0, bytesRead);
            client.Close();
            listener.Stop();
            Console.ReadLine();


        }
    }
}
*/





//namespace ChatServer
//{
//class Program
////{
//static byte[] Buffer { get; set; }
//static Socket socket;
//static void Main(string[] args)
//{
//    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//    socket.Bind(new IPEndPoint(IPAddress.Any, 1234));
//    socket.Listen(100);

//    Socket accepted = socket.Accept();
//    Buffer = new byte[accepted.SendBufferSize];
//    int bytesRead = accepted.Receive(Buffer);
//    byte[] formatted = new byte[bytesRead];
//    for (int i = 0; i < bytesRead; i++)
//    {
//        formatted[i] = Buffer[i];
//    }
//    string stringData = Encoding.ASCII.GetString(formatted);
//    Console.WriteLine(stringData + "\r\n");
//    Console.ReadLine();

//    socket.Close();
//    accepted.Close();

//}
//    }
//}
















//    TcpListener serverSocket = new TcpListener(8888);
//    int requestCount = 0;
//    TcpClient clientSocket = default(TcpClient);
//    serverSocket.Start();
//    Console.WriteLine(" >> Server Started");
//    clientSocket = serverSocket.AcceptTcpClient();
//    Console.WriteLine(" >> Accept connection from client");
//    requestCount = 0;

//    while ((true))
//    {
//        try
//        {
//            requestCount = requestCount + 1;
//            NetworkStream networkStream = clientSocket.GetStream();
//            byte[] bytesFrom = new byte[10025];
//            networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
//            string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
//            dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
//            Console.WriteLine(" >> Data from client - " + dataFromClient);
//            string serverResponse = "Last Message from client" + dataFromClient;
//            Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
//            networkStream.Write(sendBytes, 0, sendBytes.Length);
//            networkStream.Flush();
//            Console.WriteLine(" >> " + serverResponse);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.ToString());
//        }
//    }

//    clientSocket.Close();
//    serverSocket.Stop();
//    Console.WriteLine(" >> exit");
//    Console.ReadLine();



















































