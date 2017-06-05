
using System;


namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nPlease enter the access key given to you by the chat administrator:\n");
            string localIP = Console.ReadLine();
            const int portNumber = 11111;
            ServerScanner serverScanner = new ServerScanner();
            ChatClient chatClient = new ChatClient(serverScanner.ScanForServer(localIP, portNumber));
            chatClient.Run();
        }
    }
}