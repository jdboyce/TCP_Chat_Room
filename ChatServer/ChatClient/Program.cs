


namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const int portNumber = 11111;
            const string localIP = "10.2.20.50";
            ServerScanner serverScanner = new ServerScanner();
            ChatClient chatClient = new ChatClient(serverScanner.ScanForServer(localIP, portNumber));
            chatClient.Run();
        }
    }
}